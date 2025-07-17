import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { ReservaService } from '../../core/services/reserva.service';
import { AuthService } from '../../core/services/auth.service';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-welcome',
  standalone: true,
  imports: [FormsModule, CommonModule],
  templateUrl: './welcome.component.html',
  styleUrls: ['./welcome.component.scss']
})
export class WelcomeComponent {
  irAReservas() {
    this.router.navigate(['/admin/bookings']);
  }
  irAUsuarios() {
    this.router.navigate(['/admin/users']);
  }
  showReservaForm = false;
  showAsignada = false;
  pagado = false;
  mesa = '';
  hora = '';
  fecha = '';
  personas = '';

  // Mesas simuladas
  mesasDisponibles = [
    { id: '1', capacidad: 4 },
    { id: '2', capacidad: 2 },
    { id: '3', capacidad: 6 }
  ];

  constructor(
    private reservaService: ReservaService,
    private authService: AuthService,
    private router: Router
  ) {}

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
      usuario: 'Usuario actual'
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

logout() {
  this.authService.logout().subscribe({
    next: () => {
    },
    error: (err: HttpErrorResponse) => {
      console.error('Error al cerrar sesión:', err);
      alert('Ocurrió un error al cerrar sesión');
    },
    complete: () => {
      alert('Sesión cerrada exitosamente');
      this.router.navigate(['/login']);
    }
  });
}

}
