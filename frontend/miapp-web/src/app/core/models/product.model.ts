export interface Product {
  id: number;
  name: string;
  price: number;
  category: string;
  imageUrl: string;
}

export interface ProductDetail extends Product {
  description: string;
  stock: number;
}