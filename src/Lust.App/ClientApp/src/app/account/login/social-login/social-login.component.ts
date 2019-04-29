import { Component, Input } from '@angular/core';
import { filter, take, delay, first, tap, map, catchError } from 'rxjs/operators';
import { from } from 'rxjs/observable/from';

import { AppService } from '../../../app.service';
import { OAuthService } from 'app/angular-oauth2-oidc/oauth-service';
import { Observable } from 'rxjs/Observable';
import { Observer } from 'rxjs';

@Component({
    selector: 'appc-social-login',
    styleUrls: ['./social-login.component.scss'],
    templateUrl: './social-login.component.html'
})
export class SocialLoginComponent {
  @Input() activeLogins: ISocialLogins[];

 // socialLogins: Observable<any>;
  socialLogins: ISocialLogins[];
    constructor(
        private appService: AppService,
        private oAuthService: OAuthService,

    ) {

      this.socialLogins =[...this.appService.appData.loginProviders].map(login => {
        var a = 0;
        return {
          loginProvider: login,
          providerKey: login,
          providerDisplayName: login,
          active: this.activeLogins && this.isActive(login)
        };
      });
      //this.socialLogins = from([...this.appService.appData.loginProviders]).pipe(map(login => {
      //  var a = 0;
      //      return {
      //          loginProvider: login,
      //          providerKey: login,
      //          providerDisplayName: login,
      //          active: this.activeLogins && this.isActive(login)
      //      };
      //}));


    }

   
  


    public loginCss(login: string): string {
        if (login.toLowerCase() === 'microsoft') {
            return 'windows';
        }

        if (login.toLowerCase() === 'stackexchange') {
            return 'stack-exchange';
        }

        return `${login.toLowerCase()}`;
    }

    redirect(provider) {
        this.oAuthService.initImplicitFlow(null, { provider: provider });
    }

    isActive(login: string): boolean {
        return this.activeLogins.some(l => l.loginProvider === login);
    }

 
}
