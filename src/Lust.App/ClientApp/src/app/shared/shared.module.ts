import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';


import { MatButtonModule, MatTooltipModule, MatIconModule, MatError, MatFormFieldModule, MatDialogModule, MatListModule } from '@angular/material';

// Components

import { DynamicFormComponent } from './forms/dynamic-form/dynamic-form.component';
import { DynamicFormControlComponent } from './forms/dynamic-form-control/dynamic-form-control.component';
import { NouisliderComponent } from './components/nouislider/nouislider.component';

// Pipes
import { UppercasePipe } from './pipes/uppercase.pipe';
import { TranslatePipe } from '../translate.pipe';
// Services
import { FormControlService } from './forms/form-control.service';

import { GuidImageDirective } from './directives/guid-image.directive';
import { StarRatingComponent } from './components/star-rating/star-rating.component';
import { GeneroPipe } from './pipes/genero.pipe';
import { DestinadoPipe } from './pipes/destinado.pipe';
import { StatusAnalisePipe } from './pipes/status-analise.pipe';
import { ShowErrosDialogComponent } from './components/show-erros-dialog/show-erros-dialog.component';
import { GuidImagePipe } from './pipes/guid-image.pipe';


@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    MatButtonModule,
    MatTooltipModule,
    MatIconModule,
    MatFormFieldModule,
    MatDialogModule,
    MatListModule
    // No need to export as these modules don't expose any components/directive etc'
  ],
  declarations: [
    
    DynamicFormComponent,
    DynamicFormControlComponent,
    
      UppercasePipe,
    TranslatePipe,
    GeneroPipe,
    DestinadoPipe,
      GuidImageDirective,

      NouisliderComponent,

      StarRatingComponent,

      StatusAnalisePipe,

      ShowErrosDialogComponent,

    GuidImagePipe
    
  ],
  exports: [
    // Modules
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    // Providers, Components, directive, pipes
    
    DynamicFormComponent,
    DynamicFormControlComponent,
    
      UppercasePipe,
    TranslatePipe, 
    StatusAnalisePipe,
    GeneroPipe,
    DestinadoPipe,
    GuidImageDirective,
    GuidImagePipe,
    NouisliderComponent,
    StarRatingComponent,
    ShowErrosDialogComponent
  ],
  entryComponents: [
    ShowErrosDialogComponent
  ],
  providers: [
    FormControlService
  ]

})
export class SharedModule { }
