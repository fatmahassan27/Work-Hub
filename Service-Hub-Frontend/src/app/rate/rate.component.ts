import { Component, OnInit } from '@angular/core';
import { RateService } from '../Services/rate.service';
import { Rate } from '../Models/Rate.model';
import { FormsModule } from '@angular/forms';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-rate',
  standalone: true,
  imports: [FormsModule,RouterLink],
  templateUrl: './rate.component.html',
  styleUrl: './rate.component.css'
})
export class RateComponent implements OnInit {
  rate : Rate = new Rate(0,0,"",0,0);
  constructor(public rateService:RateService)
  { }
  ngOnInit(){
    this.rateService.AddRate(this.rate)
  }
  addRate()
{
     this.rateService.AddRate(this.rate);
}
}

