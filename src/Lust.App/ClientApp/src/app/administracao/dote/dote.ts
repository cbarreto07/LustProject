export class Dote {

  id: string;
  descricao: string;

  constructor(dote?) {
    dote = dote || { id: '', descricao:''};
    Object.assign(this, dote);
  }

}
