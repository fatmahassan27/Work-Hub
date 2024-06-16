import { Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { AboutComponent } from './about/about.component';
import { ServicesComponent } from './Services/services.component';
import { NotificationComponent } from './notification/notification.component';
import { ChatmessageComponent } from './chatmessage/chatmessage.component';
import { loginGuard } from './guards/login.guard';

export const routes: Routes = [
  { path: "home", component: HomeComponent },
  { path: "About", component: AboutComponent },
  { path: "Services", component: ServicesComponent,canActivate:[loginGuard] },
  { path: "Notification", component: NotificationComponent,canActivate:[loginGuard]},
  { path: "Chat", component: ChatmessageComponent,canActivate:[loginGuard] }
];
