import { Component, OnInit } from '@angular/core';
import { Job } from '../Models/job';
import { JobService } from '../Services/job.service';

@Component({
  selector: 'app-job',
  standalone: true,
  imports: [],
  templateUrl: './job.component.html',
  styleUrl: './job.component.css'
})
export class JobListComponent implements OnInit {
  jobs!: Job[];
  job!: Job;

  constructor(private jobService: JobService) { }

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
      },
      error: (err) => {
        console.error('Error fetching job by id', err);
      }
    });
  }
}