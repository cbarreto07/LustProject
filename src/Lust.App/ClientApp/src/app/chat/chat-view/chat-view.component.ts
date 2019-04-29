import { AfterViewInit, Component, OnInit, ViewChild, ViewChildren } from '@angular/core';
import { NgForm } from '@angular/forms';

import { FusePerfectScrollbarDirective } from '@fuse/directives/fuse-perfect-scrollbar/fuse-perfect-scrollbar.directive';
import * as uuidv4 from 'uuid/v4';
import { ChatService } from '../services/chat.service';

@Component({
    selector   : 'fuse-chat-view',
    templateUrl: './chat-view.component.html',
    styleUrls  : ['./chat-view.component.scss']
})
export class FuseChatViewComponent implements OnInit, AfterViewInit
{
    user: any;
    chat: any;
    dialogo: any;
    contato: any;
    replyInput: any;
    selectedChat: any;
    @ViewChild(FusePerfectScrollbarDirective) directiveScroll: FusePerfectScrollbarDirective;
    @ViewChildren('replyInput') replyInputField;
    @ViewChild('replyForm') replyForm: NgForm;

    constructor(private chatService: ChatService)
    {
    }

    ngOnInit()
    {
        this.user = this.chatService.user;
        this.chatService.onChatSelected
            .subscribe(chatData => {
                if ( chatData )
                {
                    this.selectedChat = chatData;
                    this.contato = chatData.cliente;
                    //this.contato.avatar = 'assets/images/avatars/profile.jpg'; // TODO
                    this.dialogo = chatData.dialogo;
                    this.readyToReply();
                }
            });
        this.chatService.onMessageReceived
            .subscribe(message => {

                setTimeout(() => {
                    if (message.chatId === this.selectedChat.id) {
                        this.dialogo.push(message);
                        this.scrollToBottom();
                    }
                });
                
            });
    }

    ngAfterViewInit()
    {
        this.replyInput = this.replyInputField.first.nativeElement;
        this.readyToReply();
    }

    selectContact()
    {
        this.chatService.selectContato(this.contato);
    }

    readyToReply()
    {
        setTimeout(() => {
            this.replyForm.reset();
            this.focusReplyInput();
            this.scrollToBottom();
        });

    }

    focusReplyInput()
    {
        setTimeout(() => {
            this.replyInput.focus();
        });
    }

    scrollToBottom(speed?: number)
    {
        speed = speed || 400;
        if ( this.directiveScroll )
        {
            this.directiveScroll.update();

            setTimeout(() => {
                this.directiveScroll.scrollToBottom(0, speed);
                this.directiveScroll.update();
            });
        }
    }

    reply(event)
    {
        // Message
        const message = {
            id: uuidv4(),
            who    : this.user.id,
            message: this.replyForm.form.value.message,
            time   : new Date().toISOString()
        };

        // Add the message to the chat
        this.dialogo.push(message);

        //// Update the server
        //this.chatService.updateDialog(this.selectedChat.chatId, this.dialog).then(response => {
        //    this.readyToReply();
        //});

        this.chatService.SendMessage(this.selectedChat, message ).then(response => {
            this.readyToReply();
        });

    }
}
