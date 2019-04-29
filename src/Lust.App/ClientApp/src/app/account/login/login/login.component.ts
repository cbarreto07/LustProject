import { Component, OnInit,ElementRef, ViewChildren, ViewContainerRef } from '@angular/core';
import { FormBuilder, FormGroup, Validators, FormControlName } from '@angular/forms';

import { FuseConfigService } from '@fuse/services/config.service';
import { fuseAnimations } from '@fuse/animations';
import { AccountService } from 'app/core/services/account.service';
import { Router } from '@angular/router';

import { CustomValidators } from 'ng2-validation';
import { GenericValidator } from 'app/utils/generic-form-validator';
import { OAuthService } from 'app/angular-oauth2-oidc/oauth-service';


@Component({
  selector: 'appc-login',
  templateUrl: './login.component.html',
    styleUrls: ['./login.component.scss'],
    animations: fuseAnimations
})
export class LoginComponent implements OnInit {

    @ViewChildren(FormControlName, { read: ElementRef }) formInputElements: ElementRef[];

    loginForm: FormGroup;
    
    private genericValidator: GenericValidator;    
    public errors: any[] = [];
    validationMessages: { [key: string]: { [key: string]: string } };
    displayMessage: { [key: string]: [string] } = {};

    constructor(
        private fuseConfig: FuseConfigService,
        private formBuilder: FormBuilder,
        private accountService: AccountService,
        private router: Router,
        
        public oAuthService: OAuthService,
        vcr: ViewContainerRef
    ) {
        this.fuseConfig.setConfig({
            layout: {
                navigation: 'none',
                toolbar: 'none',
                footer: 'none'
            }
        });

        
        
        this.validationMessages = {
            email: {
                required: 'Informe o e-mail',
                email: 'Email invalido'
            },
            password: {
                required: 'Informe a senha',
                minlength: 'A senha deve possuir no mínimo 6 caracteres'
            }
        };

        this.genericValidator = new GenericValidator(this.validationMessages);
    }

    ngOnInit() {

        //if (this.accountService.isLoggedIn) { // se ja estiver logado não ve a tela inicial
        //    var page = ""
        //    var user = this.accountService.user;

        //    if (user.role.indexOf("Admin") >= 0)
        //        page = "administracao"
        //    else if (user.quer_oferecer)
        //        page = "quero-oferecer"
        //    else if (user.quer_desfrutar)
        //        page = "quero-desfrutar";
        //    else page = "bem-vindo"
        //    this.router.navigate([page]);
        //}

        this.loginForm = this.formBuilder.group({
            email: ['', [Validators.required, Validators.email]],
            password: ['', [Validators.required]]
        });

        this.loginForm.valueChanges.subscribe(() => {
            //this.onFormValuesChanged();
            this.displayMessage = this.genericValidator.processMessages(this.loginForm);
        });
    }

    realizarLogin() {

        if (this.loginForm.valid && this.loginForm.dirty) {

          this.oAuthService.fetchTokenUsingPasswordFlow(this.loginForm.value.email, this.loginForm.value.password)
            .then(
              (resolve: any) => {
                localStorage.setItem('id_token', resolve.id_token);
                this.oAuthService.setupAutomaticSilentRefresh();
                var page = ""
                var user = this.accountService.userByIdToken(resolve.id_token);

                if (user.role.indexOf("Admin") >= 0)
                  page = "administracao"
                else if (user.estaOferecendo)
                  page = "perfil"
                else if (user.estaDesfrutando)
                  page = "quero-desfrutar";
                else page = "bem-vindo"
                this.router.navigate([page]);


                //this.toastr.success('Login realizado com sucesso!', 'Bem Vindo!!!', { dismiss: 'controlled' })
                //    .then((toast: Toast) => {
                //        setTimeout(() => {
                //            this.toastr.dismissToast(toast);
                //            this.router.navigate([page]);
                //        }, 1500);
                //    });
              },
              reject => {
                console.error('error logging in', reject);
              }


            );
              
        }
    }

  

    

    //onFormValuesChanged() {
    //    for (const field in this.errors) {
    //        if (!this.errors.hasOwnProperty(field)) {
    //            continue;
    //        }

    //        // Clear previous errors
    //        this.errors[field] = {};

    //        // Get the control
    //        const control = this.loginForm.get(field);

    //        if (control && control.dirty && !control.valid) {
    //            // this.loginFormErrors[field] = control.errors;

    //        }
    //    }
    //}
}
