import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Job } from '../Models/job.model';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})

export class JobService {
  private baseurl="http://localhost:5018/api/Jobs";
  public tempJobId : number =0;
  constructor(public http:HttpClient) { }
  
  getAll(): Observable<Job[]>
  {
    return this.http.get<Job[]>(this.baseurl);
  }
  getById(id:number):Observable<Job>
  {
    return this.http.get<Job>(`${this.baseurl}/${id}`);
  }
  
}
