import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import * as jwtdecode from 'jwt-decode';
@Injectable({
  providedIn: 'root'
})


export class AccountService {
  isAuthenticated=false;
  baseurl="http://localhost:5018/api/Account/Login";
  login(emailuser:string,password:string)
  {
    let str:string=`?username=${emailuser}&password=${password}`;
   return this.http.post(this.baseurl+str,{responseType :'text'}).subscribe(d=>{this.isAuthenticated=true;
      localStorage.setItem("token",d.toString());
      let r:{useremail:string,isUser:boolean,isWorker:boolean}=jwtdecode.jwtDecode(d.toString());
      console.log(r.useremail);
      console.log(r.isUser);
      console.log(r.isWorker);
    });
    }
    logout()
    {
      this.isAuthenticated=false;
      localStorage.removeItem("token");
    }
  constructor(public http:HttpClient) { }
}

