import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { User } from '../models/user.model';

@Injectable({ providedIn: 'root' })
export class AuthService {
  private user: User | null = null;
  private apiUrl = 'http://localhost:8000/register'; // Kong proxy
  private useApi = false; // Cambia a false para modo simulado

  constructor(private http: HttpClient) {}

  register(user: { nombres: string; apellidos: string; email: string; password: string }): Observable<any> {
    if (this.useApi) {
      return this.http.post(this.apiUrl, user);
    } else {
      // Simulación: guarda en memoria y retorna un observable exitoso
      this.user = { id: Date.now().toString(), ...user };
      return of({ message: 'Usuario registrado (simulado)' });
    }
  }

  // Métodos de login/logout pueden implementarse igual usando la API real
  login(email: string, password: string): boolean {
    // Lógica simulada (puedes actualizarla para usar la API real)
    if (email && password) {
      this.user = { id: '1', nombres: 'Demo', apellidos: 'User', email };
      return true;
    }
    return false;
  }

  logout() {
    this.user = null;
  }

  getUser(): User | null {
    return this.user;
  }
}
