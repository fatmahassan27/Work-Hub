import { Component, OnInit } from '@angular/core';
import { CommonModule, DatePipe } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { NotificationService } from '../Services/notification.service';
import { AccountService } from './../Services/account.service';
import { NotificationDTO } from '../Models/notification.model';

@Component({
  selector: 'app-notification',
  templateUrl: './notification.component.html',
  styleUrls: ['./notification.component.css'],
  imports: [DatePipe, FormsModule, CommonModule],
  standalone: true
})
export class NotificationComponent implements OnInit {

  notifications: NotificationDTO[] = [];
  userId: number = 0; // Assuming you have userId
  workerId: number = 0; // Assuming you have workerId
  currentUserId: number = 0;

  constructor(
    private notificationService: NotificationService,
    public accountService: AccountService
  ) {}

  ngOnInit() {
    if (this.accountService.currentUserValue?.id) {
      this.currentUserId = this.accountService.currentUserValue?.id;
      console.log(`${this.currentUserId} CURRENT USER ID`);
    }

    this.notificationService.getNotificationsHttp(this.currentUserId).subscribe({
      next: (nots: NotificationDTO[]) => {
        console.log(nots);
        this.notifications = nots;
        console.log("Component: notifications loaded successfully.");
      },
      error: (err) => {
        console.error('Component: Error receiving notifications: ', err);
      }
    });

    this.notificationService.invokeOnNewNotification().subscribe({
      next: (notification: NotificationDTO) => {
        this.notifications.push(notification);
        console.log('Component: New notification added:', notification);
      },
      error: (err) => {
        console.error('Component: Error receiving new notification: ', err);
      }
    });

    this.notificationService.addNotificationListener((notification: NotificationDTO) => {
      this.notifications.push(notification);
      console.log('Component: New notification added:', notification);
    })
  }
}


  // reload(){
  //   console.log("reloading... ownerId: ",this.currentUserId);

  //   this.notificationService.getNotificationsHttp(this.currentUserId)
  //   .subscribe({
  //     next: (nots: NotificationDTO[]) => {
  //       console.log(nots);
  //       this.notifications =nots;
  //       console.log("Component: notifications reloaded successfully.");
  //     },
  //     error: (err) => {
  //       console.error('Component: Error receiving notifications: ', err);
  //     },
  //     complete:()=>{
  //       console.log("Component: notifications retreived successfully");
  //     }
  //   });
  // }

  // SendOrderCreatedNotification() {
  //   this.notificationService.sendOrderCreatedNotification(this.userId, this.workerId)
  //     .then(() => {
  //       alert("Notification sent successfully");
  //     })
  //     .catch(err => {
  //       console.error('Error while sending notification: ', err);
  //       alert("Error sending notification");
  //     })
  //     .finally(() => {
  //       console.log("Send button clicked");
  //     });
  // }
  // SendOrderAcceptedNotification() {
  //   this.notificationService.sendOrderAcceptedNotification(this.userId, this.workerId)
  //   .then(() => {
  //     alert("Notification sent successfully");
  //   })
  //   .catch(err => {
  //     console.error('Error while sending notification: ', err);
  //     alert("Error sending notification");
  //   })
  //   .finally(() => {
  //     console.log("Send button clicked");
  //   });    }

