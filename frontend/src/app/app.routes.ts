// app.routes.ts
import { Routes } from '@angular/router';
import { LoginComponent } from './pages/login/login.component';
import { RegisterComponent } from './pages/register/register.component';
import { WelcomeComponent } from './pages/welcome/welcome.component';
import { AdminComponent } from './pages/admin/admin.component';
import { AuthGuard } from './core/services/auth.guard';

export const routes: Routes = [
  { path: '', redirectTo: 'login', pathMatch: 'full' },
  { path: 'login', component: LoginComponent },
  { path: 'register', component: RegisterComponent },
  // { path: 'welcome', component: WelcomeComponent, canActivate: [AuthGuard] },
  { path: 'welcome', component: WelcomeComponent},
  // { path: 'admin', component: AdminComponent, canActivate: [AuthGuard] },
  { path: 'admin', component: AdminComponent},
  { path: '**', redirectTo: 'login' }
];
