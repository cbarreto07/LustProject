import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'statusAnalise'
})
export class StatusAnalisePipe implements PipeTransform {
  private generos = [
    'Não verificado',
    'Aprovado',
    'Reprovado'];

  transform(value: number): any {
    return this.generos[value];
  }

}
