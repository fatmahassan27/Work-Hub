import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class AccountService {
  isAuthenticated=false;
  baseurl="http://localhost:5018/api/Account/Login";
  login(emailuser:string , password:string)
  {
    let str:string=`?username=${emailuser}&password=${password}`;
    this.http.get<string>(this.baseurl+str).subscribe(d=>{console.log(d)});
    }
  constructor(public http:HttpClient) { }
}
