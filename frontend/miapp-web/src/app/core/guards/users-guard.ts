import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';

import { AuthService } from '../services/auth.service';

export const usersGuard: CanActivateFn = () => {
  const authService = inject(AuthService);
  const router = inject(Router);

  if (!authService.isAuthenticated()) {
    return router.createUrlTree(['/login']);
  }

  const role = authService.getRole();

  if (role === 'Administrator' || role === 'Auditor') {
    return true;
  }

  return router.createUrlTree(['/products']);
};