import { Component, OnInit, OnDestroy } from '@angular/core';

import { FuseConfigService } from '@fuse/services/config.service';



@Component({
    selector: 'appc-home',
    templateUrl: './home.component.html',
    styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {//, OnDestroy {

    onTokenReceved: any;

    constructor(                
        private fuseConfig: FuseConfigService        
    ) {
        this.fuseConfig.setConfig({
            layout: {
                navigation: 'none',
                toolbar: 'none',
                footer: 'none'
            }
        });
    }

    public ngOnInit() {
        
        //;
        //this.onTokenReceved = this.oauthService.events.subscribe(e => {
        //    this.checkLogado();
        //});
    }

    //ngOnDestroy(): void {
        
    //    this.onTokenReceved.unsubscribe();
    //}

    

}
