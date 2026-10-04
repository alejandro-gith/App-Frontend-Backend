import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Product, ProductDetail } from '../../../../core/models/product.model';
import { CreateProductRequest } from '../../../../core/models/create-product-request.model'; // US06
import { UpdateProductRequest } from '../../../../core/models/update-product-request.model'; // US07

@Injectable({
  providedIn: 'root'
})
export class ProductService {
  // Puerto 5150: el mismo que usa auth.service.ts y launchSettings.json del backend
  private apiUrl = 'http://localhost:5150/api/products';

  constructor(private http: HttpClient) { }

  getProducts(): Observable<Product[]> {
    return this.http.get<Product[]>(this.apiUrl);
  }

  getProductsByCategory(category: string): Observable<Product[]> {
    return this.http.get<Product[]>(`${this.apiUrl}/category/${category}`);
  }

  getProductById(id: number): Observable<ProductDetail> {
    return this.http.get<ProductDetail>(`${this.apiUrl}/${id}`);
  }

  // US06: envía un POST al backend para crear un producto nuevo.
  // Recibe los datos del formulario y devuelve (como Observable) el producto creado con su ID.
  // El interceptor de autenticación agrega el token automáticamente.
  createProduct(request: CreateProductRequest): Observable<Product> {
    return this.http.post<Product>(this.apiUrl, request);
  }

  // US07: envía un PUT al backend para editar el producto con el id indicado.
  // Recibe el id y los datos nuevos; devuelve el detalle del producto ya actualizado.
  updateProduct(id: number, request: UpdateProductRequest): Observable<ProductDetail> {
    return this.http.put<ProductDetail>(`${this.apiUrl}/${id}`, request);
  }
}