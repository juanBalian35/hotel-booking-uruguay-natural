import {Component, Input, OnInit} from '@angular/core';
import {HttpStateService} from '../../../services/http-state/http-state.service';
import {IHttpState} from '../../../models/IHttpState';
import {HttpProgressState} from '../../../models/http-progress-state';

@Component({
  selector: 'app-http-loading-spinner',
  templateUrl: './http-loading-spinner.component.html',
  styleUrls: ['./http-loading-spinner.component.css']
})
export class HttpLoadingSpinnerComponent implements OnInit {
  public loading = false;
  @Input() public filterBy: string | null = null;
  constructor(private httpStateService: HttpStateService) { }

  /**
   * receives all HTTP requests and filters them by the filterBy
   * values provided
   */
  ngOnInit() {
    this.httpStateService.state.subscribe((progress: IHttpState) => {
      if (progress && progress.url) {
        if (!this.filterBy) {
          this.loading = (progress.state === HttpProgressState.start) ? true : false;
        } else if (progress.url.indexOf(this.filterBy) !== -1) {
          this.loading = (progress.state === HttpProgressState.start) ? true : false;
        }
      }
    });
  }

}
