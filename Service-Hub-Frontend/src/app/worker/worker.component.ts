import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { RouterLink } from '@angular/router';
import { WorkerService } from '../Services/worker.service';
import { CityService } from '../Services/city.service';
import { DistrictService } from '../Services/district.service';
import { NotificationService } from '../Services/notification.service';
import { OrderService } from '../Services/order.service';
import { AccountService } from '../Services/account.service';
import { Worker } from '../Models/worker.model';
import { City } from '../Models/City.model';
import { District } from '../Models/District.model';
import { UserInfo } from '../interfaces/user-info';
import { Role } from '../enums/role';
import { lastValueFrom } from 'rxjs';

@Component({
  selector: 'app-worker',
  standalone: true,
  imports: [CommonModule, FormsModule, RouterLink],
  templateUrl: './worker.component.html',
  styleUrls: ['./worker.component.css']
})
export class WorkerComponent implements OnInit {
  workers: Worker[] = [];
  cities: City[] = [];
  districts: District[] = [];
  filteredWorkers: Worker[] = [];
  selectedCityId: number = 0;
  selectedDistrictId: number = 0;
  selectedjobId: number | null = null;
  currentUserInfo: UserInfo | null = { id: 0, name: "", role: Role.User, email: "", jobId: '', districtId: '' };

  constructor(
    public workerService: WorkerService,
    public cityServices: CityService,
    public districtService: DistrictService,
    public notificationService: NotificationService,
    public orderService: OrderService,
    public accountService: AccountService
  ) { }

  ngOnInit() {
    // Fetch cities
    this.cityServices.getAll().subscribe((data: City[]) => {
      this.cities = data;
      //console.log('Cities:', this.cities); // Debug logging
    });

    // Fetch workers
    this.workerService.getAll().subscribe((data: Worker[]) => {
      this.workers = data;
      this.filteredWorkers = data;
      //console.log('Workers:', this.workers); // Debug logging
    });

    // Set current user info
    this.currentUserInfo = this.accountService.currentUserValue;
    console.log(this.currentUserInfo);

  
  }

  onCityChange(event: any) {
    this.selectedCityId = Number(event.target.value); // Ensure this is a number
    console.log('Selected City ID:', this.selectedCityId); // Debug logging
    this.districtService.getAllByCityId(this.selectedCityId).subscribe((data: District[]) => {
      this.districts = data;
      console.log('Districts:', this.districts); // Debug logging
    });
  }

  onDistrictChange(event: any) {
    this.selectedDistrictId = Number(event.target.value); // Ensure this is a number
    console.log('Selected District ID:', this.selectedDistrictId); // Debug logging
    this.filterWorkersByDistrict(this.selectedDistrictId);
  }

  filterWorkersByDistrict(districtId: number) {
    console.log('Filtering workers by District ID:', districtId); // Debug logging
    this.filteredWorkers = this.workers.filter(worker => {
      console.log('Worker District ID:', worker.district.id); // Debug logging
      return worker.district.id === districtId;
    });
    console.log('Filtered Workers:', this.filteredWorkers); // Debug logging
  }

  async CreateOrder(workerId: number) {
    try {
      // Ensure the current user ID and worker ID are valid
      if (!this.currentUserInfo?.id) {
        console.error("Current user ID is not available.");
        alert("Error creating order: User ID is missing.");
        return;
      }

      // Create the order
      await lastValueFrom(this.orderService.createOrder(this.currentUserInfo.id, workerId));
      
      // Log success
      console.log(`Order created successfully. User ID: ${this.currentUserInfo.id}, Worker ID: ${workerId}`);

      // After creating the order, send a notification
      await this.notificationService.sendOrderCreatedNotification(this.currentUserInfo.id, workerId);

      // Notify success
      console.log('Notification sent successfully.');
      alert("Order created and notification sent successfully.");

    } catch (error) {
      // Handle errors during order creation or notification sending
      console.error('Error while creating order or sending notification:', error);
      alert("Error creating order or sending notification.");
    }
  }
}