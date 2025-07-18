// src/app/pages/reservas/reservas.component.ts
import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { ReservaApiService } from '../../core/services/reserva.service';
import { ReservaResponse } from '../../core/models/reserva-response';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-reservas',
  standalone: true,
  imports: [
    CommonModule,
    HttpClientModule
  ],
  templateUrl: './reservas.component.html',
  styleUrls: ['./reservas.component.scss']
})
export class ReservasComponent implements OnInit {
  reservas: ReservaResponse[] = [];
  loading = true;
  errorMsg: string | null = null;

  constructor(private reservaApi: ReservaApiService) {}

  ngOnInit(): void {
    this.reservaApi.getAllReservas().subscribe(
      (data: ReservaResponse[]) => {
        this.reservas = data;
        this.loading = false;
      },
      (err: HttpErrorResponse) => {
        console.error('Error al cargar reservas:', err);
        this.errorMsg = 'No se pudieron cargar las reservas.';
        this.loading = false;
      }
    );
  }
}
