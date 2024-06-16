import { HttpClient ,HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import * as jwtdecode from 'jwt-decode';
import { UserLogin } from '../Models/user-login';

@Injectable({
  providedIn: 'root'
})
export class AccountService {
  r: { isAdmin: boolean, isCustomer: boolean, name: string, UserId: number } = { isAdmin: true, isCustomer: true, name: "", UserId: 0 };

  isAuthenticated=false;
  baseurl='http://localhost:5018/api/Account/Login';
  constructor(public http:HttpClient) { this.checkToken}

  private checkToken(): void {
    let token = localStorage.getItem("token");
    if (token) {
      this.isAuthenticated = true;
      this.r = jwtdecode.jwtDecode(token);
      console.log(this.r.isAdmin);
      console.log(this.r.isCustomer);
      console.log(this.r.name);
      console.log(this.r.UserId);
    } else {
      this.isAuthenticated = false;
    }
  }
  // login(user:UserLogin) 
  // {
  //    let str:string=`?Email=${user.Email}&Password=${user.Password}`;
  //    return  this.http.post(this.baseurl+str,{responseType :'text'}).subscribe(d=>
  //     {this.isAuthenticated=true;
  //   let str: string = `?username=${user.Email}&password=${user.Password}`;
  //   return this.http.post(this.baseurl + str, { responseType: 'text' }).subscribe(d => {
  //     this.isAuthenticated = true;
  //     localStorage.setItem("token",d.toString());
  //     let r=jwtdecode.jwtDecode(d.toString());
  //     console.log(this.r.isAdmin);
  //     console.log(this.r.isCustomer);
  //     console.log(this.r.name);
  //     console.log(this.r.UserId);
      
  //   },
  //   error => {
  //     console.error('Login failed', error);
  //   });
  //   }
  
  //   logout() :void{
    
  //       this.isAuthenticated=false;
  //       localStorage.removeItem("token");
     
  //   }
  }


