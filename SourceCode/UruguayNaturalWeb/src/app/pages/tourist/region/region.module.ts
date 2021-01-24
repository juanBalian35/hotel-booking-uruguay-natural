import { NgModule } from '@angular/core';
import {RouterModule} from '@angular/router';
import {RegionComponent} from './region.component';
import {TouristSpotService} from '../../../services/tourist-spot/tourist-spot.service';
import {CommonModule} from '@angular/common';
import {HTTP_INTERCEPTORS, HttpClient, HttpClientModule} from '@angular/common/http';
import {NgbModule, NgbPaginationModule} from '@ng-bootstrap/ng-bootstrap';
import { TouristSpotCardComponent } from './tourist-spot-card/tourist-spot-card.component';
import {FontAwesomeModule} from '@fortawesome/angular-fontawesome';
import {HoverClassDirective} from '../../../directives/hover-class.directive';
import {FormsModule} from '@angular/forms';
import {RegionService} from '../../../services/region/region.service';
import {CategoryService} from '../../../services/category/category.service';
import {SharedModule} from '../../../shared/shared.module';
import {SpinnerInterceptor} from '../../../services/spinner-interceptor/spinner-interceptor';
import { AuthInterceptor } from 'src/app/services/auth-interceptor/auth-interceptor';

@NgModule({
    imports: [
        RouterModule,
        CommonModule,
        HttpClientModule,
        NgbPaginationModule,
        FontAwesomeModule,
        FormsModule,
        NgbModule,
        SharedModule
    ],
  declarations: [
      RegionComponent,
      TouristSpotCardComponent],
  exports: [RegionComponent],
  providers: [
      TouristSpotService,
      RegionService,
      CategoryService,
      {
          provide: HTTP_INTERCEPTORS,
          useClass: SpinnerInterceptor,
          multi: true
      }
  ]
})
export class RegionModule { }
