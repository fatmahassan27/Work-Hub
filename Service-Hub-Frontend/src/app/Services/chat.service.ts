import { HttpClient } from '@angular/common/http';
import * as signalR from '@microsoft/signalr';
import { Injectable } from '@angular/core';
import { ChatMessage } from '../Models/Chat.model';
import { Observable, Subject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ChatService {

  private baseurl = 'http://localhost:5018/api/Chat';
  private signalrUrl = `http://localhost:5018/chatHub`;

  private token: string | null = '';
  private hubConnection: signalR.HubConnection;
  private chatSubject = new Subject<ChatMessage>();
  //private connectionPromise: Promise<void>;

  constructor(public http:HttpClient) 
  {
    this.token = localStorage.getItem("token");

    this.hubConnection = new signalR.HubConnectionBuilder()
    .withUrl(this.signalrUrl, {
      skipNegotiation: true,
      transport: signalR.HttpTransportType.WebSockets,
      accessTokenFactory: () => this.token ? this.token : ''
    })
    .build();
    
    this.hubConnection.start()
    .then(() => console.log('Connection started!'))
    .catch(err => console.error('Error while starting connection: ' + err));
      
    this.hubConnection.on('newmessage', (chatMessage: ChatMessage) => {
      this.chatSubject.next(chatMessage);
      console.log('New message received:', chatMessage);
    });



    // this.hubConnection.onclose(error => {
    //   if (error) {
    //     console.error('SignalR connection closed due to error:', error);
    //   } else {
    //     console.warn('SignalR connection closed');
    //   }
    // });
  
  }

  public addMessageListener(callback: (message: ChatMessage) => void): void {
    this.hubConnection.on('newmessage', callback);
  }

  public sendMessage(chatMessage: ChatMessage): void {
    console.log('Sending message:', chatMessage);
    this.hubConnection.invoke<any>("SendMessage", chatMessage)
     .then(() => console.log('Message sent successfully!'))
     .catch(err => console.error(err.toString()));
  }
  public getChatObservable(): Observable<ChatMessage> {
    return this.chatSubject.asObservable();
  }
}
