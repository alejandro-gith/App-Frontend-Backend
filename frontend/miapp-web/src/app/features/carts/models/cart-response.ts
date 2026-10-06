export interface CartItemResponse {
  productId: number;
  quantity: number;
}

export interface CartResponse {
  id: number;
  date: string;
  userId: number;
  products: CartItemResponse[];
}