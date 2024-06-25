import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { HeaderComponent } from './core/header/header.component';
import { FooterComponent } from './core/footer/footer.component';
import { AccountService } from './Services/account.service';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet,HeaderComponent,FooterComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})

export class AppComponent {
  title = 'Service-Hub';

  constructor(public accountService: AccountService) {
    const currentUser = accountService.currentUserValue;

    if (currentUser) {
      console.log(`Online: ${currentUser.role} => ${currentUser.name} `);
    }
  }
}
