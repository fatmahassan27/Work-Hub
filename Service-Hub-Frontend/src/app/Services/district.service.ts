import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { District } from '../Models/District.model';

@Injectable({
  providedIn: 'root'
})
export class DistrictService {
  private baseurl = "http://localhost:5018/api/District/";
  
  getAll(): Observable<District[]>{
    return this.http.get<District[]>(this.baseurl);
  }

  getAllByCityId(id:number): Observable<District[]> {
    return this.http.get<District[]>(this.baseurl+id);
  }
  
  getById(id:number):Observable<District>{
    return this.http.get<District>(this.baseurl+"ById/"+id);
  }
  
  constructor(public http: HttpClient) { }
}
