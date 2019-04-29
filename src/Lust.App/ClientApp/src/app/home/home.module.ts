import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HomeComponent } from './home/home.component';

import { RouterModule } from '@angular/router';
import { SharedModule } from '../shared/shared.module';

import { FuseSharedModule } from '@fuse/shared.module';
import { MatListModule, MatIconModule, MatButtonModule } from '@angular/material';

@NgModule({
  imports: [
      CommonModule,
      SharedModule,
      MatListModule,
      MatIconModule,
      MatButtonModule,
      FuseSharedModule,
      RouterModule.forChild([
          {
              path: '',
              component: HomeComponent
          }
      ])
  ],
  declarations: [HomeComponent]
})
export class HomeModule { }
