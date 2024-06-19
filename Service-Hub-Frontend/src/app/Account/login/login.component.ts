import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms'; // Import FormsModule
import { UserLogin } from '../../Models/user-login.model';
import { AccountService } from '../../Service/account.service';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [FormsModule,RouterLink],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent {
  
  user:UserLogin=new UserLogin("","");

  constructor(public accountService:AccountService){ }
  login()
  {
    console.log(this.user);
    this.accountService.login(this.user)
  }
}
