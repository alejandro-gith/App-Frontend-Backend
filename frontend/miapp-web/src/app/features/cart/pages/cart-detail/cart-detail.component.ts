import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { CartService } from '../../../../core/services/cart.service';
import { AuthService } from '../../../../core/services/auth.service';
import { Cart } from '../../../../core/models/cart.model';
@Component({
  selector: 'app-cart-detail',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './cart-detail.component.html',
  styleUrls: ['./cart-detail.component.css']
})
export class CartDetailComponent implements OnInit {
  cart: Cart | null = null;
  totalAmount: number = 0;
  userId: number = 0;
  loading: boolean = true;
  message: string = '';

  constructor(
    private cartService: CartService,
    private authService: AuthService
  ) {}

  ngOnInit(): void {
    const currentUser = this.authService.getCurrentUser();
    this.userId = currentUser?.id || 1; // Fallback para pruebas
    this.loadCart();
  }

  loadCart(): void {
    this.loading = true;
    this.cartService.getCart(this.userId).subscribe({
      next: (data) => {
        this.cart = data;
        this.calculateTotal();
        this.loading = false;
      },
      error: () => {
        this.cart = null;
        this.totalAmount = 0;
        this.loading = false;
      }
    });
  }

  // US10: Incrementar o decrementar cantidad
  updateQuantity(productId: number, currentQuantity: number, change: number): void {
    const newQuantity = currentQuantity + change;

    this.cartService.updateItemQuantity(this.userId, { productId, quantity: newQuantity }).subscribe({
      next: (updatedCart) => {
        this.cart = updatedCart;
        this.calculateTotal();
      },
      error: (err) => {
        this.message = err.error?.message || 'Error al actualizar la cantidad.';
      }
    });
  }

  // US10: Eliminar producto
  removeItem(productId: number): void {
    this.cartService.removeItem(this.userId, productId).subscribe({
      next: (updatedCart) => {
        this.cart = updatedCart;
        this.calculateTotal();
      },
      error: (err) => {
        this.message = err.error?.message || 'Error al eliminar el producto.';
      }
    });
  }

  // US10: Cálculo del Total con 2 decimales
  calculateTotal(): void {
  if (!this.cart || !this.cart.products || this.cart.products.length === 0) {
    this.totalAmount = 0;
    return;
  }

  const rawTotal = this.cart.products.reduce((sum, item) => {
    return sum + (item.unitPrice * item.quantity);
  }, 0);

  this.totalAmount = Math.round(rawTotal * 100) / 100;
}
}