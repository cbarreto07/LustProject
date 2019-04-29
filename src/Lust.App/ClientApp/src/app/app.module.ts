

import { BrowserModule, BrowserTransferStateModule } from '@angular/platform-browser';
import { NgModule, APP_INITIALIZER, LOCALE_ID } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { RouterModule, Routes } from '@angular/router';
import { TranslateModule } from '@ngx-translate/core';
import 'hammerjs';

import { OAuthModule } from './angular-oauth2-oidc/angular-oauth-oidic.module';

import { MAT_MOMENT_DATE_FORMATS, MomentDateAdapter } from '@angular/material-moment-adapter';
import { DateAdapter, MAT_DATE_FORMATS, MAT_DATE_LOCALE } from '@angular/material/core';


import { FuseModule } from '@fuse/fuse.module';
import { FuseSharedModule } from '@fuse/shared.module';

import { fuseConfig } from './fuse-config';


import { FuseMainModule } from './main/main.module';
import { CoreModule } from './core/core.module';

import { AppComponent } from './app.component';
import { AppService } from './app.service';
import { AuthGuard } from './core/guard/auth.guard'

export function appServiceFactory(appService: AppService): Function {
  return () => appService.getAppData();
}

import { registerLocaleData } from '@angular/common';
import localePt from '@angular/common/locales/pt';
import localePtExtra from '@angular/common/locales/extra/pt';

registerLocaleData(localePt, 'pt', localePtExtra);

const appRoutes: Routes = [
  //{ path: '', component: HomeComponent, pathMatch: 'full', data: { state: 'home' } },
  { path: '', loadChildren: './home/home.module#HomeModule' },
  { path: 'login', loadChildren: './account/login/login.module#LoginModule' },
  { path: 'registro', loadChildren: './account/register/register.module#RegisterModule' },
  //{ path: 'login', loadChildren: './account/+login/login.module#LoginModule' },
  //{ path: 'register', loadChildren: './account/+register/register.module#RegisterModule' },
  { path: 'administracao', loadChildren: './administracao/administracao.module#AdministracaoModule', canActivate: [AuthGuard], data: { role: 'admin' }, canLoad: [AuthGuard] },
  { path: 'quero-desfrutar', loadChildren: './desfrutar/desfrutar.module#DesfrutarModule', canActivate: [AuthGuard] },
  { path: 'perfil', loadChildren: './perfil/perfil.module#PerfilModule', canActivate: [AuthGuard] },
  { path: 'planos', loadChildren: './planos/planos.module#PlanosModule', canActivate: [AuthGuard] },

  { path: 'quero-oferecer', loadChildren: './oferecer/oferecer.module#OferecerModule', canActivate: [AuthGuard] },
  { path: 'bem-vindo', loadChildren: './welcome/welcome.module#WelcomeModule', canActivate: [AuthGuard] },
  //{ path: 'createaccount', loadChildren: './account/+create/create.module#CreateAccountModule' },
  //{ path: 'profile', loadChildren: './account/+profile/profile.module#ProfileModule' },
  { path: 'chat', loadChildren: './chat/chat.module#ChatModule', canActivate: [AuthGuard] },
  //{ path: 'chat-antigo', loadChildren: './+chat/chat.module#ChatModule', canActivate: [AuthGuard] },
  { path: '**', redirectTo: '' }

];



@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    BrowserTransferStateModule,
    HttpClientModule,
    RouterModule.forRoot(appRoutes),
    TranslateModule.forRoot(),

    // Fuse Main and Shared modules
    FuseModule.forRoot(fuseConfig),
    FuseSharedModule,
    FuseMainModule,

    //lust
    CoreModule.forRoot(),
    OAuthModule.forRoot(),
    

    
  ],
  providers: [
    AppService,
    { provide: APP_INITIALIZER, useFactory: appServiceFactory, deps: [AppService], multi: true },
    //{ provide: PERFECT_SCROLLBAR_CONFIG, useValue: DEFAULT_PERFECT_SCROLLBAR_CONFIG }
    { provide: MAT_DATE_LOCALE, useValue: 'pt-BR' },
    { provide: LOCALE_ID, useValue: 'pt' },
    { provide: DateAdapter, useClass: MomentDateAdapter, deps: [MAT_DATE_LOCALE] },
    { provide: MAT_DATE_FORMATS, useValue: MAT_MOMENT_DATE_FORMATS },
  ],
  exports: [
    CoreModule
  ],
  bootstrap: [
    AppComponent
  ]
})
export class AppModule {
}
