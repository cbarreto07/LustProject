import { Component, OnInit, Inject, PLATFORM_ID } from '@angular/core';
import { isPlatformBrowser } from '@angular/common';

import { Params, ActivatedRoute, Router } from '@angular/router';

import { authConfig } from './auth.config';

//import { routerTransition } from './router.animations';
import { ExternalLoginStatus } from './app.models';
import { AccountService } from './core/services/account.service';

import { FuseSplashScreenService } from '@fuse/services/splash-screen.service';
import { FuseTranslationLoaderService } from '@fuse/services/translation-loader.service';
import { FuseNavigationService } from '@fuse/components/navigation/navigation.service';

import { TranslateService } from '@ngx-translate/core';

import { locale as navigationPortugues } from './navigation/i18n/pt';
import { JwksValidationHandler } from './angular-oauth2-oidc/token-validation/jwks-validation-handler';
import { OAuthService } from './angular-oauth2-oidc/oauth-service';

import { filter, take, delay, first, tap, map } from 'rxjs/operators';

@Component({
  selector: 'appc-root',
  //animations: [routerTransition],
  styleUrls: ['./app.component.scss'],
  templateUrl: './app.component.html'
})
export class AppComponent implements OnInit {
  appData: IApplicationConfig;

    constructor(
        private accountService: AccountService,
        private router: Router,
        @Inject('BASE_URL') private baseUrl: string,
        @Inject(PLATFORM_ID) private platformId: string,
        private activatedRoute: ActivatedRoute,
        private oauthService: OAuthService,
        private fuseNavigationService: FuseNavigationService,
        private fuseSplashScreen: FuseSplashScreenService,
        private fuseTranslationLoader: FuseTranslationLoaderService,
        private translate: TranslateService
  ) {

    if (isPlatformBrowser(this.platformId)) {
      this.configureOidc();
    }

        // Add languages
        this.translate.addLangs(['pt']);

        // Set the default language
        this.translate.setDefaultLang('pt');

        // Set the navigation translations
        this.fuseTranslationLoader.loadTranslations(navigationPortugues);

        // Use a language
        this.translate.use('pt');

  }

  public ngOnInit() {
      this.activatedRoute.queryParams.subscribe((params: Params) => {
          const param = params['externalLoginStatus'];
          if (param) {
              const status = <ExternalLoginStatus>+param;
              switch (status) {
                  case ExternalLoginStatus.CreateAccount:
                      const email = params['email'];
                      const nome = params['nome'];
                      this.accountService.ExternalEmail = email;
                      this.accountService.ExternalNome = nome;                          
                      this.router.navigate(['registro']);
                      break;

                  default:
                      break;
              }
          }
      });

      //if (!this.accountService.isLoggedIn && this.accountService.accessToken !== null) {
      //      this.accountService.refreshToken()
      //          .then((tokenResponse: any) => {

      //          })
      //          .catch((err) => {


      //          });
      //  }


     

  }

    checkLogado() {
        if (this.accountService.isLoggedIn) { // se ja estiver logado não ve a tela inicial
            var page = ""
            var user = this.accountService.user;

            if (user.role.indexOf("Admin") >= 0)
              page = "administracao"
            else if (user.estaOferecendo == true)
              page = "quero-oferecer"
            else if (user.estaDesfrutando)
                page = "quero-desfrutar";
            else page = "bem-vindo"

            setTimeout(() => {
                this.router.navigate([page]);
            }, 100);

        } else {
            this.accountService.refreshToken().then(() => {
                if (this.accountService.isLoggedIn)
                    this.checkLogado();
            }
            );
        }
    }


    public getState(outlet: any) {
        return outlet.activatedRouteData.state;
    }

    private configureOidc() {
        this.oauthService.configure(authConfig(this.baseUrl));
        this.oauthService.setStorage(localStorage);
      this.oauthService.tokenValidationHandler = new JwksValidationHandler();
      this.oauthService.setupAutomaticRefresh();

        this.oauthService.loadDiscoveryDocumentAndTryLogin()
            .then(() => {
                //console.log(this.router.url);
                if (location.pathname == '/' || location.pathname == '/login') {
                    setTimeout(() => {
                        this.checkLogado();
                    });
                    
                }
            }
            );


      this.oauthService.events.pipe(filter(e => e.type === 'token_expires')).subscribe(e => {
        var a = 0;
            this.accountService.refreshToken().then(() => {                
                if (location.pathname == '/' || location.pathname == '/login') {                    
                        this.checkLogado();
                }
            }
            );
      });


        //this.oauthService.setupAutomaticSilentRefresh();
        //;.filter(e => e.type === 'token_received')
        //this.oauthService.events.subscribe(e => {
        //    if ( this.accountService.isLoggedIn) {
        //        var page = ""
        //        var user = this.accountService.user;

        //        if (user.role.indexOf("Admin") >= 0)
        //            page = "administracao"
        //        else if (user.quer_oferecer)
        //            page = "quero-oferecer"
        //        else if (user.quer_desfrutar)
        //            page = "quero-desfrutar";
        //        else page = "bem-vindo"
        //        this.router.navigate([page]);
        //    }
        //});


        //this.oauthService.events.subscribe(e => {
        //    console.debug('oauth/oidc event', e);
        //    if (e.type === "token_received" && this.accountService.isLoggedIn) {
        //        var page = ""
        //        var user = this.accountService.user;

        //        if (user.role.indexOf("Admin") >= 0)
        //            page = "administracao"
        //        else if (user.quer_oferecer)
        //            page = "quero-oferecer"
        //        else if (user.quer_desfrutar)
        //            page = "quero-desfrutar";
        //        else page = "bem-vindo"
        //        this.router.navigate([page]);
        //    }
        //});

    }

}
