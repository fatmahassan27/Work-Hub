import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';

export const loginGuard: CanActivateFn = (route, state) => {

  let t = localStorage.getItem('token');
  //console.log(t);
  if(t==null){
    let router=inject(Router);
    router.navigate(['/login']);
    return false;
  }
  return true;
};
