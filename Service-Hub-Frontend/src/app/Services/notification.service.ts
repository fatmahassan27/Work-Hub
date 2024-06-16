import { Injectable } from '@angular/core';
import * as signalR from '@microsoft/signalr';
import { Notification } from '../Models/notification';
import { Observable, Subject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class NotificationService {
  private hubConnection: signalR.HubConnection;
  private notificationSubject = new Subject<Notification>();
  private connectionPromise: Promise<void>;

  constructor() {
    this.hubConnection = new signalR.HubConnectionBuilder()
      .withUrl('https://localhost:5018/notificationsHub')
      .build();

    this.hubConnection.on('NewNotification', (notification: Notification) => {
      this.notificationSubject.next(notification);
    });

    this.connectionPromise = this.startConnection();
  }

  private startConnection(): Promise<void> {
    return this.hubConnection
      .start()
      .then(() => console.log('Connection started'))
      .catch(err => {
        console.log('Error while starting connection: ' + err);
        // Retry logic can be added here if needed
        throw err;
      });
  }

  private ensureConnection(): Promise<void> {
    if (this.hubConnection.state === signalR.HubConnectionState.Disconnected) {
      this.connectionPromise = this.startConnection();
    }
    return this.connectionPromise;
  }

  getNotifications(): Observable<Notification> {
    return this.notificationSubject.asObservable();
  }

  send(userId: number, workerId: number): Promise<void> {
    return this.ensureConnection().then(() => {
      return this.hubConnection.invoke("SendOrderCreatedNotification", userId, workerId)
        .then(() => console.log('Notification sent successfully'))
        .catch(err => console.error('Error while sending notification: ' + err));
    });
  }
}
