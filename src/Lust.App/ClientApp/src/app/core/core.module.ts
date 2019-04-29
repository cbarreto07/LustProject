import { NgModule, Optional, SkipSelf, ModuleWithProviders, ErrorHandler } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';

// App level components
//import { HeaderComponent } from './components/header/header.component';
//import { FooterComponent } from './components/footer/footer.component';
// App level services
import { AccountService, User } from './services/account.service';
import { ImageService } from './services/image.service';

import { DataService } from './services/data.service';
import { GlobalErrorHandler } from './services/global-error.service';
import { TimingInterceptor } from './services/interceptors/timing-interceptor';
import { AuthInterceptor } from './services/interceptors/auth-interceptor';

import { AuthGuard } from './guard/auth.guard';
import { HudComunicationService } from 'app/core/services/hud-comunication.service';


//import { TranslatePipe } from '../translate.pipe';
//import { SharedModule } from '../shared/shared.module';

@NgModule({
    declarations: [
        //HeaderComponent,
        //FooterComponent,
       // TranslatePipe,
      
        ],
    imports: [
        //SharedModule,
        CommonModule,
        FormsModule,
        ReactiveFormsModule,
        HttpClientModule,
        RouterModule,
        
    ],
    exports: [
        FormsModule,
        HttpClientModule,
        ReactiveFormsModule,
      RouterModule
        
        

        // Components
        //HeaderComponent,
        //FooterComponent,
        //TranslatePipe
    ],
    providers: []
})
export class CoreModule {
    // forRoot allows to override providers
    // https://angular.io/docs/ts/latest/guide/ngmodule.html#!#core-for-root
    public static forRoot(): ModuleWithProviders {
        return {
            ngModule: CoreModule,
            providers: [
                AuthGuard,
                AccountService,
                DataService,
                { provide: HTTP_INTERCEPTORS, useClass: AuthInterceptor, multi: true },
                { provide: HTTP_INTERCEPTORS, useClass: TimingInterceptor, multi: true },
                { provide: ErrorHandler, useClass: GlobalErrorHandler },
                HudComunicationService,
                
                ImageService

            ]
        };
    }
    constructor( @Optional() @SkipSelf() parentModule: CoreModule) {
        if (parentModule) {
            throw new Error('CoreModule is already loaded. Import it in the AppModule only');
        }
    }
}
