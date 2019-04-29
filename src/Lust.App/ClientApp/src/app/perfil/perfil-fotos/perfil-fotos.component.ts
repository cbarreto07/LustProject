import { Component, OnInit } from '@angular/core';
import { Subscription } from 'rxjs';
import { PerfilService } from '../services/perfil.service';
import { fuseAnimations } from '@fuse/animations';
import { MatDialog } from '@angular/material';
import { DialogFotoComponent } from '../dialog-foto/dialog-foto.component';

@Component({
  selector: 'app-perfil-fotos',
  templateUrl: './perfil-fotos.component.html',
  styleUrls: ['./perfil-fotos.component.scss'],
  animations: fuseAnimations
})
export class PerfilFotosComponent implements OnInit {

  fotos: any = [];
  nome: string = "";
  onPerfilChanged: Subscription;

  constructor(
    private perfilService: PerfilService,
    public dialog: MatDialog
  ) { }

  ngOnInit() {
    this.onPerfilChanged = this.perfilService.onPerfilChanged
      .subscribe(perfil => {
        this.fotos = perfil.fotos;
        this.nome = perfil.nome;
      })
  }

  visualizar(foto: any) {
    let dialogRef = this.dialog.open(DialogFotoComponent, {
      //width: '100%',
      //height: '100%',
      maxWidth: '100%',
      maxHeight: '100%',
      panelClass: 'dialog-foto',
      data: { foto: foto, nome: this.nome }
    });


  }

}
