import { Component, OnInit } from '@angular/core';
import { Subscription } from 'rxjs';
import { PerfilService } from '../services/perfil.service';
import { fuseAnimations } from '@fuse/animations';

@Component({
  selector: 'app-perfil-sobre',
  templateUrl: './perfil-sobre.component.html',
  styleUrls: ['./perfil-sobre.component.scss'],
  animations: fuseAnimations
})
export class PerfilSobreComponent implements OnInit {

  perfil: any = {};
  onPerfilChanged: Subscription;

  constructor(
    private perfilService: PerfilService
  ) { }

  ngOnInit() {
    this.onPerfilChanged = this.perfilService.onPerfilChanged
      .subscribe(perfil => {
        this.perfil = perfil;
      })
  }

  corrigeNota(nota) {
    return nota * 5;
  }

}
