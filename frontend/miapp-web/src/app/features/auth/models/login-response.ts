export interface LoginData {
  userId: number;
  username: string;
  role: string;
  token: string;
}

export interface LoginResponse {
  success: boolean;
  message: string;
  data: LoginData;
}