import { Component } from '@angular/core';
import { Router } from '@angular/router';

import { RouterModule } from '@angular/router';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [RouterModule, FormsModule],
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent {
  email: string = '';
  password: string = '';

  constructor(private router: Router) {}

  onLogin() {
    if (!this.email || !this.password) {
      alert('Por favor ingresa tu email y contraseña.');
      return;
    }
    this.router.navigate(['/welcome']);
  }
}
