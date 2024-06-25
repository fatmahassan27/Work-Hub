import { Component, OnInit } from '@angular/core';
import { RateService } from '../Services/rate.service';
import { Rate } from '../Models/Rate.model';
import { FormsModule } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { AccountService } from '../Services/account.service';

@Component({
  selector: 'app-rate',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './rate.component.html',
  styleUrls: ['./rate.component.css']
})
export class RateComponent implements OnInit {
  currentUserId = 0;
  userId: number = 0;
  workerId: number = 0;
  rate: Rate = new Rate(0, 0, "", 0, 0);

  constructor(
    public rateService: RateService,
    public activatedRoute: ActivatedRoute,
    public accountService: AccountService,
    public router: Router
  ) {}

  ngOnInit(): void {
    this.currentUserId = this.accountService.currentUserValue?.id!;
    console.log('Current User ID:', this.currentUserId);

    this.activatedRoute.params.subscribe({
      next: (params) => {
        this.userId = +params['userId'];
        this.workerId = +params['workerId'];
        console.log('User ID:', this.userId);
        console.log('Worker ID:', this.workerId);
        console.log('Parameters retrieved successfully');

        if (this.currentUserId == this.userId) {
          this.rate.FromUserId = this.userId;
          this.rate.ToUserId = this.workerId;
        } else if (this.currentUserId == this.workerId) {
          this.rate.FromUserId = this.workerId;
          this.rate.ToUserId = this.userId;
        }
        console.log('Rate Object after initialization:', this.rate);
      },
      error: (e) => console.log(e),
    });
  }

  addRate(): void {
    console.log('Adding Rate:', this.rate); // Check rate before sending
    this.rateService.AddRate(this.rate).subscribe({
      next: () => {
        console.log('Rate added successfully:', this.rate);
        alert("Thanks for The Rating!");
        this.router.navigateByUrl('/jobs');
      },
      error: (e) => console.log(e),
      complete: () => console.log('Add rate request complete')
    });
  }
}
