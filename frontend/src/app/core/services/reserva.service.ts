import { Injectable } from '@angular/core';

export interface Reserva {
  mesa: string;
  hora: string;
  fecha: string;
  personas: string;
  usuario: string;
}

@Injectable({ providedIn: 'root' })
export class ReservaService {
  private reservas: Reserva[] = [];

  addReserva(reserva: Reserva) {
    this.reservas.push(reserva);
  }

  eliminarReserva(reserva: Reserva) {
    this.reservas = this.reservas.filter(r =>
      r.mesa !== reserva.mesa ||
      r.hora !== reserva.hora ||
      r.fecha !== reserva.fecha ||
      r.personas !== reserva.personas ||
      r.usuario !== reserva.usuario
    );
  }

  getReservas(): Reserva[] {
    return this.reservas;
  }
}
