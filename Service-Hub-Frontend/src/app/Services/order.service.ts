import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class OrderService {

  baseUrl = "http://localhost:5018/api/Orders/" ;
  constructor(public http:HttpClient) { }

  createOrder(userId:number,workerId:number){
    return this.http.post(`${this.baseUrl}${userId}/${workerId}`,{ responseType: 'text' });
  }
}
