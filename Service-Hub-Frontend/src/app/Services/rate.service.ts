import { Injectable } from '@angular/core';
import { Rate } from '../Models/Rate.model';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class RateService {
  private baseurl = 'http://localhost:5018/api/Rate';

  constructor(public http: HttpClient) {}

  AddRate(rate: Rate): Observable<any> {
    return this.http.post(this.baseurl, rate, { responseType: 'text' });
  }
}
