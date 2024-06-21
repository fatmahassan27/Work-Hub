import { Component, OnInit } from '@angular/core';
import { WorkerService } from '../Services/worker.service';
import { FormsModule, NgModel } from '@angular/forms';
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
import { JobService } from '../Services/job.service';

@Component({
  selector: 'app-worker',
  standalone: true,
  imports: [CommonModule, FormsModule],
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
  selectedjobId: number |null = null; // Variable to hold the jobId as a number from jobService

  currentUserInfo : UserInfo | null  = {id:0,name:"",role:Role.User,email:"",jobId:'',districtId:''};

  constructor(
    public workerService: WorkerService,
    public cityServices: CityService,
    public districtService: DistrictService,
    public notificationService: NotificationService,
    public orderService: OrderService,
    public accountService: AccountService,
   // public jobService:JobService
  ) {}

  ngOnInit() {
    this.cityServices.getAll().subscribe((data: City[]) => {
      this.cities = data;
    });

    this.workerService.getAll().subscribe((data: Worker[]) => {
      this.workers = data;
      this.filteredWorkers = data; // Initialize filteredWorkers with all workers
    });
    this.currentUserInfo = this.accountService.userInfo ;
    //  this.selectedjobId=this.jobService.tempJobId;
    //  console.log(this.selectedjobId);
    //  this.filterWorkersByJobId(this.selectedjobId);
  }

  onCityChange(event: Event) {
    const selectedCityId = (event.target as HTMLSelectElement).value;
    this.selectedCityId = +selectedCityId;
    this.loadDistricts(this.selectedCityId);
  }

  loadDistricts(cityId: number) {
    this.districtService.getAllByCityId(cityId).subscribe((districts: District[]) => {
      console.log(cityId);
      console.log(districts);
      this.districts = districts;
      this.selectedDistrictId = 0; // Reset selected district when city changes
      this.filteredWorkers = this.workers; // Reset filtered workers
    });
  }

  onDistrictChange(event: Event) {
    const selectedDistrictId = (event.target as HTMLSelectElement).value;
    this.selectedDistrictId = +selectedDistrictId;
    this.filterWorkersByDistrict(this.selectedDistrictId);
  }

  filterWorkersByDistrict(districtId: number) {
    if (districtId === 0) {
      // If no district selected, show all workers
      this.filteredWorkers = this.workers;
    } else {
      this.filteredWorkers = this.workers.filter(worker => worker.districtId === districtId);
    }
  }
   
  CreateOrder(workerId: number) {
    console.log(this.currentUserInfo?.id!);
    this.orderService.createOrder(this.currentUserInfo?.id!, workerId).subscribe(
      {
        next: () => {
          console.log("Order created successfully");
          console.log(this.currentUserInfo?.id!, workerId);
          this.notificationService.sendOrderCreatedNotification(this.currentUserInfo?.id!, workerId)
            .then(() => {
              alert("Notification sent successfully");
            })
            .catch(err => {
              console.error('Error while sending notification: ', err);
              alert("Error sending notification" + err.message );
            })
            .finally(() => {
              console.log("Send button clicked");
            });
        },
        error: (e) => {
          console.error(e);
          alert("Error creating order");
        },
        complete: () => {
          console.log("Order creation complete");
        }
      });
  }
    
 
  // filterWorkersByJobId(jobId:number|null) {
  //   if (jobId == null || jobId == 0) {
  //     this.filteredWorkers = this.workers;
  //   } else {
  //     this.filteredWorkers = this.workers.filter(worker => worker.jobId == jobId);
  //     console.log(jobId);

  //   }
  // }
    
}
