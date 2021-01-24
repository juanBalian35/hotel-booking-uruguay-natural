import {Injectable} from '@angular/core';
import {HttpEvent, HttpHandler, HttpInterceptor, HttpRequest} from '@angular/common/http';
import {HttpStateService} from '../http-state/http-state.service';
import {Observable} from 'rxjs';
import {delay, finalize, retryWhen, take, tap} from 'rxjs/operators';
import {HttpProgressState} from '../../models/http-progress-state';

@Injectable({
  providedIn: 'root'
})
export class SpinnerInterceptor implements HttpInterceptor {
  private exceptions: string[] = [
    'login'
  ];

  constructor(private httpStateService: HttpStateService) {
  }

  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {

    if (!this.exceptions.every((term: string) => request.url.indexOf(term) === -1)) {
      return next.handle(request).pipe(tap((response: any) => {},
          (error) => {}));
    }

    this.httpStateService.state.next({
      url: request.url,
      state: HttpProgressState.start
    });

    return next.handle(request).pipe(finalize(() => {
      this.httpStateService.state.next({
        url: request.url,
        state: HttpProgressState.end
      });
    }));
  }
}
