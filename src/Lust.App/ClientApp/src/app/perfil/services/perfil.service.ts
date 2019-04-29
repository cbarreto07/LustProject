import { Injectable } from '@angular/core';
import { DataService } from 'app/core/services/data.service';
import { ActivatedRouteSnapshot, Resolve, RouterStateSnapshot } from '@angular/router';
import { BehaviorSubject, Observable } from 'rxjs';

@Injectable()
export class PerfilService implements Resolve<any>
{
  routeParams: any;
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

    this.routeParams = route.params;

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

      this.dataService.get<any>('api/perfis/' + this.routeParams.id)
        .subscribe((response: any) => {
          this.perfil = response;
          this.onPerfilChanged.next(this.perfil);
          resolve(response);
        }, reject);

    });
  }


}
