import { Component, OnInit, OnDestroy } from '@angular/core';
import { fuseAnimations } from '@fuse/animations';
import { FormGroup, FormBuilder } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { FuseConfigService } from '@fuse/services/config.service';
import { DataService } from 'app/core/services/data.service';

import { Subscription } from 'rxjs/Subscription';
import { PreferenciaService } from '../services/preferencia.service';
import { MatSnackBar } from '@angular/material';
import { Preferencia } from 'app/classes/preferencia';

@Component({
    selector: 'appc-preferencia',
    templateUrl: './preferencia.component.html',
    styleUrls: ['./preferencia.component.scss'],
    animations: fuseAnimations
})
export class PreferenciaComponent implements OnInit, OnDestroy {

    preferenciaForm: FormGroup;
    preferencia = new Preferencia();
    onPreferenciaChanged: Subscription;

    constructor(
        private router: Router,
        private formBuilder: FormBuilder,
        private route: ActivatedRoute,
        private fuseConfig: FuseConfigService,
        private preferenciaService: PreferenciaService,
        public snackBar: MatSnackBar,
    ) {
       
    }

    ngOnInit() {

        this.onPreferenciaChanged =
            this.preferenciaService.onPreferenciaChanged
                .subscribe(preferencia => {

                    this.preferencia = preferencia;
                    

                    this.preferenciaForm = this.creatPreferenciaForm();
                });

    }

    ngOnDestroy() {
        this.onPreferenciaChanged.unsubscribe();
    }

    creatPreferenciaForm() {
        return this.formBuilder.group({
            mulher: [this.preferencia.mulher],
            homem: [this.preferencia.homem],
            trans: [this.preferencia.trans],
            casal: [this.preferencia.casal],
            idade: [[this.preferencia.idadeMinima, this.preferencia.idadeMaxima]],
            distancia: [this.preferencia.distancia],
            preco: [[this.preferencia.precoMinimo, this.preferencia.precoMaximo]]

        });
    }

    save() {

        //this.router.navigate(["quero-desfrutar"]);
        const data = this.preferenciaForm.getRawValue();
        const pref = new Preferencia();
        pref.mulher = data.mulher;
        pref.trans = data.trans;
        pref.casal = data.casal;
        pref.homem = data.homem;
        pref.distancia = data.distancia;
        pref.idadeMinima = data.idade[0];
        pref.idadeMaxima = data.idade[1];
        pref.precoMinimo = data.preco[0];
        pref.precoMaximo = data.preco[1];

        this.preferenciaService.savePreferencia(pref)
            .then(() => {

                

                // Show the success message
                this.snackBar.open('Preferências atualizadas', 'OK', {
                    verticalPosition: 'top',
                    duration: 2000
                });

                this.router.navigate([".."], { relativeTo: this.route });
            });
    }
}
