import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute, RouterModule } from '@angular/router';
import { ProductService } from '../product.service';
import { ProductDetail } from '../../../../../core/models/product.model';
import { AuthService } from '../../../../../core/services/auth.service'; // US07

@Component({
  selector: 'app-detail',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './detail.component.html'
})
export class DetailComponent implements OnInit {
  product: ProductDetail | null = null;
  error: string = '';

  // US07: true si el usuario es Administrator (controla si se ve el botón "Editar producto").
  // Solo mejora la experiencia: la seguridad real debe estar en el backend.
  isAdmin: boolean = false;

  constructor(
    private route: ActivatedRoute,
    private productService: ProductService,
    private cdr: ChangeDetectorRef,
    private authService: AuthService // US07
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
}