import { Component } from '@angular/core';
import { Router } from '@angular/router';

import { AuthService } from './services/auth.service';

@Component({
  selector: 'app-root',
  templateUrl: 'app.component.html',
  styleUrls: ['app.component.css']
})
export class AppComponent {
  public isLogged = false;
  public title = 'fullstack-portal';

  constructor(
    private router: Router,
    private authService: AuthService
  ) {
    this.authService.isLogged.subscribe(x => this.isLogged);
  }

  logout() {
    this.authService.logout();
  }
}
