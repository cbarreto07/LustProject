import { Injectable, PLATFORM_ID, Inject } from '@angular/core';

import { JwtHelper } from 'angular2-jwt';
import { isPlatformBrowser } from '@angular/common';

import { OAuthService } from 'app/angular-oauth2-oidc/oauth-service';
import { filter, take, delay, first, tap, map } from 'rxjs/operators';
import { Subject } from 'rxjs/Subject';
import { DataService } from './data.service';

@Injectable()



export class AccountService {

  private _user: User;
  private role: string;
    public jwtHelper: JwtHelper = isPlatformBrowser(this.platformId) && new JwtHelper();
  public onUserLoggedIn  = new Subject<any>();
  
  constructor(private oAuthService: OAuthService, @Inject(PLATFORM_ID) private platformId: string, private dataService: DataService) {

      

      oAuthService.events.pipe(filter(e => e.type === 'token_received')).subscribe(e => {
        this.onUserLoggedIn.next(this.user);//para agilizar e ter algo para exibir
        this.loadUserInfo();
      });

  }

  private loadUserInfo() {
    
    this.dataService.get<User>('api/manage/userinfo')
      .subscribe(
      result => {
        result.role = this.role;
          this._user = result;
          this.onUserLoggedIn.next(result);
      });

  }

    public get isLoggedIn(): boolean {
        let hasValidAccessToken =  this.oAuthService.hasValidAccessToken()
        return isPlatformBrowser(this.platformId) && hasValidAccessToken   ;
    }

    public refreshToken(): Promise<object> {

      return this.oAuthService.refreshToken();
    }
    public get user(): User | undefined {
      if (this._user == null && isPlatformBrowser(this.platformId) && this.idToken) {
        //return this.jwtHelper.decodeToken(this.idToken);

        let user = this.jwtHelper.decodeToken(this.idToken);
        this._user = new User();
        this._user.id = user.sub;
        this._user.nome = user.name;
        this._user.email = user.name;
        this._user.estaOferecendo = user.quer_oferecer == 'true';
        this._user.estaDesfrutando = user.quer_desfrutar == 'true';
        this._user.role = user.role;
        this.role = user.role;
        this.loadUserInfo();
      }
      return this._user || new User();
    }

    public userByIdToken(idToken: string): User | undefined {

        localStorage.setItem('id_token', idToken);
        return this.user;
    }

    public get accessToken(): string {
        if (isPlatformBrowser(this.platformId)) {
            return this.oAuthService.getAccessToken();
        }
        return '';
    }
    // Used to access user information
    public get idToken(): string {
        //return this.oAuthService.getIdToken();
        return localStorage.getItem('id_token');
    }

    public ExternalEmail: string;
    public ExternalNome: string;


   
}


export class User {
    id: string;
    nome: string;
    email: string;
    dataNascimento: Date;
    fotoDeCapa: string;
    fotoDePerfil: String;
    estaOferecendo: boolean;
    estaDesfrutando: boolean;
    role: string;
}
