import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { ReservaResponse } from '../../core/models/reserva-response';
import { ReservaApiService } from '../../core/services/reserva.service';
import { AuthService } from '../../core/services/auth.service';
import { HttpErrorResponse } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { CreateReservaDto } from '../../core/models/create-reserva-dto';

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
  nombreCliente = '';
  correoCliente = '';

  constructor(
    private reservaApi: ReservaApiService,
    private authService: AuthService,
    private router: Router
  ) {}

  abrirReserva() {
    this.showReservaForm = true;
    this.showAsignada = false;
    this.pagado = false;
  }

  reservarMesa() {
      const dto: CreateReservaDto = {
        nombreCliente: this.nombreCliente,
        correoCliente: this.correoCliente,
        fechaHora: `${this.fecha}T${this.hora}`,
        cantidadPersonas: Number(this.personas),
        mesaId: Number(this.mesa)
      };

      this.reservaApi.createReserva(dto).subscribe({
        next: (res) => {
          alert(`¡Reserva creada con ID ${res.id}!`);
          this.showReservaForm = false;
          this.showAsignada = true;
        },
        error: (err: HttpErrorResponse) => {
          console.error(err);
          alert('Error al crear reserva: ' + err.message);
        }
      });
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
