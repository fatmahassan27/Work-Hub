import { Component } from '@angular/core';
import { Route, Router, RouterLink, RouterLinkActive } from '@angular/router';
import { AccountService } from '../../Services/account.service';
import { RegisterationDTO } from '../../Models/registration-dto';
import { UserLogin } from '../../Models/user-login.model';
import { Role } from '../../enums/role';
@Component({
  selector: 'app-header',
  standalone: true,
  imports: [RouterLink,RouterLinkActive],
  templateUrl: './header.component.html',
  styleUrl: './header.component.css'
})
export class HeaderComponent {
  [x: string]: any;

  selectedRole: Role = Role.User;
  constructor(public accountService:AccountService ,public router: Router)
  {

  }
  logout()
  {
    this.accountService.logout();
  }

}
