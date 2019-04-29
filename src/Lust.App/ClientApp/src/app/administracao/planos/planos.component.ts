import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { MatPaginator, MatSort, MatSnackBar, MatDialogRef, MatDialog } from '@angular/material';
import { DataSource } from '@angular/cdk/collections';

import { merge, Observable, BehaviorSubject, fromEvent } from 'rxjs';
import { debounceTime, distinctUntilChanged, map } from 'rxjs/operators';

import { fuseAnimations } from '@fuse/animations';
import { FuseUtils } from '@fuse/utils';
import { PlanosService } from './planos.service';
import { FuseConfirmDialogComponent } from '@fuse/components/confirm-dialog/confirm-dialog.component';
import { ShowErrosDialogComponent } from 'app/shared/components/show-erros-dialog/show-erros-dialog.component';



@Component({
  selector: 'app-planos',
  templateUrl: './planos.component.html',
  styleUrls: ['./planos.component.scss'],
  animations: fuseAnimations
})

export class PlanosComponent implements OnInit {
  dataSource: FilesDataSource | null;
  displayedColumns = ['descricao', 'destinado', 'ordem', 'quantidadeMeses','valor','buttons'];

  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild('filter') filter: ElementRef;
  @ViewChild(MatSort) sort: MatSort;

  confirmDialogRef: MatDialogRef<FuseConfirmDialogComponent>;
  errorDialogRef: MatDialogRef<ShowErrosDialogComponent>;

  constructor(
    private planosService: PlanosService,
    public snackBar: MatSnackBar,
    public dialog: MatDialog
  )
  { }

  ngOnInit() {
    this.dataSource = new FilesDataSource(this.planosService, this.paginator, this.sort);

    fromEvent(this.filter.nativeElement, 'keyup').pipe(
      debounceTime(150),
      distinctUntilChanged()
    ).subscribe(() => {
      if (!this.dataSource) {
        return;
      }

      this.dataSource.filter = this.filter.nativeElement.value;
    });
  }

  deletePlano(plano) {

    this.confirmDialogRef = this.dialog.open(FuseConfirmDialogComponent, {
      disableClose: false
    });

    this.confirmDialogRef.componentInstance.confirmMessage = 'Tem certeza que quer deletar?';

    this.confirmDialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.planosService.deletePlano(plano)
          .then(deletado => {
            this.snackBar.open('Plano deletado', 'OK', {
              verticalPosition: 'top',
              duration: 2000
            });
          },
          erro => {

            this.errorDialogRef = this.dialog.open(ShowErrosDialogComponent);
            this.errorDialogRef.componentInstance.erros = erro;
            
            }
          )
      }
      this.confirmDialogRef = null;
    });


    
  }

}

export class FilesDataSource extends DataSource<any>
{
  _filterChange = new BehaviorSubject('');
  
  get totalRows(): number {
    return this.planosService.totalRows;
  }

  get filter(): string {
    return this._filterChange.value;
  }

  set filter(filter: string) {
    this._paginator.pageIndex = 0;
    this._filterChange.next(filter);
  }

  constructor(
    private planosService: PlanosService,
    private _paginator: MatPaginator,
    private _sort: MatSort
  ) {
    super();


    const displayDataChanges = [
      _paginator.page,
      this._filterChange,
      _sort.sortChange,
      this.planosService.onDelete
    ];

    merge(...displayDataChanges).subscribe(q => {
      const skip = this._paginator.pageIndex * this._paginator.pageSize;
      const take = this._paginator.pageSize;
      if (skip !== undefined && take !== undefined) {
        const query = this.filter;
        return this.planosService.getPlanos(skip, take, query, _sort.active, _sort.direction);
      }
    });

    this.planosService.onTotalRowsChanged.subscribe(q => {
      this._paginator.pageIndex = 0;
    })

  }

  /** Connect function called by the table to retrieve one stream containing the data to render. */
  connect(): Observable<any[]> {

    return this.planosService.onPlanosChanged.pipe(map(() => {

      return this.planosService.planos;
    }
    ));
  }


  disconnect() {
  }
}
