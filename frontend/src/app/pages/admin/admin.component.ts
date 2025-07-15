import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { UserService } from '../../core/services/user.service';
import { ReservaService, Reserva } from '../../core/services/reserva.service';

@Component({
  selector: 'app-admin',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './admin.component.html',
  styleUrls: ['./admin.component.scss']
})
export class AdminComponent {
  usuarios: any[] = [];
  mesas: Reserva[] = [];

  constructor(private userService: UserService, private reservaService: ReservaService) {
    this.usuarios = this.userService.getUsers();
    // Actualiza la lista de reservas cada vez que se navega al admin
    this.actualizarReservas();
  }

  actualizarReservas() {
    this.mesas = this.reservaService.getReservas();
  }

  // Si quieres actualizar en tiempo real, puedes usar un timer o un observable en el futuro
}
