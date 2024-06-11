import { HttpClient ,HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import * as jwtdecode from 'jwt-decode';


@Injectable({
  providedIn: 'root'
})


export class AccountService {

  isAuthenticated=false;
  baseurl='http://localhost:5018/api/Account/Login';
  constructor(public http:HttpClient) { }

  login(Email:string,Password:string) :void{
  {  
    //const headers = new HttpHeaders({ 'Content-Type': 'application/json' });
     let str:string=`?Email=${Email}&Password=${Password}`;
     this.http.post(this.baseurl+str,{responseType :'text'}).subscribe(d=>{this.isAuthenticated=true;
      localStorage.setItem("token",d.toString());
      let r:{username:string,isUser:boolean,isWorker:boolean}=jwtdecode.jwtDecode(d.toString());
      console.log(r.username);
      console.log(r.isUser);
      console.log(r.isWorker);
    },
    error => {
      console.error('Login failed', error);
    });
    }
  }
    logout() :void{
    
        this.isAuthenticated=false;
        localStorage.removeItem("token");
     
    }
   
}

