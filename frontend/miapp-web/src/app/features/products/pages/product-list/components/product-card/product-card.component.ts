import { Component, Input, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { CartService } from '../../../../../../core/services/cart.service';
import { AuthService } from '../../../../../../core/services/auth.service';

@Component({
  selector: 'app-product-card',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './product-card.component.html',
  styleUrls: ['./product-card.component.css']
})
export class ProductCardComponent implements OnInit {
  @Input() product: any;
  cartForm!: FormGroup;
  isAuditor: boolean = false;
  message: string = '';

  constructor(
    private fb: FormBuilder,
    private cartService: CartService,
    private authService: AuthService
  ) {}

  ngOnInit(): void {
    this.cartForm = this.fb.group({
      quantity: [1, [Validators.required, Validators.min(1)]]
    });

    // Comprobar si el usuario actual es Auditor
    const currentUser = this.authService.getCurrentUser();
    this.isAuditor = currentUser?.role === 'Auditor';
  }

  onAddToCart(): void {
  if (this.cartForm.invalid || this.isAuditor) {
    return;
  }

  const currentUser = this.authService.getCurrentUser();

  if (!currentUser) {
    this.message = 'No hay una sesión activa.';
    return;
  }

  const request = {
    userId: currentUser.id,
    productId: this.product.id,
    quantity: this.cartForm.value.quantity
  };

  this.cartService.addToCart(request).subscribe({
    next: () => {
      this.message = '¡Producto añadido al carrito con éxito!';
    },
    error: (err) => {
      this.message =
        err.error?.message ||
        'Error al agregar el producto.';
    }
  });
}
}