import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { DatePipe } from '@angular/common';

import { CartResponse } from '../../models/cart-response';
import { CartQueryService } from '../../services/cart-query.service';

@Component({
  selector: 'app-cart-list',
  standalone: true,
  imports: [DatePipe],
  templateUrl: './cart-list.html',
  styleUrl: './cart-list.css'
})
export class CartList implements OnInit {
  carts: CartResponse[] = [];
  loading = false;
  error = '';

  constructor(
    private cartService: CartQueryService,
    private changeDetector: ChangeDetectorRef
  ) {}

  ngOnInit(): void {
    this.loadCarts();
  }

  loadCarts(): void {
    this.loading = true;
    this.error = '';

    this.cartService.getAll().subscribe({
      next: (carts) => {
        this.carts = carts;
        this.loading = false;
        this.changeDetector.markForCheck();
      },
      error: () => {
        this.error =
          'No se pudieron cargar los carritos. Intenta nuevamente.';
        this.loading = false;
        this.changeDetector.markForCheck();
      }
    });
  }
}