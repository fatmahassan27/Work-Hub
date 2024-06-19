import { HttpClient ,HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import * as jwtdecode from 'jwt-decode';
import { UserLogin } from '../Models/user-login.model';
import { Router } from '@angular/router';


@Injectable({
  providedIn: 'root'
})
export class AccountService {

  //r: { isAdmin: boolean, isCustomer: boolean, name: string, UserId: number } = { isAdmin: true, isCustomer: true, name: "", UserId: 0 };
  
  r:string|null=null;
  isAuthenticated=false;
  baseurl='http://localhost:5018/api/Account/login';

  constructor(public http: HttpClient, public router: Router) { }

  login(user: UserLogin) {
    this.http.post(this.baseurl,user,{responseType:'text'}).subscribe(d=>{
      this.isAuthenticated=true;
      console.log(d);
      localStorage.setItem("token",d);
      this.r=jwtdecode.jwtDecode(d);
      console.log(this.r);
      // console.log(this.r.isAdmin);
      // console.log(this.r.isCustomer);
      // console.log(this.r.name);
      // console.log(this.r.UserId);
      this.router.navigateByUrl("/home");
    })
  }

    logout() :void{

        this.isAuthenticated=false;
        localStorage.removeItem("token");

    }

}

