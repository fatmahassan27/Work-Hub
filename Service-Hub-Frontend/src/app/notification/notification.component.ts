import { Component, OnInit } from '@angular/core';
import { NotificationService } from '../Services/notification.service';
import { Notification } from '../Models/notification';
import { DatePipe } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-notification',
  templateUrl: './notification.component.html',
  styleUrls: ['./notification.component.css'],
  imports:[DatePipe,FormsModule],
  standalone: true
})
export class NotificationComponent implements OnInit {
  notifications: Notification[] = [];
  userId: number = 1; // Assuming you have userId
  workerId: number = 2; // Assuming you have workerId

  constructor(private notificationService: NotificationService) {}

  ngOnInit() {
    this.notificationService.getNotifications().subscribe((notification: Notification) => {
      this.notifications.push(notification);
    });
  }

  Send() {
    this.notificationService.send(this.userId, this.workerId)
      .then(() => {
        alert("Notification sent successfully");
      })
      .catch(err => {
        console.error('Error while sending notification: ' + err);
        alert("Error sending notification");
      })
      .finally(() => {
        alert("clicked");
      });
  }
}
