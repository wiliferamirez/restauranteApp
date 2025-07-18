import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ReservaResponse } from '../models/reserva-response';

@Injectable({ providedIn: 'root' })
export class ReservaApiService {
  private apiUrl = 'http://localhost:5198/api/Reservas';

  constructor(private http: HttpClient) {}

  /** Obtiene todas las reservas desde el backend */
  getAllReservas(): Observable<ReservaResponse[]> {
    return this.http.get<ReservaResponse[]>(this.apiUrl);
  }
}
