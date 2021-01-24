import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { Admin } from 'src/app/models/admin';
import { AdministratorBasicInfo } from 'src/app/models/administrator-basic-info';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class AdministratorService {
  private uri = environment.URI_BASE + 'administrators';

  constructor(private http: HttpClient) { }

  getAll(): Observable<AdministratorBasicInfo[]> {
    return this.http.get<AdministratorBasicInfo[]>(this.uri).pipe(catchError(this.handleError));
  }

  create(admin: Admin): Observable<AdministratorBasicInfo>{
    return this.http.post<AdministratorBasicInfo>(this.uri, admin).pipe(catchError(this.handleError));
  }

  modify(id, admin): Observable<AdministratorBasicInfo>{
    return this.http.put<AdministratorBasicInfo>(this.uri + "/" + id, admin).pipe(catchError(this.handleError));
  }

  delete(id: number): Observable<void>{
    return this.http.delete<void>(this.uri + "/" + id).pipe(catchError(this.handleError));
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
