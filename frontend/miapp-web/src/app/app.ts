import { Component } from '@angular/core';
import { Router, RouterLink, RouterOutlet } from '@angular/router';

import { AuthService } from './core/services/auth.service';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, RouterLink],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App {

  constructor(
    public authService: AuthService,
    private router: Router
  ) {}

  canViewUsers(): boolean {
    const role = this.authService.getRole();

    return this.authService.isAuthenticated()
      && (role === 'Administrator' || role === 'Auditor');
  }

  canViewCarts(): boolean {
    const role = this.authService.getRole();

    return this.authService.isAuthenticated()
      && (role === 'Administrator' || role === 'Auditor');
  }

  logout(): void {
    this.authService.logout();
    this.router.navigate(['/login']);
  }
}