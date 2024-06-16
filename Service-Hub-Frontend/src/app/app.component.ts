import { Component } from '@angular/core';
import { RouterLink, RouterOutlet } from '@angular/router';
import { HeaderComponent } from './Core/header/header.component';
import { FooterComponent } from './Core/footer/footer.component';
import { NotificationComponent } from './notification/notification.component';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet,RouterLink,HeaderComponent,FooterComponent,NotificationComponent],
templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  title = 'Service-Hub-Frontend';
}
