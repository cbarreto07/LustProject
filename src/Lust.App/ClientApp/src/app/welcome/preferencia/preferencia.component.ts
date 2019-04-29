import { Component, OnInit } from '@angular/core';
import { fuseAnimations } from '@fuse/animations';
import { FormGroup, FormBuilder } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { FuseConfigService } from '@fuse/services/config.service';
import { Preferencia } from 'app/classes/preferencia';
import { DataService } from 'app/core/services/data.service';
import { MatSnackBar } from '@angular/material';

@Component({
  selector: 'appc-preferencia',
  templateUrl: './preferencia.component.html',
    styleUrls: ['./preferencia.component.scss'],
    animations: fuseAnimations
})
export class PreferenciaComponent implements OnInit {

    preferenciaForm: FormGroup;
    
    constructor(
        private router: Router,
        private formBuilder: FormBuilder,
        private route: ActivatedRoute,
        private fuseConfig: FuseConfigService,
        private dataService: DataService,
        public snackBar: MatSnackBar,
    ) {
        this.fuseConfig.setConfig({
            layout: {
                navigation: 'none',
                toolbar: 'none'

            }
        });
    }

    ngOnInit() {

        this.preferenciaForm = this.formBuilder.group({
            mulher: [false],            
            homem: [false],            
            trans: [false],            
            casal: [false],            
            idade: [[18, 65]],
            distancia: [150],
            preco: [[0,5000]]
            
        });

    }
       
    prosseguir() {
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

        this.dataService.post<Preferencia>('api/manage/preferencias', pref)
            .subscribe((response: any) => {
                this.snackBar.open('Preferências Criadas', 'OK', {
                    verticalPosition: 'top',
                    duration: 2000
                });

                this.router.navigate(["quero-desfrutar"]);
            },this.reject);

    }

    reject() {

    }

}
