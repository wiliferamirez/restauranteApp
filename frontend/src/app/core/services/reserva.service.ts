import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ReservaResponse } from '../models/reserva-response';
import { CreateReservaDto } from '../models/create-reserva-dto';

@Injectable({ providedIn: 'root' })
export class ReservaApiService {
  // Apunta directamente al localhost:
  private baseUrl = 'http://localhost:5198/api/Reservas';

  constructor(private http: HttpClient) {}

  /** 1) Listar todas las reservas */
  getAllReservas(): Observable<ReservaResponse[]> {
    return this.http.get<ReservaResponse[]>(this.baseUrl);
  }

  /** 2) Crear una nueva reserva */
  createReserva(dto: CreateReservaDto): Observable<ReservaResponse> {
    return this.http.post<ReservaResponse>(this.baseUrl, dto);
  }

  /** 3) Eliminar una reserva por ID */
  deleteReserva(id: number): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/${id}`);
  }
}
