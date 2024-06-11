import { Component } from '@angular/core';
import { RouterLink, RouterOutlet } from '@angular/router';
import { WorkerListComponent } from './Worker/worker-list/worker-list.component';
import { RegisterComponent } from './Account/register/register.component';
import { LoginComponent } from './Account/Login/login/login.component';
import { HeaderComponent } from './Core/header/header.component';
import { FooterComponent } from './Core/footer/footer.component';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet,WorkerListComponent,LoginComponent,RouterLink,RegisterComponent],
  imports: [RouterOutlet,RouterLink,HeaderComponent,FooterComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  title = 'Service-Hub-Frontend';
}
