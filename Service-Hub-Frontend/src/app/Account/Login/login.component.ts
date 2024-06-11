import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { AccountService } from '../../../Services/account.service';
import { UserLogin } from '../../../Models/user-login';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [FormsModule],
  providers:[AccountService],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent {
  user:UserLogin=new UserLogin("","");
  constructor(public accountService:AccountService){ }
  login()
  {
    this.accountService.login(this.user)
  }
}
