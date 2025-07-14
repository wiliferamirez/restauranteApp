
import { Component } from '@angular/core';
import { RouterModule } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { ReservaService } from '../../core/services/reserva.service';

@Component({
  selector: 'app-welcome',
  standalone: true,
  imports: [RouterModule, FormsModule, CommonModule],
  templateUrl: './welcome.component.html',
  styleUrls: ['./welcome.component.scss']
})
export class WelcomeComponent {
  showReservaForm = false;
  showAsignada = false;
  pagado = false;
  mesa = '';
  hora = '';
  fecha = '';
  personas = '';

  constructor(private reservaService: ReservaService) {}

  abrirReserva() {
    this.showReservaForm = true;
    this.showAsignada = false;
    this.pagado = false;
  }

  mostrarAsignada() {
    this.showAsignada = true;
    this.showReservaForm = false;
  }

  reservarMesa() {
    this.reservaService.addReserva({
      mesa: this.mesa,
      hora: this.hora,
      fecha: this.fecha,
      personas: this.personas,
      usuario: 'Usuario actual' // Puedes mejorar esto luego
    });
    alert('¡Reserva realizada!');
    this.showReservaForm = false;
    this.showAsignada = true;
    this.pagado = false;
  }

  pagarReserva() {
    this.pagado = true;
    alert('¡Reserva pagada exitosamente!');
  }

  eliminarReserva() {
    this.reservaService.eliminarReserva({
      mesa: this.mesa,
      hora: this.hora,
      fecha: this.fecha,
      personas: this.personas,
      usuario: 'Usuario actual'
    });
    this.mesa = '';
    this.hora = '';
    this.fecha = '';
    this.personas = '';
    this.showAsignada = false;
    this.pagado = false;
    alert('Reserva eliminada');
  }
}

