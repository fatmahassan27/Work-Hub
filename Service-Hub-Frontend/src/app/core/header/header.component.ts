import { Component } from '@angular/core';
import { RouterLink, RouterLinkActive } from '@angular/router';
import { AccountService } from '../../Services/account.service';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Role } from '../../enums/role';
import { NotificationService } from '../../Services/notification.service';

@Component({
  selector: 'app-header',
  standalone: true,
  imports: [RouterLink, RouterLinkActive, CommonModule, FormsModule],
  templateUrl: './header.component.html',
  styleUrl: './header.component.css'
})
export class HeaderComponent {
searchKeyWord: any;
notificationCount = 0;
  constructor(public accountService: AccountService, public notificationServices: NotificationService) { }
  ngOnInit(): void {
    this.notificationServices.notificationCount$.subscribe((count: number) => {
      this.notificationCount=count;
    })
  };

  logout() {
    this.accountService.logout();
  }

}

