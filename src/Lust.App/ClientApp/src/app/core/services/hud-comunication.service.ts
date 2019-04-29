import { Injectable,  OnInit, Inject } from '@angular/core';
import { HudComunicationEvent} from 'app/classes/events';
import { Observable } from 'rxjs/Observable';
import { Subject } from 'rxjs/Subject';
//import { HubConnectionBuilder} from '@aspnet/signalr';
import * as signalR from '@aspnet/signalr';
import { DataHudComunication } from 'app/classes/types';
import { AccountService } from 'app/core/services/account.service';
//const signalR = require("@aspnet/signalr");
@Injectable()
export class HudComunicationService {

    //public events: Observable<HudComunicationEvent>;
    public hubConnection: any;
    //private eventsSubject: Subject<HudComunicationEvent> = new Subject<HudComunicationEvent>();
    public onMenssagemRecebida = new Subject<any>();
    private isConected = false;
    private lastPosition = { latitude: 0, longitude: 0 };

    constructor( @Inject('BASE_URL') private baseUrl: string,
        private accountService: AccountService
    ) {
        //this.events = this.eventsSubject.asObservable();
    }

    public Connect() { 
        if (this.isConected) return;
            let option = {
                accessTokenFactory: () => {
                    return this.accountService.accessToken;
                }
                //,
                //transport : 1
            };
        
        this.hubConnection = new signalR.HubConnectionBuilder()
            .withUrl(`${this.baseUrl}chathub`, option)            
            .build();

           // this.hubConnection = new HubConnection(`${this.baseUrl}chathub`, option);

            //this.hubConnection.on('send', (data: any) => {
            //    //const received = `Received: ${data}`;
            //    //this.messages.push(received);
            //});

            this.hubConnection.on('message', (data: DataHudComunication) => {
                //const received = `Received: ${data}`;
                //this.messages.push(received);
                //this.onMenssagemRecebida.next(new HudComunicationEvent('mensagem_recebida', data));
                this.onMenssagemRecebida.next(data);
            });

            this.hubConnection.start()
                .then(() => {
                    this.isConected = true;
                    //this.eventsSubject.next(new HudComunicationEvent('hub_connection_started'));
                    //var watchId = navigator.geolocation.watchPosition(this.checkCoordinates.bind(this));
                })
                .catch(err => {
                    this.isConected = false;
                    console.log('Error while establishing connection: ' + err);
                    //this.eventsSubject.next(new HudComunicationEvent('error_while_establishing_connection', err));
                });


        
    }

  private checkCoordinates(position: any) {
    
      if (!(position.coords.latitude == this.lastPosition.latitude && position.coords.longitude == this.lastPosition.longitude)) {
        this.lastPosition.latitude = position.coords.latitude;
        this.lastPosition.longitude = position.coords.longitude;
        this.sendCoordinates(position.coords);    
      }
    }

    private sendCoordinates(coords:any): Promise<any> {
       
        return this.hubConnection.invoke('Position', coords);
    }

    public sendMessage(id: string, chatId:string, to: string, message: string): Promise<any> {
        let data = new DataHudComunication();
        data.time = new Date();   
        data.id = id;
        data.chatId = chatId;
        data.to= to;
        //data.from= this.accountService.user.jti;
        data.message = message;
        
        return this.hubConnection.invoke('message', data);
            //.then(() => {
            //    this.eventsSubject.next(new HudComunicationEvent('mensagem_enviada', data));
            //})
            //.catch(err => {
            //    console.log('Error ao enviar mensagem: ' + err);
            //    this.eventsSubject.next(new HudComunicationEvent('error_while_send_message', err));
            //});;        
    }




}
