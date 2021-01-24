import { Injectable } from '@angular/core';
import {environment} from '../../../environments/environment';
import {HttpClient, HttpErrorResponse} from '@angular/common/http';
import {Observable, throwError} from 'rxjs';
import {catchError} from 'rxjs/operators';
import {ReportBasicInfo} from '../../models/report-basic-info';

@Injectable()
export class ReportService {
  private uri = environment.URI_BASE + 'reports';

  constructor(private http: HttpClient) { }

  getReport(dateFrom: string, dateTo: string, touristSpotId: number): Observable<ReportBasicInfo[]> {
    const uriWithParams = this.uri + '?checkin=' + dateFrom + '&checkout=' + dateTo + '&touristspot=' + touristSpotId;
    return this.http.get<ReportBasicInfo[]>(uriWithParams).pipe(catchError(this.handleError));
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
