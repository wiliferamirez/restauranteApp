
import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { RouterModule } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { AuthService } from '../../core/services/auth.service';
import { UserService } from '../../core/services/user.service';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [RouterModule, FormsModule],
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent {
  nombres = '';
  apellidos = '';
  email = '';
  password = '';
  confirmPassword = '';

  constructor(private authService: AuthService, private userService: UserService, private router: Router) {}

  onRegister() {
    if (!this.nombres || !this.apellidos || !this.email || !this.password || !this.confirmPassword) {
      alert('Por favor, completa todos los campos.');
      return;
    }
    if (this.password !== this.confirmPassword) {
      alert('Las contraseñas no coinciden.');
      return;
    }
    const user = {
      nombres: this.nombres,
      apellidos: this.apellidos,
      email: this.email,
      password: this.password
    };
    this.authService.register(user).subscribe({
      next: () => {
        // Guardar usuario en UserService para administración
        this.userService.addUser({
          id: Date.now().toString(),
          nombres: this.nombres,
          apellidos: this.apellidos,
          email: this.email
        });
        alert('¡Usuario registrado con éxito!');
        this.router.navigate(['/login']);
      },
      error: (err) => {
        alert('Error al registrar usuario: ' + (err.error?.message || err.statusText));
      }
    });
  }
}
