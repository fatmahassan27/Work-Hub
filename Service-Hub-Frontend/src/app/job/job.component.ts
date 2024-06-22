import { Component, OnInit } from '@angular/core';
import { JobService } from '../Services/job.service';
import { FormsModule } from '@angular/forms';
import { Job } from '../Models/job.model';
import { CommonModule } from '@angular/common';
import { WorkerService } from '../Services/worker.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-job',
  standalone: true,
  imports: [FormsModule,CommonModule],
  templateUrl: './job.component.html',
  styleUrl: './job.component.css'
})
export class JobListComponent implements OnInit {
  jobs!: Job[];
  job!: Job;
  constructor(private jobService: JobService, public router:Router) { }

  ngOnInit(): void {
    this.jobService.getAll().subscribe({
      next: (data: Job[]) => {
        this.jobs = data;
      },
      error: (err) => {
        console.error('Error fetching jobs', err);
      }
    });   
    // Example usage of getById
    this.jobService.getById(1).subscribe({
      next: (data: Job) => {
        this.job = data;
        console.log(this.job);
      },
      error: (err) => {
        console.error('Error fetching job by id', err);
      }
    });
  }

  
   public ShowWorkers(jobId:number) 
    {
       this.jobService.tempJobId=jobId;
       this.router.navigateByUrl("/workerByJob");
    }
   
}