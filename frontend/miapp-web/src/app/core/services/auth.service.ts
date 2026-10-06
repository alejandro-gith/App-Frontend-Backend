import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, tap } from 'rxjs';

import { LoginRequest } from '../../features/auth/models/login-request';
import {
  LoginData,
  LoginResponse
} from '../../features/auth/models/login-response';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private readonly apiUrl = 'http://localhost:5150/api/auth';

  constructor(private http: HttpClient) {}

  login(credentials: LoginRequest): Observable<LoginResponse> {
    return this.http
      .post<LoginResponse>(
        `${this.apiUrl}/login`,
        credentials
      )
      .pipe(
        tap(response => {
          if (response.isSuccess && response.value) {
            this.saveSession(response.value);
          }
        })
      );
  }

  private saveSession(data: LoginData): void {
    localStorage.setItem(
      'token',
      data.token
    );

    localStorage.setItem(
      'userId',
      data.userId.toString()
    );

    localStorage.setItem(
      'username',
      data.username
    );

    localStorage.setItem(
      'role',
      data.role
    );
  }

  logout(): void {
    localStorage.removeItem('token');
    localStorage.removeItem('userId');
    localStorage.removeItem('username');
    localStorage.removeItem('role');

    // Estado temporal relacionado con US02.
    localStorage.removeItem('cart');
  }

  isAuthenticated(): boolean {
    const token = this.getToken();

    if (!token) {
      return false;
    }

    try {
      const payload = JSON.parse(
        atob(token.split('.')[1])
      );

      const expiration = payload.exp * 1000;

      if (Date.now() >= expiration) {
        this.logout();
        return false;
      }

      return true;
    } catch {
      this.logout();
      return false;
    }
  }

  getToken(): string | null {
    return localStorage.getItem('token');
  }

  getRole(): string | null {
    return localStorage.getItem('role');
  }

  getUsername(): string | null {
    return localStorage.getItem('username');
  }
  getCurrentUser(): { id: number; username: string; role: string } | null {
  const userId = localStorage.getItem('userId');
  const username = localStorage.getItem('username');
  const role = localStorage.getItem('role');

  if (!userId || !username || !role) {
    return null;
  }

  return {
    id: Number(userId),
    username,
    role
  };
}
}