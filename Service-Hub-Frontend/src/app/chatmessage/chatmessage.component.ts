import { Component, OnInit } from '@angular/core';
import { ChatMessage } from '../Models/Chat.model';
import { ChatService } from '../Services/chat.service';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { AccountService } from '../Services/account.service';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-chatmessage',
  standalone: true,
  imports: [CommonModule,FormsModule],
  templateUrl: './chatmessage.component.html',
  styleUrl: './chatmessage.component.css'
})
export class ChatmessageComponent implements OnInit {
   
  public messages: ChatMessage[] = [];
  // public messageText: string='';
  // public username: string=''; // Assuming username is used for displaying purposes
  // public senderId: number=0; // Set this to the current user's ID
  // public receiverId: number=0; //

  ///////////////////////////
  chatmessage:ChatMessage=new ChatMessage(0,0,0,"",false,new Date());
  currentUserId: number = 0;
  constructor(public chatservice:ChatService ,public accountService:AccountService,public activatedroute:ActivatedRoute)
  {}

  ngOnInit(): void {
      this.chatservice.addMessageListener((message: ChatMessage) => {
          this.messages.push(message);
      });

    if (this.accountService.currentUserValue?.id) {
      this.currentUserId = this.accountService.currentUserValue?.id;
      console.log(`${this.currentUserId} CURRENT USER ID`);
      this.chatmessage.SenderId = this.currentUserId;
      
    }

    this.activatedroute.params.subscribe((p)=>{
      console.log(p['id']);
      this.chatmessage.ReceiverId = p['id'] ;
    })
  }
  
  public send(): void {
    this.messages.push(this.chatmessage);
    if (this.chatmessage) {
      //const message = new ChatMessage(0, this.senderId, this.receiverId, this.messageText, false, new Date());
      this.chatservice.sendMessage(this.chatmessage);
      this.chatmessage.Message = '';
    }
  }
}
