import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { RouterModule } from '@angular/router';

import { MatInputModule, MatButtonModule, MatDividerModule, MatIconModule, MatTabsModule, MatFormFieldModule, MatButtonToggleModule, MatSnackBarModule, MatProgressSpinnerModule } from '@angular/material';

import { FuseSharedModule } from '@fuse/shared.module';

import { SharedModule } from '../shared/shared.module';

import { DesfrutarComponent } from './desfrutar/desfrutar.component';
import { PreferenciaComponent } from './preferencia/preferencia.component';
import { PreferenciaService } from './services/preferencia.service';

@NgModule({
  imports: [
      CommonModule,
      MatInputModule, MatButtonModule, MatDividerModule, MatIconModule, MatTabsModule, MatFormFieldModule, MatButtonToggleModule, MatSnackBarModule, MatProgressSpinnerModule,
      FuseSharedModule,
      SharedModule,
    RouterModule.forChild([      
        {
          path: '',
          component: DesfrutarComponent
        },
          {
              path: 'preferencias',
              component: PreferenciaComponent,
              resolve: {
                  data: PreferenciaService
              }
          }
          
      ])
  ],
    declarations: [DesfrutarComponent, PreferenciaComponent],
    providers: [PreferenciaService]
})
export class DesfrutarModule { }
