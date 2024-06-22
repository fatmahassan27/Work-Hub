import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Order } from '../Models/order';
import { OrderStatus } from '../enums/order-status';
import { Observable } from 'rxjs';
import { tap } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class OrderService {
  baseUrl = 'http://localhost:5018/api/Orders/';

  constructor(public http: HttpClient) {}

  createOrder(userId: number, workerId: number): Observable<any> {
    return this.http.post(`${this.baseUrl}${userId}/${workerId}`, { responseType: 'text' });
  }

  getAllbyUserId(userId: number): Observable<Order[]> {
    console.log(`Fetching orders for user: ${userId}`);
    return this.http.get<Order[]>(`${this.baseUrl}user/${userId}`).pipe(
      tap((orders) => console.log(`Orders fetched for user:`, orders))
    );
  }

  getAllbyWorkerId(workerId: number): Observable<Order[]> {
    console.log(`Fetching orders for worker: ${workerId}`);
    return this.http.get<Order[]>(`${this.baseUrl}worker/${workerId}`).pipe(
      tap((orders) => console.log(`Orders fetched for worker:`, orders))
    );
  }

  updateOrder(orderId: number, newStatus: OrderStatus): Observable<any> {
    return this.http.put(`${this.baseUrl}${orderId}/${newStatus}`, {},{responseType: 'text'});
  }
}
