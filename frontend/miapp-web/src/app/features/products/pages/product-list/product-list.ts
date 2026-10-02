import { Component } from '@angular/core';
import { Router } from '@angular/router';

import { AuthService } from '../../../../core/services/auth.service';

@Component({
  selector: 'app-product-list',
  standalone: true,
  template: `
    <h1>Productos</h1>

    <p>Bienvenido, {{ authService.getUsername() }}</p>
    <p>Rol: {{ authService.getRole() }}</p>

    <button (click)="logout()">
      Cerrar sesión
    </button>
  `
})
export class ProductList {

  constructor(
    public authService: AuthService,
    private router: Router
  ) {}

  logout(): void {
    this.authService.logout();
    this.router.navigate(['/login']);
  }
}