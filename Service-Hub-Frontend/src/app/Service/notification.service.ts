import { Injectable } from '@angular/core';
import * as signalR from '@microsoft/signalr';
import { Notification } from '../Models/notification.model';
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

  constructor(private http: HttpClient) {
    const token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpZCI6IjgiLCJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1laWRlbnRpZmllciI6IjgiLCJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiYW1yciIsImh0dHA6Ly9zY2hlbWFzLnhtbHNvYXAub3JnL3dzLzIwMDUvMDUvaWRlbnRpdHkvY2xhaW1zL2VtYWlsYWRkcmVzcyI6ImFtcnJAZXhhbXBsZS5jb20iLCJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL3JvbGUiOiJVc2VyIiwiZXhwIjoxNzE4OTE5MTMyLCJpc3MiOiJodHRwOi8vbG9jYWxob3N0OjUwMTgvIn0.WCQCWBzJCVgHRA3_RJclFxcRYHB5uKK6YQgv37niDY4";

    this.hubConnection = new signalR.HubConnectionBuilder()
      .withUrl('http://localhost:5018/notificationsHub', {
        skipNegotiation: true,
        transport: signalR.HttpTransportType.WebSockets,
        accessTokenFactory: () => token,
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
      console.log('New notification received:', notification);
    });

    this.connectionPromise = this.startConnection();
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

  getNotificationsHttp(ownerId: number): Observable<Notification[]> {
    const headers = new HttpHeaders({
      'Authorization':`Bearer YOUR_ACCESS_TOKEN_HERE`
    });
    return this.http.get<Notification[]>(`${this.apiUrl}notifications/${ownerId}, { headers }`);
  }

  getNotifications(): Observable<Notification> {
    return this.notificationSubject.asObservable();
  }

  sendOrderCreatedNotification(userId: number, workerId: number): Promise<void> {
    return this.ensureConnection().then(() => {
      return this.hubConnection.invoke('SendOrderCreatedNotification', userId, workerId)
        .then(() => console.log('Notification sent successfully'))
        .catch(err => {
          console.error('Error while sending notification:', err);
          throw err;
        });
    });
  }

  sendOrderAcceptedNotification(userId: number, workerId: number): Promise<void> {
    return this.ensureConnection().then(() => {
      return this.hubConnection.invoke('SendOrderAcceptedNotification', userId, workerId)
        .then(() => console.log('Notification sent successfully'))
        .catch(err => {
          console.error('Error while sending notification:', err);
          throw err;
        });
    });
  }
}

// import { Injectable } from '@angular/core';
// import * as signalR from '@microsoft/signalr';
// import { Notification } from '../Models/notification';
// import { Observable, Subject } from 'rxjs';
// import { HttpClient, HttpHeaders } from '@angular/common/http';

// @Injectable({
//   providedIn: 'root'
// })
// export class NotificationService {

//   private apiUrl = 'http://localhost:5018/api/';

//   private hubConnection: signalR.HubConnection;
//   private notificationSubject = new Subject<Notification>();
//   private connectionPromise: Promise<void>;


//   constructor(public http :HttpClient)
//   {
//     const token = 'YOUR_JWT_TOKEN';

// connection.start().catch(err => console.error(err.toString()));

//     this.hubConnection = new signalR.HubConnectionBuilder()
//       .withUrl('http://localhost:5018/notificationsHub', {
//         skipNegotiation: true,
//         transport: signalR.HttpTransportType.WebSockets,
//         accessTokenFactory () => token ,
//       })
//       .build();

//       this.hubConnection.onclose(error => {
//         if (error) {
//           console.error('SignalR connection closed due to error:', error);
//         } else {
//           console.warn('SignalR connection closed');
//         }
//       });


//     this.hubConnection.on('NewNotification', (notification: Notification) => {
//       this.notificationSubject.next(notification);
//       console.log(notification);
//       console.log("on new notification");
//     });

//     this.connectionPromise = this.startConnection();
//   }

//   private startConnection(): Promise<void> {
//     return this.hubConnection
//       .start()
//       .then(() => console.log('Connection started'))
//       .catch(err => {
//         console.error('Error while starting connection: ', err);
//         setTimeout(() => this.startConnection(), 5000); // Retry connection after 5 seconds
//         throw err;
//       });
//   }

//   private ensureConnection(): Promise<void> {
//     if (this.hubConnection.state === signalR.HubConnectionState.Disconnected) {
//       this.connectionPromise = this.startConnection();
//     }
//     return this.connectionPromise;
//   }

//   getNotificationsHttp(ownerId: number): Observable<Notification[]> {
//     // Example of setting Authorization header correctly
//     const headers = new HttpHeaders({
//       'Authorization': 'Bearer your_access_token_here'
//     });
//     return this.http.get<Notification[]>(${this.apiUrl}notifications/${ownerId}, { headers });
//   }

//   getNotifications(): Observable<Notification> {
//     return this.notificationSubject.asObservable();
//   }

//   SendOrderCreatedNotification(userId: number, workerId: number): Promise<void> {
//     return this.ensureConnection().then(() => {
//       return this.hubConnection.invoke("SendOrderCreatedNotification", userId, workerId)
//         .then(() => console.log('Service: Notification sent successfully'))
//         .catch(err => {
//           console.error('Service: Error while sending notification: ', err);
//           throw err;
//         });
//     });
//   }

//   SendOrderAcceptedNotification(userId: number, workerId: number): Promise<void> {
//     return this.ensureConnection().then(() => {
//       return this.hubConnection.invoke("SendOrderAcceptedNotification", userId, workerId)
//         .then(() => console.log('Service: Notification sent successfully'))
//         .catch(err => {
//           console.error('Service: Error while sending notification: ', err);
//           throw err;
//         });
//     });
//   }


// }
