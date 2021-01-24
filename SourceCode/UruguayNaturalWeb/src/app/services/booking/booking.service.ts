import { Injectable } from '@angular/core';
import {environment} from 'src/environments/environment';
import {HttpClient, HttpErrorResponse} from '@angular/common/http';
import {Observable, throwError} from 'rxjs';
import {catchError} from 'rxjs/operators';
import { BookingBasicInfo } from 'src/app/models/booking-basic-info';
import { ReviewBasicInfo } from 'src/app/models/review-basic-info';
import { BookingStateBasicInfo } from 'src/app/models/booking-state-basic-info';

@Injectable()
export class BookingService {
  private uri = environment.URI_BASE + 'bookings';

  constructor(private http: HttpClient) { }
  create(booking): Observable<BookingBasicInfo> {
    return this.http.post<BookingBasicInfo>(this.uri, booking).pipe(catchError(this.handleError));
  }

  createReview(id: number, review): Observable<ReviewBasicInfo> {
    return this.http.post<ReviewBasicInfo>(this.uri + '/' + id + '/reviews', review ).pipe(catchError(this.handleError));
  }

  getStates(id: number): Observable<BookingStateBasicInfo[]> {
    return this.http.get<BookingStateBasicInfo[]>(this.uri + '/' + id + '/states').pipe(catchError(this.handleError));
  }

  createState(bookingId: number, params): Observable<BookingStateBasicInfo> {
    return this.http.post<BookingStateBasicInfo>(this.uri + '/' + bookingId + '/states', params).pipe(catchError(this.handleError));
  }

  private handleError(error: HttpErrorResponse) {
    let message: string;
    
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
