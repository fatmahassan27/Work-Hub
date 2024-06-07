import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { WorkerListComponent } from './Worker/worker-list/worker-list.component';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet,WorkerListComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  title = 'Service-Hub-Frontend';
}
