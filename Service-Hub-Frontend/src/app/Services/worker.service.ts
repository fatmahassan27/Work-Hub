import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Worker } from '../Models/worker.model';
import { CityService } from './city.service';
import { City } from '../Models/City.model';

@Injectable({
  providedIn: 'root'
})
export class WorkerService {
  private baseurl = "http://localhost:5018/api/Worker/";

  workers: Worker[] = [];

  constructor( public cityServices: CityService,public http:HttpClient) { }
  
  getAll(): Observable<Worker[]> {
    return this.http.get<Worker[]>(this.baseurl);
  }

  getAllByDistrictId(id: number): Observable<Worker[]> {
    return this.http.get<Worker[]>(this.baseurl+"district/"+ id);
  }


  ngOnInit() {
    // this.workerService.getAll().subscribe((data: Worker[]) => {
    //   console.log(data);
    //   this.workers = data;
    // })

    //   this.cityServices.getAll().subscribe((c: City[]) => {
    //     console.log(c);
    //     this.cities = c;
    //   })
      
    }
    

  }

