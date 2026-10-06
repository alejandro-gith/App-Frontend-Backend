// US07: forma exacta del cuerpo que Angular envía al backend para editar un producto.
// Coincide con UpdateProductRequest del backend. No lleva el id: viaja en la URL (PUT api/products/{id}).
export interface UpdateProductRequest {
  title: string;
  price: number;
  category: string;
  description: string;
  image: string;
}
