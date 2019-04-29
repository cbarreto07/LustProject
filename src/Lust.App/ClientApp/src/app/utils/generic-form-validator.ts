import { FormGroup } from '@angular/forms';

export class ServerError {
    public field: string;
    public message: string;
}

export class ServerErrorList {    
    public Named: { [key: string]: [string] } = {};
    public Generic: string[] = [];
}

export class GenericValidator {
    
    constructor(private validationMessages: { [key: string]: { [key: string]: string } }) {
        
    }

    processMessages(container: FormGroup): { [key: string]: [string] } {
        let messages = {};
        for (let controlKey in container.controls) {
            if (container.controls.hasOwnProperty(controlKey)) {
                let c = container.controls[controlKey];

                if (c instanceof FormGroup) {
                    let childMessages = this.processMessages(c);
                    Object.assign(messages, childMessages);
                } else {
                    if (this.validationMessages[controlKey]) {
                        messages[controlKey] = [];
                        if ((c.dirty || c.touched) && c.errors) {
                            Object.keys(c.errors).map(messageKey => {
                                if (this.validationMessages[controlKey][messageKey] && messages[controlKey].length == 0) { // só vai exibir um erro por vez
                                    messages[controlKey].push(this.validationMessages[controlKey][messageKey]);
                                }
                            });
                        }
                    }
                }
            }
        }
        return messages;
    }
    processServerErros(container: FormGroup, erros: ServerError[]): ServerErrorList {

        let campos: string[] = [];
        let messages = new ServerErrorList();

        for (let controlKey in container.controls) {
            if (container.controls.hasOwnProperty(controlKey)) {
                campos.push(controlKey);
            }
        }

        erros.forEach(function(erro) {
            if (campos.includes(erro.field)) {
                if (!messages.Named[erro.field]) {
                    messages.Named[erro.field] = [erro.message];
                }
                else {
                    messages.Named[erro.field].push(erro.message);
                }                
            } else {
                messages.Generic.push(erro.message);
            }
        });

        
        

        return messages;
    }

}
