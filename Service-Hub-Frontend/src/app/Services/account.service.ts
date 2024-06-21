import { HttpClient ,HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import * as jwtdecode from 'jwt-decode';
import { UserLogin } from '../Models/user-login.model';
import { Router } from '@angular/router';
import { UserInfo } from './../interfaces/user-info';
import { RegisterationDTO } from '../Models/registration-dto';



@Injectable({
  providedIn: 'root'
})
export class AccountService {

  userInfo: UserInfo | null = null;
  isAuthenticated=false;
  baseurl='http://localhost:5018/api/Account/';

  constructor(public http: HttpClient, public router: Router) { }

  
  register(registerDTO :RegisterationDTO){
    return this.http.post(this.baseurl + "Register" ,registerDTO,{ responseType: 'text' });
  }

  login(user: UserLogin) {
    this.http.post(this.baseurl+ "login" ,user,{responseType:'text'}).subscribe(token=>{
      this.isAuthenticated=true;
      localStorage.setItem("token",token);
      this.userInfo=jwtdecode.jwtDecode<UserInfo>(token);
      console.log(this.userInfo);

      this.router.navigateByUrl("/home");
    })
  }

  logout() :void{

      this.isAuthenticated=false;
      localStorage.removeItem("token");

  }

}

