import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { AccountService } from '../Services/account.service';

// export const loginGuard: CanActivateFn = (route, state) => {

//   let accountService=inject(AccountService);
//   const currentUser = accountService.currentUserValue;

//   let t = localStorage.getItem('token');
//   //console.log(t);
//   if(t==null || !currentUser){
//     let router=inject(Router);
//     router.navigate(['/login'], { queryParams: { returnUrl: state.url } });
//     return false;
//   }
//   return true;
// };

export const loginGuard: CanActivateFn = (route, state) => {
  const accountService = inject(AccountService);
  const currentUser = accountService.currentUserValue;

  if (!currentUser) {
    const router = inject(Router);
    router.navigate(['/login'], { queryParams: { returnUrl: state.url } });
    return false;
  }
  return true;
};
