import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { SharedModule } from '../../shared/shared.module';
import { RegisterComponent } from './register/register.component';


import {
    MatButtonModule,
    MatCheckboxModule,
    MatFormFieldModule,
    MatInputModule,
    MatDatepickerModule,
    MatDialogModule
} from '@angular/material';
import { FuseSharedModule } from '@fuse/shared.module';
import { TermosECondicoesComponent } from './termos-e-condicoes/termos-e-condicoes.component';


@NgModule({
  imports: [
      CommonModule,
      SharedModule,

      MatButtonModule,
      MatCheckboxModule,
      MatFormFieldModule,
      MatInputModule,
      MatDatepickerModule,
      MatDialogModule,

      FuseSharedModule,

      RouterModule.forChild([
          {
              path: '',
              component: RegisterComponent
          }
      ])
  ],
    declarations: [RegisterComponent,  TermosECondicoesComponent],
    entryComponents: [TermosECondicoesComponent]
})
export class RegisterModule { }
