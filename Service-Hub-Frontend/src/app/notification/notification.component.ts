import { Component, OnInit } from '@angular/core';
import { NotificationService } from '../Services/notification.service';
import { Notification } from '../Models/notification';
import { CommonModule, DatePipe } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-notification',
  templateUrl: './notification.component.html',
  styleUrls: ['./notification.component.css'],
  imports: [DatePipe, FormsModule, CommonModule],
  standalone: true
})
export class NotificationComponent implements OnInit {

  notifications: Notification[] = [];
  userId: number = 1; // Assuming you have userId
  workerId: number = 2; // Assuming you have workerId

  constructor(private notificationService: NotificationService) {}

  ngOnInit() {
    this.notificationService.getNotifications().subscribe({
      next: (notification: Notification) => {
        this.notifications.push(notification);
        console.log("ng on init");
      },
      error: (err) => {
        console.error('Error receiving notifications: ', err);
      }
    });
  }
  reload(ownerId : number){
    console.log("reloading... ownerId: ",ownerId);

    this.notificationService.getNotificationsHttp(ownerId)
    .subscribe({
      next: (notifications: Notification[]) => {
        this.notifications =(notifications);
        console.log("reloading");
        console.log(notifications);
      },
      error: (err) => {
        console.error('Error receiving notifications: ', err);
      }
    });
  }

  SendOrderCreatedNotification() {
    this.notificationService.SendOrderCreatedNotification(this.userId, this.workerId)
      .then(() => {
        alert("Notification sent successfully");
      })
      .catch(err => {
        console.error('Error while sending notification: ', err);
        alert("Error sending notification");
      })
      .finally(() => {
        console.log("Send button clicked");
      });
  }
  SendOrderAcceptedNotification() {
    this.notificationService.SendOrderAcceptedNotification(this.userId, this.workerId)
    .then(() => {
      alert("Notification sent successfully");
    })
    .catch(err => {
      console.error('Error while sending notification: ', err);
      alert("Error sending notification");
    })
    .finally(() => {
      console.log("Send button clicked");
    });    }
}
