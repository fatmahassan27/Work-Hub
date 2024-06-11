import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';

export const loginGuard: CanActivateFn = (route, state) => {
  let router = inject(Router);
  router.navigateByUrl("/login");
  return false;
};
