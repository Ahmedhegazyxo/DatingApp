import { CanActivateFn, Router } from '@angular/router';
import { AuthenticationStateService } from '../services/AuthneticationStateService';
import { inject } from '@angular/core';

export const authGuard: CanActivateFn = (route, state) => {
  let authStateService = inject(AuthenticationStateService)
  let router = inject(Router);
  if (authStateService.userModel() != null) {
    return true;
  }
  else
  {
    router.navigate(['/login']);
    return false;
  }   
};
