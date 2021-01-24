import {Component, Input, OnInit} from '@angular/core';
import {LodgingSearchBasicInfo} from '../../../../models/lodging-search-basic-info';
import {SearchLodging} from '../../../../models/search-lodging';

@Component({
  selector: 'app-lodging-card',
  templateUrl: './lodging-card.component.html',
  styleUrls: ['./lodging-card.component.css']
})
export class LodgingCardComponent implements OnInit {
  @Input() lodging: LodgingSearchBasicInfo;
  @Input() bookingDetails: SearchLodging;
  @Input() index: number;
  constructor() { }

  ngOnInit(): void {
  }

  charArrayToPng(uints: Uint8Array) {
    return 'data:image/png;base64,' + uints;
  }
}
