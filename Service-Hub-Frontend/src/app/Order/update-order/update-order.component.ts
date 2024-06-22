import { Component, OnInit } from '@angular/core';
import { OrderService } from './../../Services/order.service';
import { OrderStatus } from '../../enums/order-status';
import { Role } from '../../enums/role';
import { AccountService } from '../../Services/account.service';
import { FormsModule } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-update-order',
  standalone: true,
  imports: [FormsModule,CommonModule],
  templateUrl: './update-order.component.html',
  styleUrls: ['./update-order.component.css']
})
export class UpdateOrderComponent implements OnInit {
  currentUserRole: Role = Role.User;
  orderId: number = 0;
  newStatus: OrderStatus = OrderStatus.Done;
  OrderStatus = OrderStatus; // To use in the template

  constructor(
    public orderService: OrderService,
    public accountService: AccountService,
    public router: Router,
    public activatedRoute: ActivatedRoute
  ) {}

  ngOnInit(): void {
    this.currentUserRole = this.accountService.userInfo?.role!;
    console.log('Current User Role:', this.currentUserRole);

    this.activatedRoute.params.subscribe({
      next: (params) => {
        this.orderId = +params['orderId']; // Convert string to number
        console.log('Order ID:', this.orderId);
      },
      error: (e) => console.log(e),
      complete: () => console.log('Route reading complete')
    });
  }

  Update() {
    this.orderService.updateOrder(this.orderId, this.newStatus).subscribe({
      next: (d) => {
        console.log('Updating order');
        console.log(d);
      },
      error: (e) => {
        console.log(e);
      },
      complete: () => {
        console.log('Update complete');
        this.router.navigateByUrl('/orders');
      }
    });
  }
}
