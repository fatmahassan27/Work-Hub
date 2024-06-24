import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms'; // Import FormsModule
import { UserLogin } from '../../Models/user-login.model';
import { AccountService } from '../../Services/account.service';
import { Router, RouterLink } from '@angular/router';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [FormsModule,RouterLink],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent {
  
  user:UserLogin=new UserLogin("","");

  constructor(public accountService:AccountService, public router:Router){ }
  login()
  {
    console.log(`Login Email: ${this.user.email}`);
    this.accountService.login(this.user).subscribe(
      (d)=>{
        console.log(d);
        alert(`Welcome ${d.role} ${d.name}`);
      },
      (er)=>{
        console.log(er);
        alert(er.statusText)
      },
      ()=> this.router.navigate(['/home'])
    )
  }
}
