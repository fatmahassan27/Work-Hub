import { Component } from '@angular/core';

@Component({
  selector: 'app-worker-list',
  standalone: true,
  imports: [],
  templateUrl: './worker-list.component.html',
  styleUrl: './worker-list.component.css'
})
export class WorkerListComponent {
  workers : Worker[]=[
    // اللي راجع من الداتا بيز 
  ]

}
