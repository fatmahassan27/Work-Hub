import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { AccountService } from '../../../Services/account.service';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [FormsModule],
  providers:[AccountService],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent {
  Email:string="";
  Password:string="";
  constructor(public accountService:AccountService){ }
  login()
  {
    this.accountService.login(this.Email,this.Password)
  }
}
