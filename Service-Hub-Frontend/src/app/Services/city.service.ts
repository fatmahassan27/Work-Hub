import { Injectable } from '@angular/core';
import { City } from '../Models/City.model';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { WorkerComponent } from '../worker/worker.component';

@Injectable({
  providedIn: 'root'
})

export class CityService {
  private baseurl="http://localhost:5018/api/city";
  
  getAll():Observable<City[]>
  {
    return this.http.get<City[]>(this.baseurl);
  }

  constructor(public http:HttpClient) { }

}
