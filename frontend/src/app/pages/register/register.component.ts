import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { RouterModule, Router } from '@angular/router';
import { CommonModule } from '@angular/common';

import { AuthService } from '../../core/services/auth.service';
import { UserService } from '../../core/services/user.service';
import { RegisterRequest } from '../../core/models/register-request';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [RouterModule, FormsModule, CommonModule],
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent {
  firstName = '';
  lastName = '';
  email = '';
  password = '';
  confirmPassword = '';
  mobilePhoneNumber = '';
  address = '';
  showModal = false;

  constructor(
    private authService: AuthService,
    private userService: UserService,
    private router: Router
  ) {}

  onRegister() {
    if (!this.firstName || !this.lastName || !this.email || !this.password || !this.confirmPassword || !this.mobilePhoneNumber || !this.address) {
      alert('Por favor, completa todos los campos.');
      return;
    }

    if (this.password !== this.confirmPassword) {
      alert('Las contraseñas no coinciden.');
      return;
    }

    const user: RegisterRequest = {
      firstName: this.firstName,
      lastName: this.lastName,
      email: this.email,
      password: this.password,
      mobilePhoneNumber: this.mobilePhoneNumber,
      address: this.address
    };

    this.authService.register(user).subscribe({
      next: () => {
        this.userService.addUser({
          id: Date.now().toString(),
          nombres: this.firstName,
          apellidos: this.lastName,
          email: this.email
        });

        this.showModal = true;

        setTimeout(() => {
          this.router.navigate(['/login']);
        }, 2500); // Redirige en 2.5 segundos
      },
      error: (err) => {
        console.error(err);
        alert('Error al registrar: ' + (err.error?.message || err.statusText));
      }
    });
  }
}
