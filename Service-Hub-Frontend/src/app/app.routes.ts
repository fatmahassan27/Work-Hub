import { Routes } from '@angular/router';
import { LogoutComponent } from './Account/logout/logout.component';
import { LoginComponent } from './Account/Login/login/login/login.component';

export const routes: Routes = [
    {path:'login', component:LoginComponent},
    {path:'logout',component:LogoutComponent}
];
