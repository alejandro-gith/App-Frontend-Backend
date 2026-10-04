import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute, Router, RouterModule } from '@angular/router'; // US08: Router
import { ProductService } from '../product.service';
import { ProductDetail } from '../../../../../core/models/product.model';
import { AuthService } from '../../../../../core/services/auth.service'; // US07

@Component({
  selector: 'app-detail',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './detail.component.html',
  styleUrl: './detail.component.css' // style: archivo de estilos del detalle
})
export class DetailComponent implements OnInit {
  product: ProductDetail | null = null;
  error: string = '';

  // US07/US08: true si el usuario es Administrator (controla si se ven los botones Editar y Eliminar).
  // Solo mejora la experiencia: la seguridad real debe estar en el backend.
  isAdmin: boolean = false;

  // US08: estados del proceso de eliminación
  showConfirm: boolean = false; // true mientras se muestra la pregunta "¿Seguro?"
  deleting: boolean = false;    // true mientras se espera la respuesta del backend
  deleted: boolean = false;     // true cuando el producto ya se eliminó
  deleteError: string = '';     // mensaje si la eliminación falla

  constructor(
    private route: ActivatedRoute,
    private productService: ProductService,
    private cdr: ChangeDetectorRef,
    private authService: AuthService, // US07
    private router: Router            // US08: para volver al catálogo
  ) {}

  ngOnInit(): void {
    this.isAdmin = this.authService.getRole() === 'Administrator'; // US07
    const idParam = this.route.snapshot.paramMap.get('id');
    if (idParam) {
      this.loadDetail(Number(idParam));
    }
  }

  loadDetail(id: number): void {
    this.productService.getProductById(id).subscribe({
      next: (data: ProductDetail) => {
        this.product = data;
        this.cdr.markForCheck();
      },
      error: (err: any) => {
        this.error = 'Producto no encontrado';
        this.cdr.markForCheck();
      }
    });
  }

  // US08: el administrador pulsó "Eliminar producto": muestra la confirmación (aún no se borra nada)
  askDelete(): void {
    this.deleteError = '';
    this.showConfirm = true;
  }

  // US08: el administrador pulsó "Cancelar": oculta la confirmación y NO se envía ninguna petición
  cancelDelete(): void {
    this.showConfirm = false;
  }

  // US08: el administrador confirmó: envía el DELETE al backend.
  // Si sale bien, muestra el mensaje de éxito y vuelve al catálogo.
  confirmDelete(): void {
    if (!this.product) return;

    this.deleting = true; // evita que se envíe dos veces
    this.deleteError = '';

    this.productService.deleteProduct(this.product.id).subscribe({
      next: () => {
        this.deleting = false;
        this.showConfirm = false;
        this.deleted = true;
        this.cdr.markForCheck();
        // Deja ver el mensaje un momento y regresa al catálogo
        setTimeout(() => this.router.navigate(['/catalog']), 1200);
      },
      error: (err: any) => {
        this.deleting = false;
        this.deleteError = this.getDeleteErrorMessage(err);
        this.cdr.markForCheck();
      }
    });
  }

  // US08: traduce el error HTTP a un mensaje entendible para el usuario
  private getDeleteErrorMessage(err: any): string {
    if (err.status === 0) return 'No se pudo conectar con el servidor.';
    if (err.status === 401) return 'Tu sesión expiró. Inicia sesión de nuevo.';
    if (err.status === 403) return 'No tienes permiso para eliminar productos.';
    if (err.status === 404) return 'El producto ya no existe.';
    return 'Ocurrió un error inesperado al eliminar el producto.';
  }
}