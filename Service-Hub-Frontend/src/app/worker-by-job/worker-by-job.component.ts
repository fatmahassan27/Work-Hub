import { Component, OnInit } from '@angular/core';
import { JobService } from '../Services/job.service';
import { Worker } from '../Models/worker.model';
import { CommonModule } from '@angular/common';
import { WorkerService } from '../Services/worker.service';

@Component({
  selector: 'app-worker-by-job',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './worker-by-job.component.html',
  styleUrl: './worker-by-job.component.css'
})
export class WorkerByJobComponent  implements OnInit {
 
  workers: Worker[] = [];
  filteredWorkers: Worker[] = [];
  selectedjobId: number |null = null; // Variable to hold the jobId as a number from jobService
   constructor( public jobService:JobService , public workerService:WorkerService  )
   {

   }
  ngOnInit(): void {

    this.selectedjobId=this.jobService.tempJobId;
     console.log(this.selectedjobId);
        this.workerService.getAllByJobId(this.selectedjobId).subscribe((data: Worker[]) => {
        this.workers = data;
        this.filteredWorkers = data; // Initialize filteredWorkers with all workers
      });

  }
   
  filterWorkersByJobId(jobId:number|null) {
    if (jobId == null || jobId == 0) {
      this.filteredWorkers = this.workers;
    } else {
      this.filteredWorkers = this.workers.filter(worker => worker.jobId == jobId);
      console.log(jobId);

    }
  }
    
}
