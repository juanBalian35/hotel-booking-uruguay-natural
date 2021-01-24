import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { LodgingSearchBasicInfo } from 'src/app/models/lodging-search-basic-info';
import { SearchLodging } from 'src/app/models/search-lodging';
import { environment } from 'src/environments/environment';
import {LodgingReviewBasicInfo} from 'src/app/models/lodging-review-basic-info';

@Injectable({
  providedIn: 'root'
})
export class LodgingService {
  private uri = environment.URI_BASE + 'lodgings';

  constructor(private http: HttpClient) { }

  getAll(search: SearchLodging): Observable<LodgingSearchBasicInfo[]> {
    let uriWithParams = this.uri + '?checkin=' + search.checkIn + '&checkout=' + search.checkOut  + '&touristspot=' + search.touristSpot
        + '&adults=' + search.adults + '&babies=' + search.babies + '&children=' + search.children + '&retirees=' + search.retirees
        + '&page=' + search.page + '&resultsPerPage=' + search.resultsPerPage;
    if (search.isFull != null) {
      uriWithParams += '&isFull=' + (search.isFull ? 'true' : 'false');
    }
    return this.http.get<LodgingSearchBasicInfo[]>(uriWithParams).pipe(catchError(this.handleError));
  }

  create(params): Observable<LodgingSearchBasicInfo> {
    return this.http.post<LodgingSearchBasicInfo>(this.uri, params).pipe(catchError(this.handleError));
  }

  update(id: number) {
    return this.http.put<LodgingSearchBasicInfo>(this.uri + '/' + id, {}).pipe(catchError(this.handleError));
  }

  delete(id: number) {
    return this.http.delete<void>(this.uri + '/' + id, {}).pipe(catchError(this.handleError));
  }
  getReviews(id: number, page: number, resultsPerPage): Observable<LodgingReviewBasicInfo[]> {
    const uriWithParams = this.uri + '/' + id + '/reviews?page=' + page + '&resultsPerPage=' + resultsPerPage;
    return this.http.get<LodgingReviewBasicInfo[]>(uriWithParams).pipe(catchError(this.handleError));
  }

  private handleError(error: HttpErrorResponse) {
    let message;
 
    if (error.error instanceof ErrorEvent) {
      message = {'connection': 'Error, do you have an active internet connection?'};
    } else {
      if (error.status === 0) {
        message = {'connection': 'The server is shutdown'};
      } else {
        message = error.error.errors;
      }
    }

    return throwError(message);
  }
}
