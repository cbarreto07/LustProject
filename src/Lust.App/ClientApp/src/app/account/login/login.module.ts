import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';

import { SharedModule } from '../../shared/shared.module';

import { MatButtonModule, MatCheckboxModule, MatFormFieldModule, MatInputModule } from '@angular/material';
import { FuseSharedModule } from '@fuse/shared.module';

import { LoginComponent } from './login/login.component';
import { SocialLoginComponent } from './social-login/social-login.component';


@NgModule({
  imports: [
      CommonModule,
      SharedModule,

      MatButtonModule,
      MatCheckboxModule,
      MatFormFieldModule,
      MatInputModule,

      FuseSharedModule,

      RouterModule.forChild([
          {
              path: '',
              component: LoginComponent
          }
      ])
  ],
    declarations: [LoginComponent,  SocialLoginComponent]
})
export class LoginModule { }
