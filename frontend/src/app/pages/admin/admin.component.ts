import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { AuthService } from '../../core/services/auth.service';
import { AuthUserResponse } from '../../core/models/user-response';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-admin',
  standalone: true,
  imports: [
    CommonModule,
    HttpClientModule    // ← aquí en imports y listo
  ],
  templateUrl: './admin.component.html',
  styleUrls: ['./admin.component.scss']
})
export class AdminComponent implements OnInit {
  usuarios: AuthUserResponse[] = [];

  constructor(
    private authService: AuthService,
  ) {}

  ngOnInit(): void {
    this.authService.getAllUsers().subscribe(
      (users: AuthUserResponse[]) => this.usuarios = users,
      (err: HttpErrorResponse) => {
        console.error('Error al cargar usuarios:', err);
        alert('No se pudieron cargar los usuarios.');
      }
    );
  }

}
