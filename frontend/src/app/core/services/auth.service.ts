import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { map, catchError } from 'rxjs/operators';

import { LoginRequest } from '../models/login-request';
import { LoginResponse } from '../models/login-response';
import { RegisterRequest } from '../models/register-request';

import { AuthUser } from '../models/auth-user';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private authUrl = 'http://localhost:5217/api/v1/Auth';
  private userUrl = 'http://localhost:5217/api/v1/User'; // ✅ URL real de usuarios

  constructor(private http: HttpClient) {}

  login(data: LoginRequest): Observable<LoginResponse> {
    return this.http.post<LoginResponse>(`${this.authUrl}/login`, data, {
      withCredentials: true
    });
  }

  register(data: RegisterRequest): Observable<any> {
    return this.http.post(`${this.authUrl}/register`, data, {
      withCredentials: true
    });
  }

  logout(): Observable<any> {
    return this.http.post(`${this.authUrl}/logout`, {}, {
      withCredentials: true
    });
  }

  checkSession(): Observable<boolean> {
    return this.http.get<{ isAuthenticated: boolean }>(
      `${this.authUrl}/check-session`,
      { withCredentials: true }
    ).pipe(
      map(response => response.isAuthenticated),
      catchError(() => of(false))
    );
  }

  getAllUsers(): Observable<AuthUser[]> {
    return this.http.get<AuthUser[]>(this.userUrl, { withCredentials: true });
  }
}
