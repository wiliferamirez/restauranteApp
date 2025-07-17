import { Injectable } from '@angular/core';
import { User } from '../models/user.model';
import { RegisterRequest } from '../models/register-request';

@Injectable({ providedIn: 'root' })
export class UserService {
  private users: User[] = [];

  addUser(user: User) {
    this.users.push(user);
  }

  getUsers(): User[] {
    return this.users;
  }
}
