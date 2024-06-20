import { Injectable } from '@angular/core';
import { HttpClient } from '@microsoft/signalr';
import { Rate } from '../Models/Rate.model';

@Injectable({
  providedIn: 'root'
})
export class RateService {
  private baseurl="http://localhost:5018/api/Rate" ;
  constructor(public http:HttpClient) { }
 
  AddRate(rate:Rate)
  {
    this.http.post(this.baseurl+"/"+rate, {responseType:'text'});
  }
  

}
