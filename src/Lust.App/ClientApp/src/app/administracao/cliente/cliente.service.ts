import { Injectable } from '@angular/core';
import { DataService } from 'app/core/services/data.service';
import { ActivatedRouteSnapshot, Resolve, RouterStateSnapshot } from '@angular/router';
import { BehaviorSubject, Observable } from 'rxjs';


@Injectable()
export class ClienteService implements Resolve<any>
{
  routeParams: any;
  cliente: any;
  onClienteChanged: BehaviorSubject<any> = new BehaviorSubject({});

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
        this.getCliente()
      ]).then(
        () => {
          resolve();
        },
        reject
      );
    });
  }

  getCliente(): Promise<any> {
    return new Promise((resolve, reject) => {
      if (this.routeParams.id === 'novo') {
        this.onClienteChanged.next(false);
        resolve(false);
      }
      else {
        this.dataService.get<any>('api/clientes/' + this.routeParams.id)
          .subscribe((response: any) => {
            this.cliente = response;
            this.onClienteChanged.next(this.cliente);
            resolve(response);
          }, reject);
      }
    });
  }

  saveCliente(cliente) {
    return new Promise((resolve, reject) => {
      this.dataService.post('api/clientes/', cliente)
        .subscribe((response: any) => {
          resolve(response);
        }, reject);
    });
  }

  addCliente(cliente) {
    return new Promise((resolve, reject) => {
      this.dataService.put('api/clientes/', cliente)
        .subscribe((response: any) => {
          resolve(response);
        }, reject);
    });
  }
}
