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
  private token: string | null = '';
  private notificationSubject = new Subject<NotificationDTO>();
  private connectionPromise: Promise<void> | null = null;
  notificationCount$: any;

  constructor(private http: HttpClient) {
    this.token = localStorage.getItem("token");
    this.hubConnection = new signalR.HubConnectionBuilder()
      .withUrl(this.signalrUrl, {
        skipNegotiation: true,
        transport: signalR.HttpTransportType.WebSockets,
        accessTokenFactory: () => this.token ? this.token : ''
      })
      .configureLogging(signalR.LogLevel.Information)
      .build();

    this.hubConnection.onclose((error) => {
      console.log('Connection closed', error ? error : "success");
    });

    this.hubConnection.onreconnecting((error) => {
      console.log('Reconnecting...', error ? error : "success");
    });

    this.hubConnection.onreconnected((connectionId) => {
      console.log('Reconnected with Connection ID:', connectionId);
    });

    this.hubConnection.on('NewNotification', (notification) => {
      this.notificationSubject.next(notification);
      console.log('New notification received:', notification);
    });

    this.startConnection();
  }

  private startConnection(): void {
    console.log(this.hubConnection.baseUrl, this.hubConnection.connectionId, this.hubConnection.state);
    this.hubConnection
      .start()
      .then(() => {
        console.log('SignalR connection started');
        console.log(this.hubConnection.baseUrl, this.hubConnection.connectionId, this.hubConnection.state);
      })
      .catch(err => {
        console.error('Error while starting SignalR connection:', err);
        setTimeout(() => this.startConnection(), 5000); // Retry connection after 5 seconds
      });
  }

  private ensureConnection(): Promise<void> {
    if (this.hubConnection.state === signalR.HubConnectionState.Disconnected) {
      console.log("hub connection state: ", this.hubConnection.state);
      this.connectionPromise = this.hubConnection.start();
    }
    if (this.connectionPromise === null) {
      this.connectionPromise = Promise.resolve();
    }
    return this.connectionPromise;
  }

  public async sendOrderCreatedNotification(userId: number, workerId: number): Promise<void> {
    // Ensure the connection is established
    await this.ensureConnection();

    // Validate and convert userId and workerId to integers
    const userIdInt = Number.isInteger(userId) ? userId : parseInt(userId.toString(), 10);
    const workerIdInt = Number.isInteger(workerId) ? workerId : parseInt(workerId.toString(), 10);

    // Log the values of userId and workerId
    console.log('Invoking SendOrderCreatedNotification with:');
    console.log('UserID:', userIdInt);
    console.log('WorkerID:', workerIdInt);

    // Perform the invocation and return the result
    try {
      await this.hubConnection!.invoke('SendOrderCreatedNotification', userIdInt, workerIdInt);
      console.log('Service: Notification sent successfully');
    } catch (err) {
      console.error('Service: Error while sending notification:', err);
      throw err; // Ensure error is propagated
    }
  }




  public sendOrderAcceptedNotification(userId: number, workerId: number): Promise<void> {
    return this.ensureConnection().then(() => {
      return this.hubConnection.invoke('SendOrderAcceptedNotification', userId, workerId)
        .then(() => console.log('Service: Notification sent successfully'))
        .catch(err => {
          console.error('Service: Error while sending notification:', err);
          throw err;
        });
    });
  }

  public getNotifications(ownerId: number): Observable<NotificationDTO[]> {
    return this.http.get<NotificationDTO[]>(`${this.apiUrl}notifications/${ownerId}`);
  }

  public onNewNotification(): Observable<NotificationDTO> {
    return this.notificationSubject.asObservable();
  }
}
