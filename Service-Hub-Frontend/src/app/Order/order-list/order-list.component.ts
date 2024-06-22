import { Component, OnInit } from '@angular/core';
import { AccountService } from './../../Services/account.service';
import { OrderService } from '../../Services/order.service';
import { Role } from '../../enums/role';
import { Order } from '../../Models/order';
import { CommonModule, DatePipe } from '@angular/common';
import { Router } from '@angular/router';
import { OrderStatus } from '../../enums/order-status';

@Component({
  selector: 'app-order-list',
  standalone: true,
  imports: [DatePipe,CommonModule],
  templateUrl: './order-list.component.html',
  styleUrls: ['./order-list.component.css']
})
export class OrderListComponent implements OnInit {
  orders: Order[] = [];
  userId: number = 0;
  userRole: Role = Role.User;
  OrderStatus = OrderStatus; // To use in the template

  constructor(
    public accountService: AccountService,
    public orderService: OrderService,
    public router: Router
  ) {}

  // ngOnInit(): void {
  //   this.userId = this.accountService.userInfo?.id!;
  //   this.userRole = this.accountService.userInfo?.role!;

  //   console.log(this.userId);
  //   console.log(this.userRole);

  //   if (this.userRole == Role.User) {
  //     this.orderService.getAllbyUserId(this.userId).subscribe({
  //       next: (data) => {
  //         this.orders = data;
  //         console.log(data);
  //       },
  //       error: (e) => console.log(e),
  //       complete: () => {
  //         console.log("user Orders loaded");
  //       }
  //     });
  //   }
  //   else if(this.userRole == Role.Worker)
  //     {
  //     this.orderService.getAllbyWorkerId(this.userId).subscribe({
  //       next: (data) => {
  //         this.orders = data;
  //         console.log(data);
  //       },
  //       error: (e) => console.log(e),
  //       complete: () => {
  //         console.log("worker Orders loaded");
  //       }
  //     });
  //   }
  // }
  ngOnInit(): void {
    this.userId = this.accountService.userInfo?.id!;
    this.userRole = this.accountService.userInfo?.role!;

    console.log('User ID:', this.userId);
    console.log('User Role:', this.userRole.toString());

    if (this.userRole.toString() === "User") {
      this.orderService.getAllbyUserId(this.userId).subscribe({
        next: (data) => {
          this.orders = data;
          console.log('Orders for User:', data);
        },
        error: (e) => {
          console.log('Error fetching orders for user:', e);
        },
        complete: () => {
          console.log('User Orders loaded');
        }
      });
    } else if (this.userRole.toString() === "Worker") {
      this.orderService.getAllbyWorkerId(this.userId).subscribe({
        next: (data) => {
          this.orders = data;
          console.log('Orders for Worker:', data);
        },
        error: (e) => {
          console.log('Error fetching orders for worker:', e);
        },
        complete: () => {
          console.log('Worker Orders loaded');
        }
      });
    }
  }

  updateOrder(orderId: number) {
    // Go to update order component with order id
    this.router.navigateByUrl(`/updateOrderStatus/${orderId}`);
  }

  addRate(userId: number, workerId: number) {
    // Go to add rate component with userid and workerid
    this.router.navigateByUrl(`/addRate/${userId}/${workerId}`);
  }
}
