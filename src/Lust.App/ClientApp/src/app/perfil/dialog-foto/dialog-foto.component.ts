
import { Component, OnInit, Inject, ViewEncapsulation } from '@angular/core';
import { MatDialog, MAT_DIALOG_DATA, MatDialogRef } from '@angular/material';

@Component({
  selector: 'app-dialog-foto',
  templateUrl: './dialog-foto.component.html',
  styleUrls: ['./dialog-foto.component.scss'],
  encapsulation: ViewEncapsulation.None
})
export class DialogFotoComponent implements OnInit {

  constructor(
    @Inject(MAT_DIALOG_DATA) public data: any,
    public dialogRef: MatDialogRef<DialogFotoComponent>
  ) { }

  ngOnInit() {
  }

}
