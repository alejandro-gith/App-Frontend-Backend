import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';

import { ProductService } from '../product.service';
import { ProductDetail } from '../../../../../core/models/product.model';
import { CartService } from '../../../../../core/services/cart.service';
import { AuthService } from '../../../../../core/services/auth.service';

@Component({
  selector: 'app-detail',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './detail.component.html',
  styleUrl: './detail.component.css'
})
export class DetailComponent implements OnInit {

  product: ProductDetail | null = null;
  error: string = '';

  // Roles
  isAdmin: boolean = false;
  isClient: boolean = false;

  // US09 - Carrito
  cartMessage: string = '';
  cartError: string = '';
  addingToCart: boolean = false;

  // US08 - Eliminación
  showConfirm: boolean = false;
  deleting: boolean = false;
  deleted: boolean = false;
  deleteError: string = '';

  constructor(
    private route: ActivatedRoute,
    private productService: ProductService,
    private cdr: ChangeDetectorRef,
    private authService: AuthService,
    private cartService: CartService,
    private router: Router
  ) {}

  ngOnInit(): void {
    const role = this.authService.getRole();

    this.isAdmin = role === 'Administrator';
    this.isClient = role === 'Client';

    const idParam = this.route.snapshot.paramMap.get('id');

    if (idParam) {
      this.loadDetail(Number(idParam));
    }
  }

  // Cargar detalle del producto
  loadDetail(id: number): void {
    this.productService.getProductById(id).subscribe({
      next: (data: ProductDetail) => {
        this.product = data;
        this.cdr.markForCheck();
      },
      error: () => {
        this.error = 'Producto no encontrado';
        this.cdr.markForCheck();
      }
    });
  }

  // US09 - Añadir producto al carrito
  addToCart(): void {
    if (!this.product || !this.isClient) {
      return;
    }

    const currentUser = this.authService.getCurrentUser();

    if (!currentUser) {
      this.cartError = 'No hay una sesión activa.';
      return;
    }

    this.addingToCart = true;
    this.cartMessage = '';
    this.cartError = '';

    this.cartService.addToCart({
      userId: currentUser.id,
      productId: this.product.id,
      quantity: 1
    }).subscribe({
      next: () => {
        this.addingToCart = false;
        this.cartMessage = 'Producto añadido al carrito correctamente.';
        this.cdr.markForCheck();
      },
      error: (err) => {
        this.addingToCart = false;

        this.cartError =
          err.error?.message ??
          'No se pudo añadir el producto al carrito.';

        this.cdr.markForCheck();
      }
    });
  }

  // US08 - Mostrar confirmación de eliminación
  askDelete(): void {
    this.deleteError = '';
    this.showConfirm = true;
  }

  // US08 - Cancelar eliminación
  cancelDelete(): void {
    this.showConfirm = false;
  }

  // US08 - Confirmar eliminación
  confirmDelete(): void {
    if (!this.product) {
      return;
    }

    this.deleting = true;
    this.deleteError = '';

    this.productService.deleteProduct(this.product.id).subscribe({
      next: () => {
        this.deleting = false;
        this.showConfirm = false;
        this.deleted = true;

        this.cdr.markForCheck();

        setTimeout(() => {
          this.router.navigate(['/catalog']);
        }, 1200);
      },
      error: (err: any) => {
        this.deleting = false;
        this.deleteError = this.getDeleteErrorMessage(err);
        this.cdr.markForCheck();
      }
    });
  }

  // US08 - Mensajes de error
  private getDeleteErrorMessage(err: any): string {
    if (err.status === 0) {
      return 'No se pudo conectar con el servidor.';
    }

    if (err.status === 401) {
      return 'Tu sesión expiró. Inicia sesión de nuevo.';
    }

    if (err.status === 403) {
      return 'No tienes permiso para eliminar productos.';
    }

    if (err.status === 404) {
      return 'El producto ya no existe.';
    }

    return 'Ocurrió un error inesperado al eliminar el producto.';
  }
}