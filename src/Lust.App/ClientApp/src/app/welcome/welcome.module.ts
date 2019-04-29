import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';

import { SharedModule } from '../shared/shared.module';

import { MatButtonModule, MatCheckboxModule, MatFormFieldModule, MatInputModule, MatDividerModule, MatSnackBarModule } from '@angular/material';
import { FuseSharedModule } from '@fuse/shared.module';

import { WelcomeComponent } from './welcome/welcome.component';
import { TipoDeServicoComponent } from './tipo-de-servico/tipo-de-servico.component';
import { CaracteristicaComponent } from './caracteristica/caracteristica.component';
import { PreferenciaComponent } from './preferencia/preferencia.component';

@NgModule({
  imports: [
      CommonModule,
      SharedModule,

      MatButtonModule, MatCheckboxModule, MatFormFieldModule, MatInputModule, MatDividerModule, MatSnackBarModule,

      FuseSharedModule,

      RouterModule.forChild([
          {
              path: '',
              component: TipoDeServicoComponent
          },
          {
              path: 'caracteristicas',
              component: CaracteristicaComponent
          },
          {
              path: 'preferencias',
              component: PreferenciaComponent

          }
      ])

  ],
  declarations: [WelcomeComponent, TipoDeServicoComponent, CaracteristicaComponent, PreferenciaComponent]
})
export class WelcomeModule { }
