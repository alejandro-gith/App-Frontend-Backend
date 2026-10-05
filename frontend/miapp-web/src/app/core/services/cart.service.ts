import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { AddToCartRequest, Cart, UpdateCartItemRequest } from '../models/cart.model';

@Injectable({
  providedIn: 'root'
})
export class CartService {
  private readonly apiUrl = 'http://localhost:5000/api/carts'; // Ajustar puerto si es necesario

  constructor(private http: HttpClient) {}

  // US09: Agregar al carrito
  addToCart(request: AddToCartRequest): Observable<Cart> {
    return this.http.post<Cart>(this.apiUrl, request);
  }

  // US10: Obtener carrito por Id de usuario
  getCart(userId: number): Observable<Cart> {
    return this.http.get<Cart>(`${this.apiUrl}/${userId}`);
  }

  // US10: Actualizar cantidad de un producto
  updateItemQuantity(userId: number, request: UpdateCartItemRequest): Observable<Cart> {
    return this.http.put<Cart>(`${this.apiUrl}/${userId}/items`, request);
  }

  // US10: Eliminar un producto del carrito
  removeItem(userId: number, productId: number): Observable<Cart> {
    return this.http.delete<Cart>(`${this.apiUrl}/${userId}/items/${productId}`);
  }
}