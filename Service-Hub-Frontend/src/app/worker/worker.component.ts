import { Component, OnInit } from '@angular/core';
import { WorkerService } from '../Service/worker.service';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { Worker } from '../Models/worker.model';
import { City } from '../Models/City.model';
import { CityService } from '../Service/city.service';
import { District } from '../Models/District.model';
import { DistrictService } from '../Service/district.service';

@Component({
  selector: 'app-worker',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './worker.component.html',
  styleUrls: ['./worker.component.css']
})
export class WorkerComponent implements OnInit {
  workers: Worker[] = [];
  cities: City[] = [];
  districts: District[] = [];
  filteredWorkers: Worker[] = [];

  selectedCityId: number = 0;
  selectedDistrictId: number = 0;

  

  constructor(
    public workerService: WorkerService,
    public cityServices: CityService,
    public districtService: DistrictService
  ) {}

  ngOnInit() {
    this.cityServices.getAll().subscribe((c: City[]) => {
      console.log(c);
      this.cities = c;
    });

    this.workerService.getAll().subscribe((data: Worker[]) => {
      console.log(data);
      this.workers = data;
      this.filteredWorkers = data; // Initialize filteredWorkers with all workers
    });
  }

  onCityChange(event: Event) {
    const selectedCityId = (event.target as HTMLSelectElement).value;
    this.selectedCityId = +selectedCityId;
    this.loadDistricts(this.selectedCityId);
  }

  loadDistricts(cityId: number) {
    this.districtService.getAllByCityId(cityId).subscribe((districts: District[]) => {
      console.log(cityId);
      console.log(districts);
      this.districts = districts;
      this.selectedDistrictId = 0; // Reset selected district when city changes
      this.filteredWorkers = this.workers; // Reset filtered workers
    });
  }

  onDistrictChange(event: Event) {
    const selectedDistrictId = (event.target as HTMLSelectElement).value;
    this.selectedDistrictId = +selectedDistrictId;
    this.filterWorkersByDistrict(this.selectedDistrictId);
  }

  filterWorkersByDistrict(districtId: number) {
    if (districtId === 0) {
      // If no district selected, show all workers
      this.filteredWorkers = this.workers;
    } else {
      this.filteredWorkers = this.workers.filter(worker => worker.districtId === districtId);
    }
  }
}
