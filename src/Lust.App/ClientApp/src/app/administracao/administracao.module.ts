import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { RouterModule } from '@angular/router';

import { CdkTableModule } from '@angular/cdk/table';

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
  MatMenuModule
} from '@angular/material';

import { NgxChartsModule } from '@swimlane/ngx-charts';
import { AgmCoreModule } from '@agm/core';

import { FuseSharedModule } from '@fuse/shared.module';
import { FuseWidgetModule } from '@fuse/components/widget/widget.module';
import { FuseConfirmDialogModule } from '@fuse/components';


import { SharedModule } from '../shared/shared.module';

import { DashboardComponent } from './dashboard/dashboard.component';
import { ClientesComponent } from './clientes/clientes.component';
import { ClienteComponent } from './cliente/cliente.component';
import { PlanoComponent } from './plano/plano.component';
import { PlanosComponent } from './planos/planos.component';
import { DoteComponent } from './dote/dote.component';
import { DotesComponent } from './dotes/dotes.component';
import { ValidaFotoComponent } from './valida-foto/valida-foto.component';
import { ValidaPerfilComponent } from './valida-perfil/valida-perfil.component';
import { ValidaVideoComponent } from './valida-video/valida-video.component';
import { ContatosComponent } from './contatos/contatos.component';
import { ContatoComponent } from './contato/contato.component';
import { AssinaturaComponent } from './assinatura/assinatura.component';
import { AssinaturasComponent } from './assinaturas/assinaturas.component';
import { ClientesService } from './clientes/clientes.service';
import { ClienteService } from './cliente/cliente.service';
import { DotesService } from './dotes/dotes.service';
import { DoteService } from './dote/dote.service';
import { PlanosService } from './planos/planos.service';
import { PlanoService } from './plano/plano.service';



@NgModule({
  imports: [
      CommonModule,
      SharedModule,

    CdkTableModule,
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

    NgxChartsModule,
    AgmCoreModule.forRoot({
      apiKey: 'AIzaSyAR0oRM_cgjLRKJ3tJIG4ylnBdKnKG3x_Y'
    }),

    FuseSharedModule,
    FuseWidgetModule,
    FuseConfirmDialogModule,

    RouterModule.forChild([
      { path: '', redirectTo:'dashboard' },
      { path: 'dashboard', component: DashboardComponent },
      { path: 'clientes', component: ClientesComponent, resolve: { data: ClientesService } },
      { path: 'cliente/:id', component: ClienteComponent, resolve: { data: ClienteService } },
      { path: 'dotes', component: DotesComponent, resolve: { data: DotesService }},
      { path: 'dote/:id', component: DoteComponent, resolve: { data: DoteService }},
      { path: 'planos', component: PlanosComponent, resolve: { data: PlanosService } },
      { path: 'plano/:id', component: PlanoComponent, resolve: { data: PlanoService } },
      { path: 'validar/perfil', component: ValidaPerfilComponent },
      { path: 'validar/foto', component: ValidaFotoComponent },
      { path: 'validar/video', component: ValidaVideoComponent },
      { path: 'assinaturas', component: AssinaturasComponent },
      { path: 'contatos', component: ContatosComponent },
    ])

  ],
    declarations: [
       
    DashboardComponent,
       
    ClientesComponent,
       
    ClienteComponent,
       
    PlanoComponent,
       
    PlanosComponent,
       
    DoteComponent,
       
    DotesComponent,
       
    ValidaFotoComponent,
       
    ValidaPerfilComponent,
       
    ValidaVideoComponent,
       
    ContatosComponent,
       
    ContatoComponent,
       
    AssinaturaComponent,
       
    AssinaturasComponent

      ],
  providers: [
    ClientesService,
    ClienteService,
    DotesService,
    DoteService,
    PlanosService,
    PlanoService
  ]
})

export class AdministracaoModule { }
