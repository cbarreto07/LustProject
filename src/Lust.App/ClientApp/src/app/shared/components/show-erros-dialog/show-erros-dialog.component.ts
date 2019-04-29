import { Component } from '@angular/core';
import { MatDialogRef } from '@angular/material';

@Component({
  selector: 'app-show-erros-dialog',
  templateUrl: './show-erros-dialog.component.html',
  styleUrls: ['./show-erros-dialog.component.scss']
})
export class ShowErrosDialogComponent  {

  public erros: any[] =[];
  //_erros: { [key: string]: [string] } = {};
  //keys: string[] = [];
  //public set erros(erros: any) {

    
  //  for (let i = 0; i < erros.length; i++) {
  //    let erro = erros[i];
  //    if (this._erros[erro.field]) {
  //      this._erros[erro.field].push(erro.message);
  //      }
  //    else {
  //      this.keys.push(erro.field);
  //      this._erros[erro.field] = [erro.message];
        
  //      }
      
  //  };
  //}

  constructor(public dialogRef: MatDialogRef<ShowErrosDialogComponent>) { }

 

}
