export interface CartItem {
  productId: number;
  quantity: number;
}

export interface Cart {
  id: number;
  userId: number;
  items: CartItem[];
}

export interface AddToCartRequest {
  userId: number;
  productId: number;
  quantity: number;
}
export interface UpdateCartItemRequest {
  productId: number;
  quantity: number;
}