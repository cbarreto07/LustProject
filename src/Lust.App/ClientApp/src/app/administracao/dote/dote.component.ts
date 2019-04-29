import { Component, OnDestroy, OnInit, ViewEncapsulation } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatSnackBar, MatDialogRef, MatDialog } from '@angular/material';
import { Subscription } from 'rxjs';

import { fuseAnimations } from '@fuse/animations';

import { Location } from '@angular/common';
import { DoteService } from 'app/administracao/dote/dote.service';
import { Dote } from './dote';
import { ShowErrosDialogComponent } from 'app/shared/components/show-erros-dialog/show-erros-dialog.component';
import { GenericValidator } from 'app/utils/generic-form-validator';

@Component({
  selector: 'app-dote',
  templateUrl: './dote.component.html',
  styleUrls: ['./dote.component.scss'],
  encapsulation: ViewEncapsulation.None,
  animations: fuseAnimations
})
export class DoteComponent implements OnInit, OnDestroy {
  dote = new Dote();
  onDoteChanged: Subscription;
  pageType: string;
  doteForm: FormGroup;
  maxDate = new Date();

  genericValidator: GenericValidator;   
  validationMessages: { [key: string]: { [key: string]: string } };
  displayMessage: { [key: string]: [string] } = {};

  errorDialogRef: MatDialogRef<ShowErrosDialogComponent>;

  constructor(
    private doteService: DoteService,
    private formBuilder: FormBuilder,
    public snackBar: MatSnackBar,
    private location: Location,
    public dialog: MatDialog
  ) {

    this.validationMessages = {
      descricao: {
        required: 'A descrição é requerida.',
        minlength: 'A descrição precisa ter no mínimo 2 caracteres',
        maxlength: 'A descrição precisa ter no máximo 50 caracteres'
      }
    };

    this.genericValidator = new GenericValidator(this.validationMessages);

  }

  ngOnInit() {
    // Subscribe to update dote on changes
    this.onDoteChanged =
      this.doteService.onDoteChanged
        .subscribe(dote => {

          if (dote) {
            this.dote = dote;//new Dote(dote);
            this.pageType = 'edit';
          }
          else {
            this.pageType = 'new';
            this.dote = new Dote();
          }

          this.doteForm = this.createDoteForm();

          this.doteForm.valueChanges.subscribe(() => {
            this.displayMessage = this.genericValidator.processMessages(this.doteForm);
          });

        });
  }

  ngOnDestroy() {
    this.onDoteChanged.unsubscribe();
  }

  createDoteForm() {
    return this.formBuilder.group({
      id: [this.dote.id],
      descricao: [this.dote.descricao, [Validators.required, Validators.maxLength(50), Validators.minLength(2)]]
    });
  }

  saveDote() {
    const data = this.doteForm.getRawValue();
    this.doteService.saveDote(data)
      .then(salvo => {

        // Trigger the subscription with new data
        this.doteService.onDoteChanged.next(salvo);

        // Show the success message
        this.snackBar.open('Dote salvo', 'OK', {
          verticalPosition: 'top',
          duration: 2000
        });
      },
      error => this.showError(error));
  }

  addDote() {
    const data = this.doteForm.getRawValue();

    this.doteService.addDote(data)
      .then( adicionado => {

        // Trigger the subscription with new data
        this.doteService.onDoteChanged.next(adicionado);

        // Show the success message
        this.snackBar.open('Dote adicionado', 'OK', {
          verticalPosition: 'top',
          duration: 2000
        });

        // Change the location with new one
        this.location.go('administracao/dote/' + this.dote.id);
      },
      error => this.showError(error));
  }

  showError(erro) {
    

      this.errorDialogRef = this.dialog.open(ShowErrosDialogComponent);
      this.errorDialogRef.componentInstance.erros = erro;

    
  }

}
