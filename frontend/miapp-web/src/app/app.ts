import { Component } from '@angular/core';
import { RouterLink, RouterOutlet } from '@angular/router';

import { AuthService } from './core/services/auth.service';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, RouterLink],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App {
  constructor(public authService: AuthService) {}

  canViewUsers(): boolean {
    const role = this.authService.getRole();

    return this.authService.isAuthenticated()
      && (role === 'Administrator' || role === 'Auditor');
  }
}