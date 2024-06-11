import { Routes } from '@angular/router';
import { LoginComponent } from './Account/Login/login.component';
import { LogoutComponent } from './Account/logout/logout.component';

export const routes: Routes = [
    {path:'login', component:LoginComponent},
    {path:'logout',component:LogoutComponent}
];
