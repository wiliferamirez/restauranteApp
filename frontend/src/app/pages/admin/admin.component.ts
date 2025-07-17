import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AuthService } from '../../core/services/auth.service';
import { ReservaService, Reserva } from '../../core/services/reserva.service';
import { AuthUser } from '../../core/models/auth-user';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-admin',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './admin.component.html',
  styleUrls: ['./admin.component.scss']
})
export class AdminComponent implements OnInit {
  usuarios: AuthUser[] = [];
  mesas: Reserva[] = [];

  constructor(
    private authService: AuthService,
    private reservaService: ReservaService
  ) {}

  ngOnInit(): void {
    this.authService.getAllUsers().subscribe({
      next: (users: AuthUser[]) => {
        this.usuarios = users;
      },
      error: (err: HttpErrorResponse) => {
        console.error('Error al cargar usuarios:', err);
        alert('No se pudieron cargar los usuarios.');
      }
    });

    this.actualizarReservas();
  }

  actualizarReservas() {
    this.mesas = this.reservaService.getReservas();
  }
}
