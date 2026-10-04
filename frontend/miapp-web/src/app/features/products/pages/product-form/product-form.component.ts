import { ChangeDetectorRef, Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { RouterModule } from '@angular/router';

import { ProductService } from '../product-list/product.service';
import { Product } from '../../../../core/models/product.model';
import { CreateProductRequest } from '../../../../core/models/create-product-request.model';

// US06: formulario para que el Administrador cree un producto nuevo.
// Este componente solo maneja la pantalla y el formulario; la comunicación HTTP
// la hace ProductService (separación de responsabilidades).
@Component({
  selector: 'app-product-form',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, RouterModule],
  templateUrl: './product-form.component.html',
  styleUrl: './product-form.component.css'
})
export class ProductFormComponent implements OnInit {
  private fb = inject(FormBuilder);
  private productService = inject(ProductService);
  private cdr = inject(ChangeDetectorRef);

  // Opciones del desplegable de categoría (se obtienen de los productos existentes)
  categories: string[] = [];

  // Estados de la pantalla: cargando, éxito y error
  loading = false;
  successMessage = '';
  errorMessage = '';

  // Formulario reactivo con las validaciones del lado del cliente (mejoran la experiencia de usuario).
  // El backend vuelve a validar todo: Angular no es la autoridad final.
  form = this.fb.group({
    title: ['', [Validators.required, Validators.maxLength(100)]],
    price: [null as number | null, [Validators.required, Validators.min(0.01)]],
    category: ['', [Validators.required]],
    description: ['', [Validators.required, Validators.maxLength(500)]],
    image: ['', [Validators.required, Validators.pattern(/^https?:\/\/.+/i)]]
  });

  // Al abrir la pantalla, carga las categorías para el desplegable
  ngOnInit(): void {
    this.loadCategories();
  }

  // Obtiene los productos y saca la lista de categorías sin repetir, ordenadas
  private loadCategories(): void {
    this.productService.getProducts().subscribe({
      next: (products: Product[]) => {
        this.categories = [...new Set(products.map(p => p.category))].sort();
        this.cdr.markForCheck(); // la app no usa zone.js: hay que avisar a Angular que redibuje
      },
      error: () => {
        this.errorMessage = 'No se pudieron cargar las categorías.';
        this.cdr.markForCheck();
      }
    });
  }

  // Indica si un campo tiene un error concreto y ya fue tocado (para mostrar el mensaje en rojo)
  hasError(field: string, error: string): boolean {
    const control = this.form.get(field);
    return !!control && control.touched && control.hasError(error);
  }

  // Se ejecuta al enviar el formulario
  onSubmit(): void {
    this.successMessage = '';
    this.errorMessage = '';

    // Si hay campos inválidos, no se envía nada al backend y se marcan los errores
    if (this.form.invalid) {
      this.form.markAllAsTouched();
      return;
    }

    // Arma el cuerpo de la petición con los nombres que espera el backend (CreateProductRequest)
    const value = this.form.getRawValue();
    const request: CreateProductRequest = {
      title: value.title ?? '',
      price: Number(value.price),
      category: value.category ?? '',
      description: value.description ?? '',
      image: value.image ?? ''
    };

    this.loading = true; // deshabilita el botón para evitar envíos dobles

    this.productService.createProduct(request).subscribe({
      next: (created: Product) => {
        this.loading = false;
        this.successMessage = `Producto creado correctamente. ID asignado: ${created.id}`;
        // Limpia el formulario para poder agregar otro producto
        this.form.reset({ title: '', price: null, category: '', description: '', image: '' });
        this.cdr.markForCheck();
      },
      error: (err: any) => {
        this.loading = false;
        this.errorMessage = this.getErrorMessage(err);
        this.cdr.markForCheck();
      }
    });
  }

  // Traduce el error HTTP a un mensaje entendible para el usuario
  private getErrorMessage(err: any): string {
    if (err.status === 0) return 'No se pudo conectar con el servidor.';
    if (err.status === 401) return 'Tu sesión expiró. Inicia sesión de nuevo.';
    if (err.status === 403) return 'No tienes permiso para crear productos.';
    if (err.status === 400 && typeof err.error === 'string') return err.error;
    return 'Ocurrió un error inesperado al crear el producto.';
  }
}