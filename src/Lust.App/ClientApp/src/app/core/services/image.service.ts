import { Injectable } from '@angular/core';
import { DataService } from 'app/core/services/data.service';
import { Observable } from 'rxjs/Observable';

export type ImageProportions = 'p1x1' | 'P3x4' | 'p4x3' | 'p16x9' | 'p9x16' | 'original';
export type ImageSizes = 'thumbnail' | 'xs' | 'sm' | 'md' | 'lg' | 'xl' | 'xxl' | 'original';

@Injectable()
export class ImageService {

    private cache: { [key: string]: {} } = {};
    public random: number;

    constructor(private dataService: DataService) {
        this.random = (new Date()).getTime();
    }

    public forceRefresh() {
        this.random = (new Date()).getTime();
    }

    public get(id: string, proportion: ImageProportions, size: ImageSizes, putRefresh: boolean): Observable<any> {
        return new Observable(observer => {
            let url = 'api/Image/';
            if (proportion != 'original') {
                url += proportion + "/" + size + "/";
            }
            else if (size != 'original') {
                url +=  size + "/";
            }
            url += id;
            
            if (this.cache[url]) {                
                observer.next(this.cache[url] + (putRefresh ? '?r=' + this.random:'') );
                observer.complete();

            } else {

                let cache = this.cache;
                this.dataService.get(url)
                    .subscribe(
                    result => {
                        cache[url] = result;
                        observer.next(result + (putRefresh ? '?r=' + this.random : ''));
                        observer.complete();
                    },
                    fail => { observer.error(fail); observer.complete(); }
                    );

            }

        });


       
    }

}
