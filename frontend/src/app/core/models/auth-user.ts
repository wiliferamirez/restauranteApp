export interface AuthUser {
  id: string;
  email: string;
  firstName: string;
  lastName: string;
  lastLogin: string;
  mobilePhoneNumber: string;
  isStaff: boolean;
  emailConfirmed: boolean;
}
