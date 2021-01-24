import { Injectable } from '@angular/core';
import {environment} from 'src/environments/environment';
import {HttpClient, HttpErrorResponse} from '@angular/common/http';
import {Observable, throwError} from 'rxjs';
import {catchError} from 'rxjs/operators';
import {TouristspotBasicInfo} from '../../models/touristspot-basic-info';
import {TouristspotDetailedInfo} from '../../models/touristspot-detailed-info';


@Injectable()
export class TouristSpotService {
  private uri = environment.URI_BASE + 'touristspots';

  constructor(private http: HttpClient) { }

  getCategoriesParams(categories: number[]) {
    let categoriesParams = '';
    if (categories.length !== 0) {
      categories.forEach(function (value) {
        categoriesParams += ('&categories=' + value);
      });
    }
    return categoriesParams;
  }

  getAll(categories: number[], region: number, page: number, resultsPerPage: number): Observable<TouristspotBasicInfo[]> {
    const categoriesParams: string = this.getCategoriesParams(categories);
    const uriWithParams = this.uri + '?region=' + region + categoriesParams + '&page=' + page + '&resultsPerPage=' + resultsPerPage;
    return this.http.get<TouristspotBasicInfo[]>(uriWithParams).pipe(catchError(this.handleError));
  }

  get(id: number) {
    return this.http.get<TouristspotDetailedInfo>(this.uri + '/' + id).pipe(catchError(this.handleError));
  }

  create(params){
    return this.http.post<TouristspotBasicInfo>(this.uri, params).pipe(catchError(this.handleError));
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
