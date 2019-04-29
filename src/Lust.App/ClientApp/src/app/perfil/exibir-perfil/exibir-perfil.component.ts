import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { Subscription } from 'rxjs';
import { PerfilService } from '../services/perfil.service';
import { fuseAnimations } from '@fuse/animations';
//import { DomSanitizer, SafeStyle } from '@angular/platform-browser';

@Component({
  selector: 'app-exibir-perfil',
  templateUrl: './exibir-perfil.component.html',
  styleUrls: ['./exibir-perfil.component.scss'],
  encapsulation: ViewEncapsulation.None,
  animations: fuseAnimations
})
export class ExibirPerfilComponent implements OnInit {
  perfil: any = { fotoDePerfilThumbnail : '/assets/images/backgrounds/dark-material-bg.jpg'};
  onPerfilChanged: Subscription;
  fotoDeCapa: string;
  
  constructor(
    private perfilService: PerfilService
  
  ) {
  
  }

  ngOnInit() {
    this.onPerfilChanged = this.perfilService.onPerfilChanged
      .subscribe(perfil => {
        this.perfil = perfil;
      })
    
  }

  changeBackground(): any {
    return { "background-image": "url('" + this.perfil.fotoDeCapaThumbnail +"')" };
  }

  

}
