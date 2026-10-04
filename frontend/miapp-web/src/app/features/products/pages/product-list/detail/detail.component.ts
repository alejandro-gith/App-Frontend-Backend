import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute, RouterModule } from '@angular/router';
import { ProductService } from '../product.service';
import { ProductDetail } from '../../../../../core/models/product.model';

@Component({
  selector: 'app-detail',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './detail.component.html'
})
export class DetailComponent implements OnInit {
  product: ProductDetail | null = null;
  error: string = '';

  constructor(
    private route: ActivatedRoute,
    private productService: ProductService,
    private cdr: ChangeDetectorRef
  ) {}

  ngOnInit(): void {
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
