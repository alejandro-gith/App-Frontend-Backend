import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { ProductService } from './product.service'; 
import { Product } from '../../../../core/models/product.model'; 

@Component({
  selector: 'app-catalog',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './catalog.component.html'
})
export class CatalogComponent implements OnInit {
  products: Product[] = [];
  categories: string[] = ['Todos', 'Computacion', 'Accesorios'];
  selectedCategory: string = 'Todos';

  constructor(
    private productService: ProductService,
    private cdr: ChangeDetectorRef
  ) {}

  ngOnInit(): void {
    this.loadProducts();
  }

  loadProducts(): void {
    this.productService.getProducts().subscribe({
      next: (data: Product[]) => {
        this.products = data;
        this.cdr.markForCheck();
      },
      error: (err: any) => console.error('Error al cargar productos', err) 
    });
  }

  filterByCategory(category: string): void {
    this.selectedCategory = category;
    if (category === 'Todos') {
      this.loadProducts();
    } else {
      this.productService.getProductsByCategory(category).subscribe({
        next: (data: Product[]) => {
          this.products = data;
          this.cdr.markForCheck();
        },
        error: (err: any) => console.error('Error al filtrar', err) 
      });
    }
  }
}