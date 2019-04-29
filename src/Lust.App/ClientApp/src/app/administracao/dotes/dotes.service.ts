import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, Resolve, RouterStateSnapshot } from '@angular/router';

import { BehaviorSubject, Observable } from 'rxjs';
import { DataService } from 'app/core/services/data.service';

@Injectable()
export class DotesService implements Resolve<any>
{
  
  dotes: any[];
  totalRows: number = 0;;
  onDotesChanged: BehaviorSubject<any> = new BehaviorSubject({});
  onTotalRowsChanged: BehaviorSubject<any> = new BehaviorSubject({});
  onDelete: BehaviorSubject<any> = new BehaviorSubject({});


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
        this.getDotes(0, 10, null)
      ]).then(
        () => {
          resolve();
        },
        reject
      );
    });
  }

  getDotes(skip: number, take: number, query?: any | null, sort?: string, direction?: string): Promise<any> {

    let param: { [key: string]: any } = { 'skip': skip, 'take': take };
    if (query)
      param.query = query;

    if (sort) {
      param.sort = sort;
      if (direction)
        param.direction = direction;
    }
    

    return new Promise((resolve, reject) => {
      this.dataService.getList('api/dotes', param)
        .subscribe((response: any) => {
          this.dotes = response.data;
          if (this.totalRows != response.totalRows) {
            this.totalRows = response.totalRows;
            this.onTotalRowsChanged.next(this.totalRows);
          }
          this.onDotesChanged.next(this.dotes);
          resolve(response);
        }, reject);
    });
  }

  deleteDote(dote: any): Promise<any> {
    return new Promise((resolve, reject) => {
      this.dataService.delete('api/dotes/' + dote.id)
        .subscribe(response => {
          this.onDelete.next(response);
          resolve(response);
        }, reject);
    });
  }

}
