import { Component } from '@angular/core';
import { AccountService } from '../../Services/account.service';

@Component({
  selector: 'app-logout',
  standalone: true,
  imports: [],
  templateUrl: './logout.component.html',
  styleUrl: './logout.component.css'
})
export class LogoutComponent {
  constructor(public accountservice: AccountService) { }
  logout() {
    this.accountservice.logout();

    // this.orderService.DeleteUserItems(this.accountservice.r.UserId).subscribe(() => {
    //   // Reset cart count to zero after deleting cart items
    //   this.orderService.updateCartCount(0);
    // });
  }
}
