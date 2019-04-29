import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ActivatedRouteSnapshot, Resolve, RouterStateSnapshot } from '@angular/router';

import { Observable } from 'rxjs/Observable';
import { Subject } from 'rxjs/Subject';
import { BehaviorSubject } from 'rxjs/BehaviorSubject';

import { FuseUtils } from '@fuse/utils';
import { HudComunicationService } from 'app/core/services/hud-comunication.service';
import { DataHudComunication } from 'app/classes/types';
import { DataService } from 'app/core/services/data.service';

@Injectable()
export class ChatService implements Resolve<any>
{
    
    
    contatos: any[];
    chats: any[];
    user: any;


    onChatSelected = new BehaviorSubject<any>(null);
    onContactSelected = new BehaviorSubject<any>(null);

    onChatsUpdated = new Subject<any>();
    onUserUpdated = new Subject<any>();
    onContatosUserUpdated = new Subject<any>();
    onContactsUpdated = new Subject<any>();
    onMessageReceived = new Subject<any>();

    

    onLeftSidenavViewChanged = new Subject<any>();
    onRightSidenavViewChanged = new Subject<any>();

    constructor(
        private http: HttpClient,
        private hudComunicationService: HudComunicationService,        
        private dataService: DataService)
    {
    }

    /**
     * Get chat
     * @param contactId
     * @returns {Promise<any>}
     */
    getChat(clienteId)
    {
        const chatItem = this.chats.find((item) => {
            return item.cliente.id === clienteId;
        });

        /**
         * Create new chat, if it's not created yet.
         */
        if ( !chatItem )
        {
            this.createNewChat(clienteId).then((newChat) => {
                this.onChatSelected.next(newChat);
            });
            return;
        }

        this.onChatSelected.next(chatItem);
    }

    /**
     * Create New Chat
     * @param contactId
     * @returns {Promise<any>}
     */
    createNewChat(clienteId)
    {
        return new Promise((resolve, reject) => {

            const newChat = { clienteId: clienteId}

            this.dataService.post('api/chatDoCliente', newChat)
                .subscribe(
                result => {
                    this.chats.unshift(result); // insere o novo chat no inicio
                    this.onChatsUpdated.next(result);
                    resolve(result);
                },
                fail => { this.onError(fail) }
            );

        });
    }



    /**
     * Select Contact
     * @param contact
     */
    selectContato(contact)
    {
        this.onContactSelected.next(contact);
    }

    /**
     * Set user status
     * @param status
     */
    setUserStatus(status)
    {
        this.user.status = status;
    }

    /**
     * Update user data
     * @param userData
     */
    updateUserData(userData)
    {
        //this.http.post('api/chat-user/' + this.user.id, userData)
        //    .subscribe((response: any) => {
        //            this.user = userData;
        //        }
        //    );
    }

    /**
     * Update the chat dialog
     * @param chatId
     * @param dialog
     * @returns {Promise<any>}
     */
    //updateDialogo(chatId, dialog): Promise<any>
    //{
    //    return new Promise((resolve, reject) => {

    //        const newData = {
    //            id    : chatId,
    //            dialog: dialog
    //        };

    //        this.http.post('api/chat-chats/' + chatId, newData)
    //            .subscribe(updatedChat => {
    //                resolve(updatedChat);
    //            }, reject);
    //    });
    //}

    /**
     * The Chat App Main Resolver
     * @param {ActivatedRouteSnapshot} route
     * @param {RouterStateSnapshot} state
     * @returns {Observable<any> | Promise<any> | any}
     */
    resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<any> | Promise<any> | any
    {
        return new Promise((resolve, reject) => {

            //Promise.all([
            //    this.getContacts(),
            //    this.getChats(),
            //    this.getUser()
            //]).then(
            //    ([contacts, chats, user]) => {
            //        this.contacts = contacts;
            //        this.chats = chats;
            //        this.user = user;
            //        resolve();
            //    },
            //    reject
            //);

            this.hudComunicationService.Connect();

            //this.hudComunicationService.events.filter(e => e.type === 'mensagem_recebida').subscribe(e => {
            //    this.mensagemRecebida(e.info);

            //});

            this.hudComunicationService.onMenssagemRecebida
                .subscribe(message => {
                    this.mensagemRecebida(message);
                });

            Promise.all([
                this.getContatos(),
                this.getChats(),
                this.getUser()
            ]).then(
                ([contacts, chats, user]) => {
                    this.contatos = contacts;
                    this.chats = chats;
                    this.user = user;
                    this.user.avatar = 'assets/images/avatars/profile.jpg'; //TODO
                    resolve();
                },
                reject
            );



        });
    }

    /**
     * Get Contacts
     * @returns {Promise<any>}
     */
    getContatos(): Promise<any>
    {
        return new Promise((resolve, reject) => {
            this.dataService.get('api/Manage/Contatos')
                .subscribe(
                result => {
                    resolve(result)
                },
                fail => reject);
        });
    }

    /**
     * Get Chats
     * @returns {Promise<any>}
     */
    getChats(): Promise<any>
    {
        return new Promise((resolve, reject) => {
            this.dataService.get('api/chatDoCliente/')
                .subscribe(
                result => {
                    resolve(result);
                },
                fail => reject);
        });
    }

    /**
     * Get User
     * @returns {Promise<any>}
     */
    getUser(): Promise<any>
    {
        return new Promise((resolve, reject) => {
            this.dataService.get('api/Manage/UserInfo')
                .subscribe(
                result => {
                    resolve(result)
                },
                fail => reject);                        
        });
    }

    mensagemRecebida(mensagem: DataHudComunication) {

        //verifica se tem o chat
        const chat = this.chats.find((item) => {
            return item.cliente.id === mensagem.from;
        });

        if (chat) //se tem adiciona a mensagem ao chat
        {
            chat.unread++;
            chat.lastMessageTime = mensagem.time;
            const dialogo = {
                id: mensagem.id,
                who: mensagem.from,
                message: mensagem.message,
                time: mensagem.time
            };
            chat.dialogo.push(dialogo);   

            this.onMessageReceived.next(dialogo);

        } else //se não tiver baixa as informações do chat
        {
            this.dataService.get('api/chatDoCliente/' + mensagem.chatId)
                .subscribe(
                result => {
                    this.chats.unshift(result); // insere o novo chat no inicio
                },
                fail => { this.onError(fail) });
        }
        
    }

    SendMessage(chat: any, mensagem: any): Promise<any>{
        return this.hudComunicationService.sendMessage(mensagem.id, chat.id, chat.cliente.id, mensagem.message);
    }

    onError(fail: any) {
       

    }
}
