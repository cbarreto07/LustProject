import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, CanLoad, Router, Route } from '@angular/router';
import { Observable } from 'rxjs/Observable';


import { AccountService } from '../services/account.service';
import { HudComunicationService } from '../services/hud-comunication.service';

@Injectable()
export class AuthGuard implements CanActivate, CanLoad {

    constructor(private accountService: AccountService, private router: Router,private hudComunicationService : HudComunicationService) { }

    canLoad(route: Route): boolean | Observable<boolean> | Promise<boolean> {
        //return false;
        
        let isLoggedIn = this.accountService.isLoggedIn;

        if (route.path == "administracao" && isLoggedIn) {            

            isLoggedIn = this.accountService.user.role.indexOf("Admin") >= 0;
        }

        if (!isLoggedIn)
            this.router.navigate(['/login']);

        this.hudComunicationService.Connect();
        return isLoggedIn;
    }

  canActivate(
    next: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean> | Promise<boolean> | boolean {

      let isLoggedIn = this.accountService.isLoggedIn;
      
      if (next.data.role ==="admin" && isLoggedIn) {
          isLoggedIn= this.accountService.user.role.indexOf("Admin") >= 0;
      }

      if (!isLoggedIn)
          this.router.navigate(['/login']);

      this.hudComunicationService.Connect();
      return isLoggedIn;
      
  }

}
