export class Cliente {

  id: string;
  nome: string;
  email: string;
  genero: number;
  celular: string;
  cpf: string;
  dataNascimento: Date;
  estaOferecendo: boolean;
  estaDesfrutando: boolean;
  latitude: number;
  longitude: number;
  curtaDescricao: string;
  longaDescricao: string;
  cep: string;
  fotoDePerfil: string;
  fotoDeCapa: string;
  nota: number;
  notaHigiene: number;
  notaPrazer: number;
  notaFidelidadeAsFotos: number;
  notaEducacao: number;
  notaAmbiente: number;
  notaPontualidade: number;
  caracteristica: {
    valor30min: number;
    valor1Hora: number;
    valor2horas: number;
    valorPernoite: number;
    localProprio: boolean;
    homem: boolean;
    mulher: boolean;
    casal: boolean;
    trans: boolean;
  };
  preferencia: {
    id: string;
    distancia: number;
    idadeMinima: number;
    idadeMaxima: number;
    precoMinimo: number;
    precoMaximo: number;
    homem: boolean;
    mulher: boolean;
    casal: boolean;
    trans: boolean;
  }
  fotos: Foto[];

  constructor(cliente?) {

  }
}

export class Foto {  
  id: string;
  descricao: string;  
  motivoReprovado: string;
  ordem: number;
  statusAnalise: number;
}
