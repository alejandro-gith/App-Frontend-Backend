import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { RouterLink } from '@angular/router';

import { AuthService } from '../../../../core/services/auth.service';
import {
  ProductService,
  Product
} from '../../../../core/services/product.service';

import { ProductCardComponent }
  from './components/product-card/product-card.component';

@Component({
  selector: 'app-product-list',
  standalone: true,
  
  imports: [
    CommonModule,
    RouterLink,
    ProductCardComponent
  ],
  template: `
    <h1>Productos</h1>

    <p>Bienvenido, {{ authService.getUsername() }}</p>
    <p>Rol: {{ authService.getRole() }}</p>

    <button (click)="logout()">
      Cerrar sesión
    </button>

    <hr>

    <p *ngIf="loading">
      Cargando productos...
    </p>

    <p *ngIf="errorMessage">
      {{ errorMessage }}
    </p>

    <div
      *ngIf="!loading && products.length > 0"
      class="product-grid">

      <app-product-card
        *ngFor="let product of products"
        [product]="product">
      </app-product-card>

    </div>
    <div class="mb-3">
  <a routerLink="/catalog" class="btn btn-outline-secondary d-inline-flex align-items-center gap-2">
    <span>←</span> Volver al Catálogo
  </a>
</div>
  `
})
export class ProductList implements OnInit {

  products: Product[] = [];

  loading = true;

  errorMessage = '';

  constructor(
    public authService: AuthService,
    private productService: ProductService,
    private router: Router,
    private cdr: ChangeDetectorRef
  ) {}

  ngOnInit(): void {
    this.loadProducts();
  }

  loadProducts(): void {
  this.loading = true;
  this.errorMessage = '';

  this.productService.getProducts().subscribe({
    next: (products) => {
      this.products = products;
      this.loading = false;

      this.cdr.detectChanges();
    },
    error: () => {
      this.errorMessage =
        'No se pudieron cargar los productos.';

      this.loading = false;

      this.cdr.detectChanges();
    }
  });
}

  logout(): void {
    this.authService.logout();
    this.router.navigate(['/login']);
  }
}