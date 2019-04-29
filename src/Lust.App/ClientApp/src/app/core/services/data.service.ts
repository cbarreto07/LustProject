import { Injectable, Injector } from '@angular/core';
import { HttpClient, HttpResponse, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';
import { Subject } from 'rxjs/Subject';
import { throwError } from 'rxjs';

import { filter, take, delay, first, tap, map, catchError } from 'rxjs/operators';

//import { AccountService } from './account.service';

@Injectable()
export class DataService {

    // Define the internal Subject we'll use to push the command count
    public pendingCommandsSubject = new Subject<number>();
    public pendingCommandCount = 0;

    // Provide the *public* Observable that clients can subscribe to
    public pendingCommands$: Observable<number>;

    constructor(
        public http: HttpClient,
        private inj: Injector
    ) {
        this.pendingCommands$ = this.pendingCommandsSubject.asObservable();
    }

    public getImage(url: string): Observable<any> {
        return Observable.create((observer: any) => {
            const req = new XMLHttpRequest();
            req.open('get', url);
            req.onreadystatechange = function () {
                if (req.readyState === 4 && req.status === 200) {
                    observer.next(req.response);
                    observer.complete();
                }
            };

           // req.setRequestHeader('Authorization', `Bearer ${this.inj.get(AccountService).accessToken}`);
            req.send();
        });
    }

    public get<T>(url: string, params?: any): Observable<T> {
       // return this.http.get<T>(url, { params: this.buildUrlSearchParams(params) });

      let response = this.http.get<T>(url, { params: this.buildUrlSearchParams(params) })
        .pipe(map(this.extractData), catchError(this.serviceError));
            
        return response;
    }

  public getList<T>(url: string, params?: any): Observable<T> {
    // return this.http.get<T>(url, { params: this.buildUrlSearchParams(params) });

    let response = this.http.get<T>(url, { params: this.buildUrlSearchParams(params) })
      .pipe(catchError(this.serviceError));

    return response;
  }

    public getFull<T>(url: string): Observable<HttpResponse<T>> {
        return this.http.get<T>(url, { observe: 'response' });
    }

    public post<T>(url: string, data?: any, params?: any): Observable<T> {
      let response = this.http.post<T>(url, data, { params: params })
        .pipe(map(this.extractData), catchError(this.serviceError));            
        return response;
    }

    public put<T>(url: string, data?: any, params?: any): Observable<T> {
      let response = this.http.put<T>(url, data, { params: params })
        .pipe(map(this.extractData), catchError(this.serviceError));
      return response;
    }

   

  public delete<T>(url: string): Observable<T> {
    let response = this.http.delete<T>(url)
      .pipe(map(this.extractData), catchError(this.serviceError));
    return response;
  }

    private buildUrlSearchParams(params: any): HttpParams {
        let searchParams = new HttpParams();
        for (const key in params) {
            if (params.hasOwnProperty(key)) {
                searchParams= searchParams.append(key, params[key]);
            }
        }
        return searchParams;
    }

    private extractData(response: any) {
        if (response) // pode não ter resposta
            return response.data || {};
        return {};
    }

    private serviceError(error: Response | any) {
        //let errMsg: string;

        //if (error instanceof Response) {

        //    errMsg = `${error.status} - ${error.statusText || ''}`;
        //}
        //else {
        //    errMsg = error.message ? error.message : error.toString();
        //}
        //console.error(error);
      if (error && error.error && error.error.errors)
          return throwError(error.error.errors);
      return throwError(error);
      
    }



}
