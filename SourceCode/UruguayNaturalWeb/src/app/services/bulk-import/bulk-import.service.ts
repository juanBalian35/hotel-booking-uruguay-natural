import { Injectable } from '@angular/core';
import {environment} from '../../../environments/environment';
import {HttpClient, HttpErrorResponse} from '@angular/common/http';
import {Observable, throwError} from 'rxjs';
import {catchError} from 'rxjs/operators';
import {CategoryDetailedInfo} from '../../models/category-detailed-info';

@Injectable()
export class BulkImportService {
  private uri = environment.URI_BASE + 'bulkImports';

  constructor(private http: HttpClient) { }

  getFormatNames(): Observable<string[]> {
    return this.http.get<string[]>(this.uri).pipe(catchError(this.handleError));
  }

  parse(params): Observable<void> {
    return this.http.post<void>(this.uri + "/lodgings", params).pipe(catchError(this.handleError));
  }

  private handleError(error: HttpErrorResponse) {
    let message: string;

    console.log(error)
    if (error.error instanceof ErrorEvent) {
      message = 'Error: do it again';
    } else {
      if (error.status === 0) {
        message = 'The server is shutdown';
      } else {
        message = error.error.errors;
      }
    }
    return throwError(message);
  }
}
