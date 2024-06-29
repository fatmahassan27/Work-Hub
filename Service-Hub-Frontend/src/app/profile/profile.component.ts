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
  workerId=this.accountservice.currentUserValue?.id
  woker  = new Worker(7, "Fatma", "fatma@gmail.com",1,new Job(0,"",200),new District("",0));
  constructor( public wokerService:WorkerService,public accountservice:AccountService)
 {
 
 }
  ngOnInit(): void {
    throw new Error('Method not implemented.');
  }
}
