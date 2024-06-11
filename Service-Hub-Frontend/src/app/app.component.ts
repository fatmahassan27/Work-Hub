import { Component } from '@angular/core';
import { RouterLink, RouterOutlet } from '@angular/router';
import { WorkerListComponent } from './Worker/worker-list/worker-list.component';
import { LoginComponent } from './Account/Login/login.component';
import { RegisterComponent } from './Account/register/register.component';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet,WorkerListComponent,LoginComponent,RouterLink,RegisterComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  title = 'Service-Hub-Frontend';
}
