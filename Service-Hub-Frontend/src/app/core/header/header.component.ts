import { Component } from '@angular/core';
import { Route, Router, RouterLink, RouterLinkActive } from '@angular/router';
import { AccountService } from '../../Services/account.service';
import { CommonModule } from '@angular/common';
import { Role } from '../../enums/role';

@Component({
  selector: 'app-header',
  standalone: true,
  imports: [RouterLink,RouterLinkActive,CommonModule],
  templateUrl: './header.component.html',
  styleUrl: './header.component.css'
})
export class HeaderComponent {
  [x: string]: any;

  selectedRole: Role = Role.User;
  constructor(public accountService:AccountService ,public router: Router )
  {
     console.log(accountService.userInfo);
  }
  logout()
  {
    this.accountService.logout();
  }
    
}
