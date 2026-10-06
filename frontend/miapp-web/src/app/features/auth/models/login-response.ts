export interface LoginData {
  userId: number;
  username: string;
  role: string;
  token: string;
}

export interface LoginResponse {
  isSuccess: boolean;
  value: LoginData | null;
  error: string | null;
}