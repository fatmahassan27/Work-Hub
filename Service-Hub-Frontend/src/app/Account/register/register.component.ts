import { Component, OnInit } from '@angular/core';
import { City } from '../../Models/City.model';
import { Subscription } from 'rxjs';
import { Router, RouterLink } from '@angular/router';
import { CityService } from '../../Services/city.service';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { CommonModule } from '@angular/common';
import { District } from '../../Models/District.model';
import { AccountService } from '../../Services/account.service';
import { DistrictService } from '../../Services/district.service';
import { Role } from '../../enums/role';
import { RegisterationDTO } from '../../Models/registration-dto';


@Component({
  selector: 'app-register',
  standalone: true,
  imports: [RouterLink, FormsModule, HttpClientModule, CommonModule],
templateUrl: './register.component.html',
  styleUrl: './register.component.css'
})
export class RegistrationFormComponent implements OnInit{

  sub: Subscription | null = null;
  RegisterDTO: RegisterationDTO = new RegisterationDTO("", "", "", "", Role.User, 0,0);
  districts: District[] =[];
  selectedRole: Role = Role.User;
  //jobs:
  
  constructor(
    private accountService: AccountService,
    public cityservice:CityService,
    public districtService:DistrictService,
    public router:Router
    //jobs
  ) { }

  ngOnInit(): void {
    // this.cityservice.getAll().subscribe((c: City[]) => {
    //   console.log(c);
    //   this.cities = c;
    // });

    this.loadDistricts();

    // if (this.selectedCityId !== null) {
    //   this.loadDistricts(this.selectedCityId);
    // }
  }

  // onSelectCity(event: Event): void {
  //   const selectElement = event.target as HTMLSelectElement;
  //   this.selectedCityId = Number(selectElement.value);
  //   if (this.selectedCityId !== null) {
  //     this.loadDistricts(this.selectedCityId);
  //   }
  // }

  // onSelectDistrict(event: Event): void {
  //   const selectElement = event.target as HTMLSelectElement;
  //   this.selectedDistrictId = Number(selectElement.value);
  // }

  loadDistricts(): void {
    this.districtService.getAll().subscribe((data: District[]) => {
      console.log(data);
      this.districts = data;
    });
  }

  save(): void {
    console.log(this.RegisterDTO);
    //if user
    this.sub = this.accountService.register(this.RegisterDTO).subscribe((data) => {
      console.log(data);
      this.router.navigateByUrl("/Home");
    });

  }

  ngOnDestroy(): void {
    this.sub?.unsubscribe();
  }
}
