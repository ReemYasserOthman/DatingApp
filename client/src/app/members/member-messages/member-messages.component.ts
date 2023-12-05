import { TimeagoModule } from 'ngx-timeago';
import { CommonModule } from '@angular/common';
import { ChangeDetectionStrategy, Component, Input, OnInit, ViewChild } from '@angular/core';
import { Message } from 'src/app/_models/message';
import { MessageService } from 'src/app/_services/message.service';
import { FormsModule, NgForm } from '@angular/forms';

@Component({
  changeDetection: ChangeDetectionStrategy.OnPush,
  selector: 'app-member-messages',
  standalone: true,
  templateUrl: './member-messages.component.html',
  styleUrls: ['./member-messages.component.css'],
  imports: [CommonModule, TimeagoModule, FormsModule]
})
export class MemberMessagesComponent  implements OnInit{
  
  @ViewChild('messageForm') messageForm?: NgForm;
  @Input() username?: string;
   messageContent ='';
  // @Input() messages: Message[] = [];

  constructor(public messageService: MessageService) {
       
  }
  ngOnInit(): void {
   
  }
  
  sendMessage() {
    if (!this.username) return;
    this.messageService.sendMessageByHttpRequst(this.username, this.messageContent)
    .subscribe(()=>
    {
      this.messageForm?.reset();
    });

   }

}
