import { Injectable } from '@angular/core';
import {environment} from '../../../environments/environment';
import {HttpClient, HttpErrorResponse} from '@angular/common/http';
import {Observable, throwError} from 'rxjs';
import {catchError} from 'rxjs/operators';
import {CategoryDetailedInfo} from '../../models/category-detailed-info';

@Injectable()
export class CategoryService {
  private uri = environment.URI_BASE + 'categories';

  constructor(private http: HttpClient) { }

  getAll(): Observable<CategoryDetailedInfo[]> {
    return this.http.get<CategoryDetailedInfo[]>(this.uri).pipe(catchError(this.handleError));
  }

  private handleError(error: HttpErrorResponse) {
    let message: string;

    if (error.error instanceof ErrorEvent) {

      message = 'Error: do it again';
    } else {
      if (error.status === 0) {
        message = 'The server is shutdown';
      } else {
        message = error.error.message;
      }
    }
    return throwError(message);
  }
}
