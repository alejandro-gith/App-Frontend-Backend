import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, tap } from 'rxjs';

import { LoginRequest } from '../../features/auth/models/login-request';
import { LoginResponse } from '../../features/auth/models/login-response';

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
          if (response.success) {
            this.saveSession(response);
          }
        })
      );
  }

  private saveSession(response: LoginResponse): void {
    localStorage.setItem(
      'token',
      response.data.token
    );

    localStorage.setItem(
      'userId',
      response.data.userId.toString()
    );

    localStorage.setItem(
      'username',
      response.data.username
    );

    localStorage.setItem(
      'role',
      response.data.role
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
}