import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';

import { SharedModule } from '../shared/shared.module';

import { OferecerComponent } from './oferecer/oferecer.component';

@NgModule({
  imports: [
      CommonModule,
      SharedModule,
      RouterModule.forChild([
          {
              path: '',
              component: OferecerComponent
              //children: [
              //    {
              //        path: '',
              //        component: TipoDeServicoComponent
              //    },
              //    {
              //        path: 'caracteristicas',
              //        component: CaracteristicaComponent
              //    },
              //    {
              //        path: 'preferencias',
              //        component: PreferenciaComponent
              //    }
              //]
          }
      ])
  ],
  declarations: [OferecerComponent]
})
export class OferecerModule { }
