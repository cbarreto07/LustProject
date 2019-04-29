import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, Resolve, RouterStateSnapshot } from '@angular/router';
import { BehaviorSubject } from 'rxjs/BehaviorSubject';
import { DataService } from 'app/core/services/data.service';
import { Preferencia } from 'app/classes/preferencia';

@Injectable()
export class PreferenciaService implements Resolve<any>{

    routeParams: any;
    preferencia: any;
    onPreferenciaChanged: BehaviorSubject<any> = new BehaviorSubject({});
    
    constructor(private dataService: DataService) { }

     /**
     * Resolve
     * @param {ActivatedRouteSnapshot} route
     * @param {RouterStateSnapshot} state
     * @returns {Observable<any> | Promise<any> | any}
     */

    resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {

        this.routeParams = route.params;

        return new Promise((resolve, reject) => {

            Promise.all([
                this.getPreferencia()
            ]).then(
                () => {
                    resolve();
                },
                reject
            );
        });
    }


    getPreferencia(): Promise<any> {
        return new Promise((resolve, reject) => {
                       
            this.dataService.get<Preferencia>('api/manage/preferencias')
                .subscribe(
                result => {
                    this.preferencia = result;
                    this.onPreferenciaChanged.next(this.preferencia);
                        resolve(result);
                    },reject);

        });
    }

    savePreferencia(preferencia) {
        return new Promise((resolve, reject) => {
            this.dataService.post<Preferencia>('api/manage/preferencias', preferencia)
                .subscribe((response: any) => {
                    resolve(response);
                }, reject);
        });
    }

}
