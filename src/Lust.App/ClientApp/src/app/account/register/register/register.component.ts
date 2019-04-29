import { Component, OnInit, ElementRef, ViewChildren,  ViewContainerRef, AfterViewInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators, FormControlName, FormControl } from '@angular/forms';

import { FuseConfigService } from '@fuse/services/config.service';
import { fuseAnimations } from '@fuse/animations';
import { AccountService } from 'app/core/services/account.service';
import { Router, ActivatedRoute } from '@angular/router';

import { CustomValidators } from 'ng2-validation';

import { GenericValidator } from 'app/utils/generic-form-validator';

import { FuseSplashScreenService } from '@fuse/services/splash-screen.service';
import { DataService } from 'app/core/services/data.service';
import { Observable } from 'rxjs/Observable';
import { TermosECondicoesComponent } from 'app/account/register/termos-e-condicoes/termos-e-condicoes.component';
import { MatDialog } from '@angular/material';
import { OAuthService } from 'app/angular-oauth2-oidc/oauth-service';

@Component({
  selector: 'appc-register',
  templateUrl: './register.component.html',
    styleUrls: ['./register.component.scss'],
    animations: fuseAnimations
})
export class RegisterComponent implements OnInit {

    @ViewChildren(FormControlName, { read: ElementRef }) formInputElements: ElementRef[];

    registerForm: FormGroup;

    private genericValidator: GenericValidator;    
    public errors: any[] = [];
    validationMessages: { [key: string]: { [key: string]: string } };
    displayMessage: { [key: string]: [string] } = {};
  maxDate = new Date();

    constructor(
        private fuseConfig: FuseConfigService,
        private formBuilder: FormBuilder,
        private router: Router,
        
        private vcr: ViewContainerRef,
        private dataService: DataService,
        private oAuthService: OAuthService,
        private fuseSplashScreen: FuseSplashScreenService,
        private dialog: MatDialog,
        private route: ActivatedRoute,
        private accountService: AccountService
    ) {

        var d = new Date();
        var year = d.getFullYear();
        var month = d.getMonth();
        var day = d.getDate();
        this.maxDate  = new Date(year - 18, month, day);

        this.fuseConfig.setConfig({
            layout: {
                navigation: 'none',
                toolbar: 'none',
                footer: 'none'
            }
        });

        

        this.validationMessages = {
            nome: {
                required: 'O Nome é requerido.',
                minlength: 'O Nome precisa ter no mínimo 2 caracteres',
                maxlength: 'O Nome precisa ter no máximo 250 caracteres'
            },
            cpf: {
                required: 'Informe o CPF',
                rangeLength: 'CPF deve conter entre 10 e 11 caracteres'
            },
            cep: {
                required: 'Informe o CEP',
                rangeLength: 'CPF deve conter entre 8 e 9 caracteres'
            },
            email: {
                required: 'Informe o e-mail',
                email: 'Email invalido'
            },
            celular: {
                required: 'Informe o seu celular'
            },
            dataNascimento: {
                required: 'Informe a sua data de nascimenento'
            },
            password: {
                required: 'Informe a senha',
                minlength: 'A senha deve possuir no mínimo 6 caracteres'
            },
            passwordConfirm: {
                required: 'Informe a senha novamente',
                minlength: 'A senha deve possuir no mínimo 6 caracteres',
                equalTo: 'As senhas não conferem'
            }
        };

        this.genericValidator = new GenericValidator(this.validationMessages);

    }

    ngOnInit() {

        let password = new FormControl('', [Validators.required, Validators.minLength(6)]);
        let passwordConfirm = new FormControl('', [Validators.required, Validators.minLength(6), CustomValidators.equalTo(password)]);

        this.registerForm = this.formBuilder.group({
            nome: [this.accountService.ExternalNome, [Validators.required, Validators.maxLength(250), Validators.minLength(2)]],
            cpf: ['', [Validators.required, CustomValidators.rangeLength([10, 11])]],
            celular: ['', [Validators.required]],
            cep: ['', [Validators.required, CustomValidators.rangeLength([8, 9])]],
            dataNascimento: [this.maxDate.toISOString(), [Validators.required]],
            email: [this.accountService.ExternalEmail, [Validators.required, CustomValidators.email]],
            password: password,
            passwordConfirm: passwordConfirm,
            aceito:['']
        });

        //this.registerForm.controls.dataNascimento.disable()

        this.registerForm.valueChanges.subscribe(() => {
            this.displayMessage = this.genericValidator.processMessages(this.registerForm);
        });
    }

    //ngAfterViewInit(): void {
    //    let controlBlurs: Observable<any>[] = this.formInputElements
    //        .map((formControl: ElementRef) => Observable.fromEvent(formControl.nativeElement, 'blur'));

    //    Observable.merge(...controlBlurs).subscribe(value => {
    //        this.displayMessage = this.genericValidator.processMessages(this.registerForm);
    //    });
    //}

    registrar() {
        //this.displayMessage = {};
        //this.displayMessage.email = ["mensagem qualquer"];
        if (this.registerForm.valid && this.registerForm.dirty) {
            this.fuseSplashScreen.show();
            this.dataService.post('api/account/register', this.registerForm.value)
                .subscribe(
                result => { this.onSaveComplete(result) },
                fail => { this.onError(fail) }
                );

        }
    }

    onSaveComplete(response: any) {

        //let org = Object.assign({}, this.organizador, this.inscricaoForm.value);

        //registra e já faz o login

        this.oAuthService.fetchTokenUsingPasswordFlow(this.registerForm.value.email, this.registerForm.value.password)
            .then((x: any) => {
                //localStorage.setItem('id_token', x.id_token);

                this.errors = [];
                this.registerForm.reset();
                this.fuseSplashScreen.hide();
                this.oAuthService.setupAutomaticSilentRefresh();
                this.router.navigate(['bem-vindo']);

                //this.toastr.success('Registro realizado com sucesso!', 'Bem Vindo!!!', { dismiss: 'controlled' })
                //    .then((toast: Toast) => {
                //        setTimeout(() => {
                //            this.toastr.dismissToast(toast);
                //            this.router.navigate(['bem-vindo']);
                //            //this.router.navigate(['../registerconfirmation'], { relativeTo: this.route, queryParams: { emailConfirmed: true } });
                //        }, 3500);
                //    });
            })
            .catch((err) => {
                console.error('error logging in', err);
                
            });



    }

    onError(fail: any) {
        this.fuseSplashScreen.hide();
        
        //let serverError = this.genericValidator.processServerErros(this.registerForm, fail);
        //this.displayMessage = serverError.Named;
        //this.errors = serverError.Generic;
        let errors = [];
        fail.forEach(function(erro) {
            errors.push(erro.message);
        });
        this.errors = errors;

    }


    openDialog() {
        const dialogRef = this.dialog.open(TermosECondicoesComponent);

        //dialogRef.afterClosed().subscribe(result => {
        //    console.log(`Dialog result: ${result}`);
        //});
    }



    
}
