import { Component, OnInit } from '@angular/core';
import { WorkerService } from '../Services/worker.service';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { Worker } from '../Models/worker.model';
import { City } from '../Models/City.model';
import { CityService } from '../Services/city.service';
import { District } from '../Models/District.model';
import { DistrictService } from '../Services/district.service';
import { NotificationService } from '../Services/notification.service';
import { OrderService } from '../Services/order.service';
import { AccountService } from '../Services/account.service';
import { UserInfo } from '../interfaces/user-info';
import { Role } from '../enums/role';
import { RouterLink } from '@angular/router';
import { HubConnection } from '@microsoft/signalr';
import { Job } from '../Models/job.model';
import { JobService } from '../Services/job.service';


@Component({
  selector: 'app-worker',
  standalone: true,
  imports: [CommonModule, FormsModule,RouterLink],
templateUrl:'./worker.component.html',
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
    this.cityServices.getAll().subscribe((data: City[]) => {
      this.cities = data;
      //console.log('Cities:', this.cities); // Debug logging
    });

    this.workerService.getAll().subscribe((data: Worker[]) => {
      this.workers = data;
      this.filteredWorkers = data;
      //console.log('Workers:', this.workers); // Debug logging
    });

    this.currentUserInfo = this.accountService.currentUserValue;
    console.log(this.currentUserInfo);
    this.notificationService.startConnection();
    this.notificationService
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
    //debugger;
    // this.orderService.createOrder(this.currentUserInfo?.id!, workerId).subscribe({
    //   next: () => {
    //     console.log("Order created successfully."+`user id: ${this.currentUserInfo?.id!} , worker id: ${workerId}`);
    //   },
    //   error: (e) => {
    //     console.error(e);
    //     alert("Error creating order");
    //   },
    //   complete: () => {
    //     console.log("Order creation complete");
    //   }
    // });
    this.notificationService.sendOrderCreatedNotification(this.currentUserInfo?.id!, workerId);
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
 // this.notificationService.hubConnection.invoke("sendordercreatednotification",this.currentUserInfo?.id!, workerId)
        //   .then(() => {
        //     alert("Notification sent successfully");
        //   })
        //   .catch(err => {
        //     console.error('Error while sending notification: ', err);
        //     alert("Error sending notification" + err.message);
        //   })
        //   .finally(() => {
        //     console.log("Order made");
        //   });

            //await this.sendNotification(uId, workerId!);
    //console.log("notification created successfully."+`user id: ${uId} , worker id: ${workerId}`);

      // async sendNotification(userId:number,workerId:number){
  //   await this.notificationService.hubConnection.invoke("sendordercreatednotification", userId, workerId)
  //       .then(() => console.log('Service: Notification sent successfully'))
  //       .catch(err => {
  //         console.error('Service: Error while sending notification:', err);
  //         throw err;
  //       })
  // }