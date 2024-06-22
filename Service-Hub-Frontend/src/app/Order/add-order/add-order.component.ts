import { Component } from '@angular/core';
import { Order } from './../../Models/order';

@Component({
  selector: 'app-add-order',
  standalone: true,
  imports: [],
templateUrl: './add-order.component.html',
  styleUrl: './add-order.component.css'
})
export class AddOrderComponent {

  orders :  Order[] = [];
  
}
