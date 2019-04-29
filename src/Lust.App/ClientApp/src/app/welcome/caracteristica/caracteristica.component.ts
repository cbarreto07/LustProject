import { Component, OnInit } from '@angular/core';
import { fuseAnimations } from '@fuse/animations';
import { FormGroup, FormBuilder } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { FuseConfigService } from '@fuse/services/config.service';

@Component({
  selector: 'appc-caracteristica',
  templateUrl: './caracteristica.component.html',
    styleUrls: ['./caracteristica.component.scss'],
    animations: fuseAnimations
})
export class CaracteristicaComponent implements OnInit {

    caracteristicaForm: FormGroup;

    constructor(
        private router: Router,
        private formBuilder: FormBuilder,
        private route: ActivatedRoute,
        private fuseConfig: FuseConfigService
    ) {
        this.fuseConfig.setConfig({
            layout: {
                navigation: 'none',
                toolbar: 'none'

            }
        });
    }

    ngOnInit() {

        this.caracteristicaForm = this.formBuilder.group({
            altura: ['']
        });

    }

    prosseguir() {

        //this.router.navigate(["quero-desfrutar"]);
        var a = 0;

    }

}

