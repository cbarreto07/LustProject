
export type EventType =
    'mensagem_recebida'
    | 'mensagem_enviada'
    | 'notificacao_recebida'
    | 'notificacao_enviada'
    | 'evento_servidor_recebido'
    | 'evento_servidor_enviado'
    | 'hub_connection_started'
    | 'error_while_establishing_connection'
    | 'error_while_send_message'
    ;

export class DataHudComunication {
    public id: string;
    public chatId: string;
    public message: string;
    public from: string;
    public to: string;
    public time: Date;
}