export function isEmailValid(email: string): boolean {
  return /^[^@\s]+@[^@\s]+\.[^@\s]+$/.test(email);
}

export function isPasswordStrong(password: string): boolean {
  return password.length >= 6;
}
