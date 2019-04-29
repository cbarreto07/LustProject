import { Injectable } from '@angular/core';
import { DataService } from 'app/core/services/data.service';
import { ActivatedRouteSnapshot, Resolve, RouterStateSnapshot } from '@angular/router';
import { BehaviorSubject, Observable } from 'rxjs';

@Injectable()
export class GerenciarPerfilService implements Resolve<any>
{  
  perfil: any;
  onPerfilChanged: BehaviorSubject<any> = new BehaviorSubject({});

  constructor(
    private dataService: DataService
  ) {
  }

  /**
   * Resolve
   * @param {ActivatedRouteSnapshot} route
   * @param {RouterStateSnapshot} state
   * @returns {Observable<any> | Promise<any> | any}
   */
  resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<any> | Promise<any> | any {

    

    return new Promise((resolve, reject) => {

      Promise.all([
        this.getPerfil()
      ]).then(
        () => {
          resolve();
        },
        reject
      );
    });
  }

  getPerfil(): Promise<any> {
    return new Promise((resolve, reject) => {
      
      this.dataService.get<any>('api/manage/userinfo')
          .subscribe((response: any) => {
            this.perfil = response;
            this.onPerfilChanged.next(this.perfil);
            resolve(response);
          }, reject);
      
    });
  }

  
}
