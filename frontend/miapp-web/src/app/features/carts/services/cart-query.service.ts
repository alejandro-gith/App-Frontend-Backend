import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

import { CartResponse } from '../models/cart-response';

@Injectable({
  providedIn: 'root'
})
export class CartQueryService {
  private readonly apiUrl = 'http://localhost:5150/api/carts';

  constructor(private http: HttpClient) {}

  getAll(): Observable<CartResponse[]> {
    return this.http.get<CartResponse[]>(this.apiUrl);
  }
}