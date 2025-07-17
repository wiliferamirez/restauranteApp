import { Component, ViewChild } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { RecaptchaModule, RecaptchaComponent } from 'ng-recaptcha';
import { AuthService } from '../../core/services/auth.service';
import { LoginRequest } from '../../core/models/login-request';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [CommonModule, FormsModule, RecaptchaModule],
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent {
  email = '';
  password = '';
  captchaToken: string = '';

  @ViewChild(RecaptchaComponent) captcha!: RecaptchaComponent;

  constructor(private authService: AuthService, private router: Router) {}

  login() {
    if (!this.email || !this.password) {
      alert('Por favor, completa todos los campos.');
      return;
    }

    this.captcha.execute(); 
  }
  goToRegister(): void {
    this.router.navigate(['/register']);
  }

  onCaptchaResolved(token: string | null): void {
    if (!token) {
      alert('No se resolvió el captcha.');
      return;
    }

    const loginData: LoginRequest = {
      email: this.email,
      password: this.password,
      captchaToken: token
    };

    this.authService.login(loginData).subscribe({
      next: () => this.router.navigate(['/welcome']),
      error: (err) => {
        console.error('Error de login:', err);
        alert(err?.error || 'Credenciales inválidas o error del servidor.');
      }
    });
  }
}
