import { Routes } from '@angular/router';
import { LoginComponent } from './Account/Login/login/login.component';
import { LogoutComponent } from './Account/Login/Logout/logout/logout.component';

export const routes: Routes = [
    {path:'login', component:LoginComponent},
    {path:'logout',component:LogoutComponent}
];
