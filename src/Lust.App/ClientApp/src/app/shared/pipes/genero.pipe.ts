import { Pipe, PipeTransform } from '@angular/core';

// tslint:disable-next-line:use-pipe-transform-interface
@Pipe({
  name: 'genero'
})
export class GeneroPipe implements PipeTransform {
  private generos = [
    'Mulher',
    'Homem',
    'Casal',
    'Trans'];

    public transform(value: number) {
      return this.generos[value];
    }
}
