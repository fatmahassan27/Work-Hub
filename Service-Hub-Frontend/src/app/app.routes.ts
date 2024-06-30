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
import { LogoutComponent } from './Account/logout/logout.component';
import { JobListComponent } from './job/job.component';
import { WorkerByJobComponent } from './worker-by-job/worker-by-job.component';
import { ProfileComponent } from './profile/profile.component';
import { OrderListComponent } from './Order/order-list/order-list.component';
import { UpdateOrderComponent } from './Order/update-order/update-order.component';
import { RateComponent } from './rate/rate.component';

export const routes: Routes = [
  { path: "home", component: HomeComponent },
  { path: "About", component: AboutComponent },
  { path: "Services", component: ServicesSectionComponent,canActivate:[loginGuard]},
  { path: "worker",component:WorkerComponent,canActivate:[loginGuard]},
  { path: "workerByJob",component:WorkerByJobComponent,canActivate:[loginGuard]},
  { path: "jobs",component:JobListComponent,canActivate:[loginGuard]},
  { path: "Notification", component: NotificationComponent ,canActivate:[loginGuard]},
  { path: "Chat/:id", component: ChatmessageComponent,canActivate:[loginGuard] },
  { path: "Chat", component: ChatmessageComponent,canActivate:[loginGuard] },
  { path: "login", component:LoginComponent},
  { path: "register", component: RegistrationFormComponent},
  { path: "logout", component: LogoutComponent },
  { path: "profile", component: ProfileComponent },

  //{ path: "order", component: OrUserComponent},
  { path: "logout", component: LogoutComponent,canActivate:[loginGuard]  },
  { path: "orders", component: OrderListComponent,canActivate:[loginGuard] },
  { path: "updateOrderStatus/:orderId", component: UpdateOrderComponent,canActivate:[loginGuard] },
  { path: "addRate/:userId/:workerId", component: RateComponent,canActivate:[loginGuard] },

  {path:'',redirectTo:'home',pathMatch:'full',title:'home'}, 
  {path:'**',component:NotFoundComponent,title:'NOT FOUND'}
];
