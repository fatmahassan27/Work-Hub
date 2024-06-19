import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class LocationService {
  private citiesUrl = 'http://localhost:5018/api/city'; // Replace with your API endpoint
  private districtsUrl = 'http://localhost:5018/api/district/cityid'; // Replace with your API endpoint

  constructor(private http: HttpClient) { }

  getCities(): Observable<any> {
    return this.http.get<any>(this.citiesUrl);
  }

  getDistricts(cityId: number): Observable<any> {
    return this.http.get<any>(`${this.districtsUrl}?cityId=${cityId}`);
  }
}
