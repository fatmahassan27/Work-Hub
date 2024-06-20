import { Injectable } from '@angular/core';
import { HttpClient } from '@microsoft/signalr';
import { Observable } from 'rxjs';
import { Job } from '../Models/job.model';

@Injectable({
  providedIn: 'root'
})
export class JobService {
  private baseurl="http://localhost:5018/api/Jobs";
  constructor(public http:HttpClient) { }
  
  getAll():Observable<Job[]>
  {
    return this.http.get<Job[]>(this.baseurl);
  }
  getById(id:number)
  {
    return this.http.get(`${this.baseurl}/${id}`);
  }
  
}
