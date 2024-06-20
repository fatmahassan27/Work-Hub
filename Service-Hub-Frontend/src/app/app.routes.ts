import { Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { AboutComponent } from './about/about.component';
import { NotificationComponent } from './notification/notification.component';
import { ChatmessageComponent } from './chatmessage/chatmessage.component';
import { LoginComponent } from './Account/login/login.component';
import { loginGuard } from './guards/login.guard';
import { RegistrationFormComponent } from './Account/register/register.component';
import { WorkerComponent } from './worker/worker.component';
import { ServicesSectionComponent } from './services-section/services-section.component';
import { NotFoundComponent } from './not-found/not-found.component';

export const routes: Routes = [
  { path: "home", component: HomeComponent },
  { path: "About", component: AboutComponent },
  { path: "Services", component: ServicesSectionComponent,canActivate:[loginGuard]},
  { path: "worker",component:WorkerComponent,canActivate:[loginGuard]},
  { path: "Notification", component: NotificationComponent ,canActivate:[loginGuard]},
  { path: "Chat", component: ChatmessageComponent,canActivate:[loginGuard] },
  { path: "login", component:LoginComponent },
  {path:"register",component:RegistrationFormComponent},
  {path:"**" , component:NotFoundComponent}
];
