import { Injectable } from '@angular/core';
import * as signalR from '@microsoft/signalr';
import { NotificationDTO } from '../Models/notification.model';
import { Observable, Subject } from 'rxjs';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class NotificationService {

  private apiUrl = 'http://localhost:5018/api/';
  private signalrUrl = `http://localhost:5018/notificationsHub`;

  private hubConnection: signalR.HubConnection;
  private notificationSubject = new Subject<NotificationDTO>();
  private connectionPromise: Promise<void>;
  private token: string | null = '';

  constructor(private http: HttpClient) {
    this.token = localStorage.getItem("token");
    this.hubConnection = new signalR.HubConnectionBuilder()
      .withUrl(this.signalrUrl, {
        skipNegotiation: true,
        transport: signalR.HttpTransportType.WebSockets,
        accessTokenFactory: () => this.token ? this.token : ''
      })
      .build();

    this.hubConnection.onclose(error => {
      if (error) {
        console.error('SignalR connection closed due to error:', error);
      } else {
        console.warn('SignalR connection closed');
      }
    });

    this.hubConnection.on('NewNotification', (notification: NotificationDTO) => {
      this.notificationSubject.next(notification);
      console.log('New notification received:', notification);
    });

    this.connectionPromise = this.startConnection();
  }

  invokeOnNewNotification(): Observable<NotificationDTO> {
    return this.notificationSubject.asObservable();
  }

  private startConnection(): Promise<void> {
    return this.hubConnection
      .start()
      .then(() => console.log('SignalR connection started'))
      .catch(err => {
        console.error('Error while starting SignalR connection:', err);
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

  sendOrderCreatedNotification(userId: number, workerId: number): Promise<void> {
    return this.ensureConnection().then(() => {
      console.log(`${userId} --- ${workerId}`);
      return this.hubConnection.invoke('sendordercreatednotification', userId, workerId)
        .then(() => console.log('Service: Notification sent successfully'))
        .catch(err => {
          console.error('Service: Error while sending notification:', err);
          throw err;
        });
    });
  }

  sendOrderAcceptedNotification(userId: number, workerId: number): Promise<void> {
    return this.ensureConnection().then(() => {
      return this.hubConnection.invoke('SendOrderAcceptedNotification', userId, workerId)
        .then(() => console.log('Service: Notification sent successfully'))
        .catch(err => {
          console.error('Service: Error while sending notification:', err);
          throw err;
        });
    });
  }

  getNotificationsHttp(ownerId: number): Observable<NotificationDTO[]> {
    return this.http.get<NotificationDTO[]>(`${this.apiUrl}notifications/${ownerId}`);
  }
}
