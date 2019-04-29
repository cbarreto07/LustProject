import { Injectable } from '@angular/core';
import { DataService } from 'app/core/services/data.service';
import { ActivatedRouteSnapshot, Resolve, RouterStateSnapshot } from '@angular/router';
import { BehaviorSubject, Observable } from 'rxjs';


@Injectable()
export class DoteService implements Resolve<any>
{
  routeParams: any;
  dote: any;
  onDoteChanged: BehaviorSubject<any> = new BehaviorSubject({});

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
        this.getDote()
      ]).then(
        () => {
          resolve();
        },
        reject
      );
    });
  }

  getDote(): Promise<any> {
    return new Promise((resolve, reject) => {
      if (this.routeParams.id === 'novo') {
        this.onDoteChanged.next(false);
        resolve(false);
      }
      else {
        this.dataService.get<any>('api/dotes/' + this.routeParams.id)
          .subscribe((response: any) => {
            this.dote = response;
            this.onDoteChanged.next(this.dote);
            resolve(response);
          }, reject);
      }
    });
  }

  saveDote(dote) {
    return new Promise((resolve, reject) => {
      this.dataService.put('api/dotes/', dote)
        .subscribe((response: any) => {
          resolve(response);
        }, reject);
    });
  }

  addDote(dote) {
    return new Promise((resolve, reject) => {
      this.dataService.post('api/dotes/', dote)
        .subscribe((response: any) => {
          resolve(response);
        }, reject);
    });
  }
}
