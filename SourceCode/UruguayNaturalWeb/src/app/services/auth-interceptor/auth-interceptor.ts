import { HttpHandler, HttpInterceptor, HttpRequest } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { SessionService } from '../session/session.service';

@Injectable()
export class AuthInterceptor implements HttpInterceptor {
  constructor(private session: SessionService) {}

  intercept(req: HttpRequest<any>, next: HttpHandler) {
    if(this.session.isLoggedIn()){
      const authToken = this.session.getToken();
      req = req.clone({
        headers: req.headers.set('Authorization', authToken)
      });
    }

    return next.handle(req);
  }
}