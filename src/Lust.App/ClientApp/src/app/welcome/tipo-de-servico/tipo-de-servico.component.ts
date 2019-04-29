import { Component, OnInit } from '@angular/core';
import { fuseAnimations } from '@fuse/animations';
import { FormGroup, FormBuilder } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { FuseConfigService } from '@fuse/services/config.service';

@Component({
  selector: 'appc-tipo-de-servico',
  templateUrl: './tipo-de-servico.component.html',
    styleUrls: ['./tipo-de-servico.component.scss'],
    animations: fuseAnimations
})
export class TipoDeServicoComponent implements OnInit {

    opcaoForm: FormGroup;

    constructor(
        private router: Router,
        private formBuilder: FormBuilder,
        private route: ActivatedRoute,
        private fuseConfig: FuseConfigService
    ) {
        this.fuseConfig.setConfig({
            layout: {
                navigation: 'none',
                toolbar:'none'
                
            }
        });
    }

    ngOnInit() {

        this.opcaoForm = this.formBuilder.group({
            opcao: 'desfrutar',
            
        });
  }

    prosseguir() {

        if (this.opcaoForm.value.opcao === "oferecer")
            this.router.navigate(["caracteristicas"], { relativeTo: this.route });
        else
            this.router.navigate(["preferencias"], { relativeTo: this.route });

    }

}
