import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { Observable, throwError } from 'rxjs';
import { catchError, tap } from 'rxjs/operators';
import { SessionBasicInfo } from 'src/app/models/session-basic-info';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class SessionService {
  private uri = environment.URI_BASE + 'sessions';

  constructor(private http: HttpClient) { }

  create(params) : Observable<SessionBasicInfo> {
    return this.http.post<SessionBasicInfo>(this.uri, params).pipe(tap((session => this.updateStorage(session, params['email']))), catchError(this.handleError));
  }

  getToken(){
    return localStorage.getItem('token');
  }

  getEmail(){
    return localStorage.getItem('email');
  }

  isLoggedIn(){
    return localStorage.getItem('token') != null;
  }

  logOut(){
    localStorage.removeItem('email');
    localStorage.removeItem('token');
  }

  private updateStorage(session: SessionBasicInfo, email){
    localStorage.setItem('email', email)
    localStorage.setItem('token', session.token);
  }

  private handleError(error: HttpErrorResponse) {
    let message;

    console.log(error)
    if (error.error instanceof ErrorEvent) {
      message = {'connection': 'Error, do you have an active internet connection?'};
    } 
    else {
      if (error.status === 0) {
        message = {'connection': 'The server is shutdown'};
      } 
      else {
        message = error.error.errors;
      }
    }
    
    return throwError(message);
  }
}
