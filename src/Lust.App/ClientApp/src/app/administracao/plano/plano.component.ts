import { Component, OnDestroy, OnInit, ViewEncapsulation } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatSnackBar, MatDialogRef, MatDialog } from '@angular/material';
import { Subscription } from 'rxjs';

import { fuseAnimations } from '@fuse/animations';

import { Location } from '@angular/common';
import { PlanoService } from 'app/administracao/plano/plano.service';

import { ShowErrosDialogComponent } from 'app/shared/components/show-erros-dialog/show-erros-dialog.component';
import { GenericValidator } from 'app/utils/generic-form-validator';
import { Plano } from 'app/classes/plano';

@Component({
  selector: 'app-plano',
  templateUrl: './plano.component.html',
  styleUrls: ['./plano.component.scss'],
  encapsulation: ViewEncapsulation.None,
  animations: fuseAnimations
})
export class PlanoComponent implements OnInit, OnDestroy {
  plano = new Plano();
  onPlanoChanged: Subscription;
  pageType: string;
  planoForm: FormGroup;
  maxDate = new Date();

  genericValidator: GenericValidator;   
  validationMessages: { [key: string]: { [key: string]: string } };
  displayMessage: { [key: string]: [string] } = {};

  errorDialogRef: MatDialogRef<ShowErrosDialogComponent>;

  constructor(
    private planoService: PlanoService,
    private formBuilder: FormBuilder,
    public snackBar: MatSnackBar,
    private location: Location,
    public dialog: MatDialog
  ) {

    this.validationMessages = {
      titulo: {
        required: 'O título é obrigatório',
        minlength: 'O título precisa ter no mínimo 5 caracteres',
        maxlength: 'O título precisa ter no máximo 100 caracteres'
      },
      subTitulo: {
        required: 'O subtítulo é obrigatório',
        minlength: 'O subtítulo precisa ter no mínimo 10 caracteres',
        maxlength: 'O subtítulo precisa ter no máximo 100 caracteres'
      },
      descricao: {
        required: 'A descrição é requerida.',
        minlength: 'A descrição precisa ter no mínimo 50 caracteres',
        maxlength: 'A descrição precisa ter no máximo 255 caracteres'
      },
      ordem: {
        required: 'A ordem é requerida.',
        min: 'O valor mínimo é 1'        
      },
      quantidadeMeses: {
        required: 'A quantidade de meses é requerida.',
        min: 'O valor mínimo é 1'
      },
      valor: {
        required: 'O valor é requerido.'
      },
      destinado: {
        required: 'Destinado é requerido.'
      }


    };

    this.genericValidator = new GenericValidator(this.validationMessages);

  }

  ngOnInit() {
    // Subscribe to update plano on changes
    this.onPlanoChanged =
      this.planoService.onPlanoChanged
        .subscribe(plano => {

          if (plano) {
            this.plano = plano;//new Plano(plano);
            this.pageType = 'edit';
          }
          else {
            this.pageType = 'new';
            this.plano = new Plano();
          }

          this.planoForm = this.createPlanoForm();

          this.planoForm.valueChanges.subscribe(() => {
            this.displayMessage = this.genericValidator.processMessages(this.planoForm);
          });

        });
  }

  ngOnDestroy() {
    this.onPlanoChanged.unsubscribe();
  }

  createPlanoForm() {
    return this.formBuilder.group({
      id: [this.plano.id],
      titulo: [this.plano.titulo, [Validators.required, Validators.maxLength(100), Validators.minLength(5)]],
      subTitulo: [this.plano.subTitulo, [Validators.required, Validators.maxLength(100), Validators.minLength(10)]],
      descricao: [this.plano.descricao, [Validators.required, Validators.maxLength(255), Validators.minLength(50)]],
      ordem: [this.plano.ordem, [Validators.required, Validators.min(1)]],
      quantidadeMeses: [this.plano.quantidadeMeses, [Validators.required, Validators.min(1)]],
      valor: [this.plano.valor, [Validators.required]],
      destinado: [this.plano.destinado.toString(), [Validators.required]]
    });
  }

  savePlano() {
    const data = this.planoForm.getRawValue();
    this.planoService.savePlano(data)
      .then(salvo => {

        // Trigger the subscription with new data
        this.planoService.onPlanoChanged.next(salvo);

        // Show the success message
        this.snackBar.open('Plano salvo', 'OK', {
          verticalPosition: 'top',
          duration: 2000
        });
      },
      error => this.showError(error));
  }

  addPlano() {
    const data = this.planoForm.getRawValue();

    this.planoService.addPlano(data)
      .then( adicionado => {

        // Trigger the subscription with new data
        this.planoService.onPlanoChanged.next(adicionado);

        // Show the success message
        this.snackBar.open('Plano adicionado', 'OK', {
          verticalPosition: 'top',
          duration: 2000
        });

        // Change the location with new one
        this.location.go('administracao/plano/' + this.plano.id);
      },
      error => this.showError(error));
  }

  showError(erro) {
    

      this.errorDialogRef = this.dialog.open(ShowErrosDialogComponent);
      this.errorDialogRef.componentInstance.erros = erro;

    
  }

}
