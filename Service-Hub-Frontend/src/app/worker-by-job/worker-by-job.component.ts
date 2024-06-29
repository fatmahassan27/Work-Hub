import { Component, OnInit } from '@angular/core';
import { JobService } from '../Services/job.service';
import { Worker } from '../Models/worker.model';
import { CommonModule } from '@angular/common';
import { WorkerService } from '../Services/worker.service';
import { City } from '../Models/City.model';

import { District } from '../Models/District.model';
import { DistrictService } from '../Services/district.service';
import { Role } from '../enums/role';
import { UserInfo } from '../interfaces/user-info';
import { CityService } from '../Services/city.service';
import { NotificationService } from '../Services/notification.service';
import { OrderService } from '../Services/order.service';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-worker-by-job',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './worker-by-job.component.html',
  styleUrl: './worker-by-job.component.css'
})
export class WorkerByJobComponent  implements OnInit {

  workers: Worker[] = [];
  filteredWorkers: Worker[] = [];
  selectedjobId: number | null = null; // Variable to hold the jobId as a number from jobService
  cities:City[] = [];
  districts:District[] = [];
  selectedCityId:number = 0;
  selectedDistrictId:number = 0;
  currentUserInfo:UserInfo | null  = {id:0,name:"",role:Role.User,email:"",jobId:'',districtId:''};
   constructor( public jobService:JobService , public workerService:WorkerService, public cityServices:CityService , public notificationService:NotificationService,public orderService:OrderService,public districService:DistrictService )
   {

   }
  ngOnInit(): void {

    this.cityServices.getAll().subscribe((data: City[]) => {
      this.cities = data;
    });

    this.selectedjobId=this.jobService.tempJobId;
     console.log(this.selectedjobId);
        this.workerService.getAllByJobId(this.selectedjobId).subscribe((data: Worker[]) => {
        this.workers = data;
        this.filteredWorkers = data; // Initialize filteredWorkers with all workers
      });

  }
  filterWorkersByDistrict(districtId: number) {
    if (districtId === 0) {
      // If no district selected, show all workers
      this.filteredWorkers = this.workers;
    } else {
      this.filteredWorkers = this.workers.filter(worker => worker.district.id === districtId);
      console.log(this.workers);
      console.log(this.filteredWorkers);
    }
  }

  filterWorkersByJobId(jobId:number|null) {
    if (jobId == null || jobId == 0) {
      this.filteredWorkers = this.workers;
    } else {
      this.filteredWorkers = this.workers.filter(worker => worker.job.id == jobId);
      console.log(jobId);

    }
  }

  CreateOrder(workerId: number) {
    console.log(this.currentUserInfo?.id!);
    this.orderService.createOrder(this.currentUserInfo?.id!, workerId).subscribe(
      {
        next: () => {
          console.log("Order created successfully");
          console.log(this.currentUserInfo?.id!, workerId);
          // this.notificationService.sendOrderCreatedNotification(this.currentUserInfo?.id!, workerId)
          //   .then(() => {
          //     alert("Notification sent successfully");
          //   })
          //   .catch((err) => {
          //     console.error('Error while sending notification: ', err);
          //     alert("Error sending notification" + err.message );
          //   })
          //   .finally(() => {
          //     console.log("Order made");
          //   });
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

}
