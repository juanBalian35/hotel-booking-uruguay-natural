import {Component, Input, OnInit} from '@angular/core';
import {LodgingService} from '../../../../../../services/lodging/lodging.service';
import {LodgingReviewBasicInfo} from '../../../../../../models/lodging-review-basic-info';

@Component({
  selector: 'app-tourist-reviews',
  templateUrl: './tourist-reviews.component.html',
  styleUrls: ['./tourist-reviews.component.css'],
})
export class TouristReviewsComponent implements OnInit {
  reviews: LodgingReviewBasicInfo[] = [];
  page = 1;
  resultsPerPage = 5;
  @Input() lodgingId: number;
  constructor( private lodgingService: LodgingService) { }

  ngOnInit(): void {
    this.getAllReviews();
  }
  getAllReviews() {
    this.lodgingService.getReviews(this.lodgingId, this.page, this.resultsPerPage).subscribe(reviews => this.reviews = reviews);
  }
  changePage() {
    this.getAllReviews();
  }
}
