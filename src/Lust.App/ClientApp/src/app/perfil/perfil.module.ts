import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { GerenciarPerfilComponent } from './gerenciar-perfil/gerenciar-perfil.component';
import { ExibirPerfilComponent } from './exibir-perfil/exibir-perfil.component';
import { PerfilService } from './services/perfil.service';
import { GerenciarPerfilService } from './gerenciar-perfil/gerenciar-perfil.service';
import { FuseSharedModule } from '@fuse/shared.module';
import { AgmCoreModule } from '@agm/core';
import { SharedModule } from 'app/shared/shared.module';
import { RouterModule } from '@angular/router';

import {
  MatButtonModule,
  MatChipsModule,
  MatFormFieldModule,
  MatIconModule,
  MatInputModule,
  MatPaginatorModule,
  MatRippleModule,
  MatSelectModule,
  MatSortModule,
  MatTableModule,
  MatTabsModule,
  MatSnackBarModule,
  MatCheckboxModule,
  MatDatepickerModule,
  MatDialogModule,
  MatMenuModule,
  MatDividerModule,
  MatToolbarModule
} from '@angular/material';
import { PerfilFotosComponent } from './perfil-fotos/perfil-fotos.component';
import { PerfilSobreComponent } from './perfil-sobre/perfil-sobre.component';
import { DialogFotoComponent } from './dialog-foto/dialog-foto.component';

@NgModule({
  imports: [
    CommonModule,
    SharedModule,
    AgmCoreModule.forRoot({
      apiKey: 'AIzaSyAR0oRM_cgjLRKJ3tJIG4ylnBdKnKG3x_Y'
    }),
    FuseSharedModule,

    
    MatButtonModule,
    MatChipsModule,
    MatFormFieldModule,
    MatIconModule,
    MatInputModule,
    MatPaginatorModule,
    MatRippleModule,
    MatSelectModule,
    MatSortModule,
    MatTableModule,
    MatTabsModule,
    MatSnackBarModule,
    MatCheckboxModule,
    MatDatepickerModule,
    MatDialogModule,
    MatMenuModule,
    MatDividerModule,
    MatToolbarModule,
    
    RouterModule.forChild([
      { path: '', component: GerenciarPerfilComponent, resolve: { data: GerenciarPerfilService }},            
      { path: ':id', component: ExibirPerfilComponent, resolve: { data: PerfilService }}
      
    ])
  ],
  declarations: [GerenciarPerfilComponent, ExibirPerfilComponent, PerfilFotosComponent, PerfilSobreComponent, DialogFotoComponent],
  providers: [PerfilService, GerenciarPerfilService],
  entryComponents: [DialogFotoComponent]
})
export class PerfilModule { }
