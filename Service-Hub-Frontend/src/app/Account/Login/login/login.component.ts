import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { AccountService } from '../../../Services/account.service';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent {
  emailuser:string="";
  password:string="";
  constructor(public accountService:AccountService){ }
  login()
  {
    this.accountService.login(this.emailuser,this.password)
  }
}
