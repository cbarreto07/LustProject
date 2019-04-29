import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import 'rxjs/add/operator/debounceTime';
import 'rxjs/add/operator/distinctUntilChanged';

import { ChatService } from '../../../services/chat.service';

@Component({
    selector   : 'fuse-chat-user-sidenav',
    templateUrl: './user.component.html',
    styleUrls  : ['./user.component.scss']
})
export class FuseChatUserSidenavComponent implements OnInit, OnDestroy
{
    user: any;
    onFormChange: any;
    userForm: FormGroup;

    constructor(private chatService: ChatService)
    {
        this.user = this.chatService.user;
        this.userForm = new FormGroup({
            humor: new FormControl(this.user.humor),
            status: new FormControl(this.user.status)
        });
    }

    ngOnInit()
    {
        this.onFormChange = this.userForm.valueChanges
                                .debounceTime(500)
                                .distinctUntilChanged()
                                .subscribe(data => {
                                    this.user.humor = data.humor;
                                    this.user.status = data.status;
                                    this.chatService.updateUserData(this.user);
                                });
    }

    changeLeftSidenavView(view)
    {
        this.chatService.onLeftSidenavViewChanged.next(view);
    }

    ngOnDestroy()
    {
        this.onFormChange.unsubscribe();
    }
}
