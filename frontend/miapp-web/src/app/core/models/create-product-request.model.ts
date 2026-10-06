// US06: forma exacta del cuerpo que Angular envía al backend para crear un producto.
// Los nombres coinciden con CreateProductRequest del backend (title, price, category, description, image).
export interface CreateProductRequest {
  title: string;
  price: number;
  category: string;
  description: string;
  image: string;
}