import { Component } from '@angular/core';
import { Router } from '@angular/router';

import { AuthService } from './services/auth.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'school-score';

  constructor(private authService: AuthService, private router: Router) { }

  Logout() {
    this.authService.logout();
    this.router.navigate(['/home']);
  }

  tokenExpired() {
    return this.authService.tokenExpired();
  }
}
