import { Component, OnInit } from '@angular/core';
import { CommonModule, DatePipe } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { NotificationService } from '../Services/notification.service';
import { AccountService } from '../Services/account.service';
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
  currentUserId: number = 0;
  currentUserName: string = '';

  constructor(
    private notificationService: NotificationService,
    public accountService: AccountService
  ) {}

  ngOnInit() {
    if (this.accountService.currentUserValue?.id) {
      this.currentUserId = this.accountService.currentUserValue.id;
      console.log(`${this.currentUserId} CURRENT USER ID`);
      this.currentUserName = this.accountService.currentUserValue.name // Assuming the name property is available
      console.log(`${this.currentUserName} CURRENT USER NAME`);
    }

    this.notificationService.getNotifications(this.currentUserId).subscribe({
      next: (nots: NotificationDTO[]) => {
        console.log(nots);
        this.notifications = nots;
        console.log("Component: notifications loaded successfully.");
      },
      error: (err) => {
        console.error('Component: Error receiving notifications: ', err);
      }
    });

    this.notificationService.onNewNotification().subscribe({
      next: (notification: NotificationDTO) => {
        this.notifications.push(notification);
        console.log('Component: New notification added:', notification);
      },
      error: (err) => {
        console.error('Component: Error receiving new notification:', err);
      }
    });
  }
}
