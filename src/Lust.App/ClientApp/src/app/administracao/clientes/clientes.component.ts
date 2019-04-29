import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { MatPaginator, MatSort } from '@angular/material';
import { DataSource } from '@angular/cdk/collections';

import { merge, Observable, BehaviorSubject, fromEvent } from 'rxjs';
import { debounceTime, distinctUntilChanged, map } from 'rxjs/operators';

import { fuseAnimations } from '@fuse/animations';
import { FuseUtils } from '@fuse/utils';
import { ClientesService } from './clientes.service';



@Component({
  selector: 'app-clientes',
  templateUrl: './clientes.component.html',
  styleUrls: ['./clientes.component.scss'],
  animations: fuseAnimations
})
export class ClientesComponent implements OnInit {
  dataSource: FilesDataSource | null;
  displayedColumns = ['nome', 'email', 'cpf', 'estaDesfrutando', 'estaOferecendo','genero' ];

  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild('filter') filter: ElementRef;
  @ViewChild(MatSort) sort: MatSort;

  constructor(
    private clientesService: ClientesService
  ) {
  }

  ngOnInit() {
    this.dataSource = new FilesDataSource(this.clientesService, this.paginator, this.sort);

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
}

export class FilesDataSource extends DataSource<any>
{
  _filterChange = new BehaviorSubject('');
  //_filteredDataChange = new BehaviorSubject('');
  
  get totalRows(): number {
    return this.clientesService.totalRows;
  }

  get filter(): string {
    return this._filterChange.value;
  }

  set filter(filter: string) {
    this._paginator.pageIndex = 0;
    this._filterChange.next(filter);
  }

  constructor(
    private clientesService: ClientesService,
    private _paginator: MatPaginator,
    private _sort: MatSort
  ) {
    super();
    
    //this.filteredData = this.productsService.products;

    const displayDataChanges = [      
      _paginator.page,
      this._filterChange,
      _sort.sortChange
    ];

    merge(...displayDataChanges).subscribe(q => {
      const skip = this._paginator.pageIndex * this._paginator.pageSize;
      const take = this._paginator.pageSize;
      if (skip !== undefined && take !== undefined) {
        const query = this.filter;
        return this.clientesService.getClientes(skip, take, query, _sort.active, _sort.direction);
      }
    });

    this.clientesService.onTotalRowsChanged.subscribe(q => {
      this._paginator.pageIndex = 0;
    })

  }

  /** Connect function called by the table to retrieve one stream containing the data to render. */
  connect(): Observable<any[]> {
   
    return this.clientesService.onClientesChanged.pipe(map(() => {
      
      return this.clientesService.clientes;
    }
    ));
  }


  disconnect() {
  }
}
