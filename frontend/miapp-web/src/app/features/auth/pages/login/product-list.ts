import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';

import { AuthService } from '../../../../core/services/auth.service';
import { ProductCardComponent } from '../../components/product-card/product-card.component';

@Component({
  selector: 'app-products',
  standalone: true,
  imports: [CommonModule, ProductCardComponent],
  template: `
    <h1>Productos</h1>

    <p>Bienvenido, {{ authService.getUsername() }}</p>
    <p>Rol: {{ authService.getRole() }}</p>

    <button (click)="logout()">
      Cerrar sesión
    </button>

    <hr />

    <!-- Tarjetas con el botón de agregar al carrito -->
    <div class="product-grid" style="display: flex; gap: 1rem; flex-wrap: wrap; margin-top: 1rem;">
      <app-product-card 
        *ngFor="let prod of products" 
        [product]="prod">
      </app-product-card>
    </div>
  `
})
export class Products {
  // Lista de prueba para renderizar las tarjetas en pantalla
  products = [
    { id: 101, name: 'Producto 1', price: 25.00 },
    { id: 102, name: 'Producto 2', price: 50.00 },
    { id: 103, name: 'Producto 3', price: 15.00 }
  ];

  constructor(
    public authService: AuthService,
    private router: Router
  ) {}

  logout(): void {
    this.authService.logout();
    this.router.navigate(['/login']);
  }
}