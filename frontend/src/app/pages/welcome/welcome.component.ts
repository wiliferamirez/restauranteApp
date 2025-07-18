import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { ReservaResponse } from '../../core/models/reserva-response';
import { ReservaApiService } from '../../core/services/reserva.service';
import { AuthService } from '../../core/services/auth.service';
import { HttpErrorResponse } from '@angular/common/http';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-welcome',
  standalone: true,
  imports: [FormsModule, CommonModule, RouterModule],
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

  constructor(
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



  pagarReserva() {
    this.pagado = true;
    alert('¡Reserva pagada exitosamente!');
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
