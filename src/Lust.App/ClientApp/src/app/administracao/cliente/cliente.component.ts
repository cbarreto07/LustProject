import { Component, OnDestroy, OnInit, ViewEncapsulation } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { MatSnackBar } from '@angular/material';
import { Subscription } from 'rxjs';

import { fuseAnimations } from '@fuse/animations';




import { Location } from '@angular/common';
import { ClienteService } from 'app/administracao/cliente/cliente.service';
import { Cliente } from './cliente';

@Component({
  selector: 'app-cliente',
  templateUrl: './cliente.component.html',
  styleUrls: ['./cliente.component.scss'],
  encapsulation: ViewEncapsulation.None,
  animations: fuseAnimations
})
export class ClienteComponent implements OnInit, OnDestroy {
  cliente = new Cliente();
  onClienteChanged: Subscription;
  pageType: string;
  clienteForm: FormGroup;
  maxDate = new Date();

  constructor(
    private clienteService: ClienteService,
    private formBuilder: FormBuilder,
    public snackBar: MatSnackBar,
    private location: Location
  ) {
  }

  ngOnInit() {
    // Subscribe to update cliente on changes
    this.onClienteChanged =
      this.clienteService.onClienteChanged
        .subscribe(cliente => {

          if (cliente) {
            this.cliente = cliente;//new Cliente(cliente);
            this.pageType = 'edit';
          }
          else {
            this.pageType = 'new';
            this.cliente = new Cliente();
          }

          this.clienteForm = this.createClienteForm();
        });
  }

  ngOnDestroy() {
    this.onClienteChanged.unsubscribe();
  }

  createClienteForm() {
    return this.formBuilder.group({
      id: [this.cliente.id],
      nome: [this.cliente.nome],
      email: [this.cliente.email],
      genero: [this.cliente.genero],
      celular: [this.cliente.celular],
      cpf: [this.cliente.cpf],
      dataNascimento: [this.cliente.dataNascimento],
      estaOferecendo: [this.cliente.estaOferecendo],
      estaDesfrutando: [this.cliente.estaDesfrutando],
      //latitude: [this.cliente.latitude],
      //longitude: [this.cliente.longitude],
      curtaDescricao: [this.cliente.curtaDescricao],
      longaDescricao: [this.cliente.longaDescricao],
      cep: [this.cliente.cep]
      //fotoDePerfil: [this.cliente.fotoDePerfil],
      //fotoDeCapa: [this.cliente.fotoDeCapa],
      /*nota: [this.cliente.nota],
      notaHigiene: [this.cliente.notaHigiene],
      notaPrazer: [this.cliente.notaPrazer],
      notaFidelidadeAsFotos: [this.cliente.notaFidelidadeAsFotos],
      notaEducacao: [this.cliente.notaEducacao],
      notaAmbiente: [this.cliente.notaAmbiente],
      notaPontualidade: [this.cliente.notaPontualidade]*/
      /*preferenciaMulher: [this.cliente.preferencia.mulher],
      preferenciaHomem: [this.cliente.preferencia.homem],
      preferenciaTrans: [this.cliente.preferencia.trans],
      preferenciaCasal: [this.cliente.preferencia.casal],
      preferenciaIdade: [[this.cliente.preferencia.idadeMinima, this.cliente.preferencia.idadeMaxima]],
      preferenciaDistancia: [this.cliente.preferencia.distancia],
      preferenciaPreco: [[this.cliente.preferencia.precoMinimo, this.cliente.preferencia.precoMaximo]]
      */
    });
  }

  saveCliente() {
    const data = this.clienteForm.getRawValue();    
    this.clienteService.saveCliente(data)
      .then(() => {

        // Trigger the subscription with new data
        this.clienteService.onClienteChanged.next(data);

        // Show the success message
        this.snackBar.open('Cliente salvo', 'OK', {
          verticalPosition: 'top',
          duration: 2000
        });
      });
  }

  addCliente() {
    const data = this.clienteForm.getRawValue();
    
    this.clienteService.addCliente(data)
      .then(() => {

        // Trigger the subscription with new data
        this.clienteService.onClienteChanged.next(data);

        // Show the success message
        this.snackBar.open('Cliente adicionado', 'OK', {
          verticalPosition: 'top',
          duration: 2000
        });

        // Change the location with new one
        this.location.go('administracao/cliente/' + this.cliente.id);
      });
  }
}
