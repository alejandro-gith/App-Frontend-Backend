import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';

import { AuthService } from '../services/auth.service';

// US06: guard que protege rutas exclusivas del Administrador.
// Si el usuario tiene sesión y su rol es Administrator, deja pasar.
// Si no (Cliente, Auditor o sin sesión), lo redirige al catálogo.
// OJO: esto solo mejora la experiencia de usuario. La seguridad real debe estar en el backend.
export const adminGuard: CanActivateFn = () => {
  const authService = inject(AuthService);
  const router = inject(Router);

  if (authService.isAuthenticated() && authService.getRole() === 'Administrator') {
    return true;
  }

  return router.createUrlTree(['/catalog']);
};