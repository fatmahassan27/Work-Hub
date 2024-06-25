// import { HttpClient ,HttpHeaders } from '@angular/common/http';
// import { Injectable } from '@angular/core';
// import * as jwtdecode from 'jwt-decode';
// import { UserLogin } from '../Models/user-login.model';
// import { Router } from '@angular/router';
// import { UserInfo } from './../interfaces/user-info';
// import { RegisterationDTO } from '../Models/registration-dto';
// import { BehaviorSubject, Observable, map } from 'rxjs';



// @Injectable({
//   providedIn: 'root'
// })
// export class AccountService {

//   // private currentUserSubject: BehaviorSubject<any>;
//   // public currentUser: Observable<any>;
  
//   userInfo: UserInfo | null = null;
//   isAuthenticated=false;
//   baseurl='http://localhost:5018/api/Account/';

//   constructor(public http: HttpClient, public router: Router){}
//   //  {
//   //   this.currentUserSubject = new BehaviorSubject<UserInfo>(jwtdecode.jwtDecode<UserInfo>(localStorage.getItem("token")!));
//   //   this.currentUser = this.currentUserSubject.asObservable();
//   //   }
//   //   public get currentUserValue() {
//   //     return this.currentUserSubject.value;
//   //   }
    
//   //   login(user: UserLogin) {
//   //     return this.http.post<any>(this.baseurl+ "login", user)
//   //       .pipe(map(token => {
//   //         localStorage.setItem('token', token);
//   //         this.userInfo=jwtdecode.jwtDecode<UserInfo>(token);
//   //         console.log(this.userInfo);
//   //         this.currentUserSubject.next(this.userInfo);
//   //         return user;
//   //       }));
//   //   }
  
//   register(registerDTO :RegisterationDTO){
//     return this.http.post(this.baseurl + "Register" ,registerDTO,{ responseType: 'text' as 'json'});
//   }

//   login(user: UserLogin) {
//     this.http.post(this.baseurl+ "login" ,user,{responseType:'text'}).subscribe(
//       token=>{
//         this.isAuthenticated=true;
//         localStorage.setItem("token",token);
//         this.userInfo=jwtdecode.jwtDecode<UserInfo>(token);
//         console.log(this.userInfo);
//         this.router.navigateByUrl("/home");
//       },
//     error => console.log(error)
//     )
//   }

//   logout() :void{
//       this.isAuthenticated=false;
//       localStorage.removeItem("token");
//       //this.currentUserSubject.next(null);

//       this.router.navigateByUrl("/home");
//   }

// }

import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import * as jwt_decode from 'jwt-decode';
import { UserLogin } from '../Models/user-login.model';
import { Router } from '@angular/router';
import { UserInfo } from './../interfaces/user-info';
import { RegisterationDTO } from '../Models/registration-dto';
import { BehaviorSubject, Observable, map } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AccountService {

  private currentUserSubject: BehaviorSubject<UserInfo | null>;
  public currentUser: Observable<UserInfo | null>;
  
  baseurl = 'http://localhost:5018/api/Account/';

  constructor(private http: HttpClient, private router: Router) {
    const token = localStorage.getItem('token');
    const decodedUserInfo = token ? jwt_decode.jwtDecode<UserInfo>(token) : null;
    this.currentUserSubject = new BehaviorSubject<UserInfo | null>(decodedUserInfo);
    this.currentUser = this.currentUserSubject.asObservable();
  }

  public get currentUserValue(): UserInfo | null {
    return this.currentUserSubject.value;
  }
  
  login(user: UserLogin): Observable<any> {
    return this.http.post<any>(`${this.baseurl}login`, user, { responseType: 'text' as 'json' })
      .pipe(map(token => {
        localStorage.setItem('token', token);
        const decodedUserInfo = jwt_decode.jwtDecode<UserInfo>(token);
        this.currentUserSubject.next(decodedUserInfo);
        return decodedUserInfo;
      }));
  }

  register(registerDTO: RegisterationDTO): Observable<string> {
    return this.http.post<string>(`${this.baseurl}Register`, registerDTO, { responseType: 'text' as 'json' });
  }

  logout(): void {
    console.log("logged out");
    localStorage.removeItem('token');
    this.currentUserSubject.next(null);
    this.router.navigateByUrl('/login');
  }

  isAuthenticated(): boolean {
    return !!this.currentUserValue;
  }
  isWorker():boolean{
    return this.currentUserValue?.role.toString() == 'Worker';
  }
}
