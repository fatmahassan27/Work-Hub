import { HttpInterceptorFn } from '@angular/common/http';

export const addTokenInterceptor: HttpInterceptorFn = (req, next) => {

  let token = localStorage.getItem('token');
  if(token==null)
  return next(req);

  req = req.clone({
    setHeaders:{
      'Authorization' : `Bearer ${token}`,
    }
  });
  return next(req);

};
