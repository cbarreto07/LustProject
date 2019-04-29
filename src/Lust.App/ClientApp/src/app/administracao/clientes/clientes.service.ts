import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, Resolve, RouterStateSnapshot } from '@angular/router';

import { BehaviorSubject, Observable } from 'rxjs';
import { DataService } from 'app/core/services/data.service';

@Injectable()
export class ClientesService implements Resolve<any>
{
  clientes: any[];
  totalRows: number = 0;;
  onClientesChanged: BehaviorSubject<any> = new BehaviorSubject({});
  onTotalRowsChanged: BehaviorSubject<any> = new BehaviorSubject({});


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
        this.getClientes(0,10, null)
      ]).then(
        () => {
          resolve();
        },
        reject
      );
    });
  }

  getClientes(skip: number, take: number, query?: any | null, sort?: string , direction?: string  ): Promise < any > {

    let param: { [key: string]: any } = { 'skip': skip, 'take': take };
    if (query)
      param.query = query;

    if (sort) {
      param.sort = sort;
      if (direction)
        param.direction = direction;
    }

    

    return new Promise((resolve, reject) => {
      this.dataService.getList('api/clientes', param)
        .subscribe((response: any) => {
          this.clientes = response.data;
          if (this.totalRows != response.totalRows) {
            this.totalRows = response.totalRows;
            this.onTotalRowsChanged.next(this.totalRows);
          }
          this.onClientesChanged.next(this.clientes);
          
          
          resolve(response);
        }, reject);
    });
  }
}
