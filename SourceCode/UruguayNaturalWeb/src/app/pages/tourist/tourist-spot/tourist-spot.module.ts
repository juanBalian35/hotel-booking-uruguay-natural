import { LOCALE_ID, NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TouristSpotComponent } from './tourist-spot.component';
import {TouristSpotService} from '../../../services/tourist-spot/tourist-spot.service';
import {FontAwesomeModule} from '@fortawesome/angular-fontawesome';
import {SharedModule} from '../../../shared/shared.module';
import {
    NgbCarouselModule,
    NgbDatepickerModule,
    NgbDropdownModule,
    NgbNavModule,
    NgbRatingModule,
    NgbTooltipModule
} from '@ng-bootstrap/ng-bootstrap';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import { LodgingSearchFilterComponent } from './lodging-search-filter/lodging-search-filter.component';
import { GuestCounterComponent } from './lodging-search-filter/guest-counter/guest-counter.component';
import { LodgingCardComponent } from './lodging-card/lodging-card.component';
import {LodgingService} from '../../../services/lodging/lodging.service';
import { registerLocaleData } from '@angular/common';
import localeEs from '@angular/common/locales/es-UY';
import { LodgingCardNavsComponent } from './lodging-card/lodging-card-navs/lodging-card-navs.component';
import { BookingComponent } from './lodging-card/lodging-card-navs/booking/booking.component';
import { TouristReviewsComponent } from './lodging-card/lodging-card-navs/tourist-reviews/tourist-reviews.component';
import { ImageGalleryComponent } from './lodging-card/lodging-card-navs/image-gallery/image-gallery.component';
import { CreateReviewComponent } from './lodging-card/lodging-card-navs/create-review/create-review.component';
import {BookingService} from '../../../services/booking/booking.service';



@NgModule({
  declarations: [
      TouristSpotComponent,
      LodgingSearchFilterComponent,
      GuestCounterComponent,
      LodgingCardComponent,
      LodgingCardNavsComponent,
      BookingComponent,
      TouristReviewsComponent,
      ImageGalleryComponent,
      CreateReviewComponent
  ],
    imports: [
        CommonModule,
        FontAwesomeModule,
        NgbDatepickerModule,
        FormsModule,
        NgbDropdownModule,
        NgbTooltipModule,
        SharedModule,
        NgbRatingModule,
        NgbNavModule,
        ReactiveFormsModule,
        NgbCarouselModule
    ],
  exports: [
      TouristSpotComponent
  ],
  providers: [
      TouristSpotService,
      LodgingService,
      BookingService
  ]
})
export class TouristSpotModule { }
