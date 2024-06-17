import { Injectable } from '@angular/core';
import * as signalR from '@microsoft/signalr';
import { Notification } from '../Models/notification';
import { Observable, Subject } from 'rxjs';
import { HttpClient, HttpHeaders } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class NotificationService {

  private apiUrl = 'http://localhost:5018/api/';

  private hubConnection: signalR.HubConnection;
  private notificationSubject = new Subject<Notification>();
  private connectionPromise: Promise<void>;

  constructor(public http :HttpClient) 
  {
    this.hubConnection = new signalR.HubConnectionBuilder()
      .withUrl('http://localhost:5018/notificationsHub', {
        skipNegotiation: true,
        transport: signalR.HttpTransportType.WebSockets
      })
      .build();

      this.hubConnection.onclose(error => {
        if (error) {
          console.error('SignalR connection closed due to error:', error);
        } else {
          console.warn('SignalR connection closed');
        }
      });
      

    this.hubConnection.on('NewNotification', (notification: Notification) => {
      this.notificationSubject.next(notification);
      console.log(notification);
      console.log("on new notification");
    });

    this.connectionPromise = this.startConnection();
  }

  private startConnection(): Promise<void> {
    return this.hubConnection
      .start()
      .then(() => console.log('Connection started'))
      .catch(err => {
        console.error('Error while starting connection: ', err);
        setTimeout(() => this.startConnection(), 5000); // Retry connection after 5 seconds
        throw err;
      });
  }

  private ensureConnection(): Promise<void> {
    if (this.hubConnection.state === signalR.HubConnectionState.Disconnected) {
      this.connectionPromise = this.startConnection();
    }
    return this.connectionPromise;
  }

  getNotificationsHttp(ownerId: number): Observable<Notification[]> {
    // Example of setting Authorization header correctly
    const headers = new HttpHeaders({
      'Authorization': 'Bearer your_access_token_here'
    });
    return this.http.get<Notification[]>(`${this.apiUrl}notifications/${ownerId}`, { headers });
  }

  getNotifications(): Observable<Notification> {
    return this.notificationSubject.asObservable();
  }

  SendOrderCreatedNotification(userId: number, workerId: number): Promise<void> {
    return this.ensureConnection().then(() => {
      return this.hubConnection.invoke("SendOrderCreatedNotification", userId, workerId)
        .then(() => console.log('Service: Notification sent successfully'))
        .catch(err => {
          console.error('Service: Error while sending notification: ', err);
          throw err;
        });
    });
  }

  SendOrderAcceptedNotification(userId: number, workerId: number): Promise<void> {
    return this.ensureConnection().then(() => {
      return this.hubConnection.invoke("SendOrderAcceptedNotification", userId, workerId)
        .then(() => console.log('Service: Notification sent successfully'))
        .catch(err => {
          console.error('Service: Error while sending notification: ', err);
          throw err;
        });
    });
  }

  
}
