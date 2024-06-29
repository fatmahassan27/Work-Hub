import { Component, OnInit } from '@angular/core';
import { RouterLink, RouterLinkActive } from '@angular/router';
import { WorkerService } from '../Services/worker.service';
import { AccountService } from '../Services/account.service';
import { Worker } from '../Models/worker.model'; // Adjust the import path as needed
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Job } from '../Models/job.model';
import { District } from '../Models/District.model';


@Component({
  selector: 'app-profile',
  standalone: true,
  imports: [RouterLink,RouterLinkActive,CommonModule,FormsModule],
  templateUrl: './profile.component.html',
  styleUrl: './profile.component.css'
})
export class ProfileComponent implements OnInit 
{
  workerId:number|null=this.accountservice.currentUserValue?.id ||null;
  worker: Worker = new Worker(0, "", "", 0, new Job(0, "", 0), new District("", 0)); // Initialize with default values
  constructor( public wokerService:WorkerService,
    public accountservice:AccountService)
 {}
 ngOnInit(): void {
  if (this.workerId) {
    console.log(this.workerId);
    this.wokerService.getWorkerById(this.workerId).subscribe({
      next: (data) => {
        this.worker = data;
        console.log('Worker data fetched:', this.worker); // Debug logging
      },
      error: (err) => {
        console.error('Error fetching worker data:', err);
      }
    });
  } else {
    console.error('No worker ID found.');
  }
}


}

