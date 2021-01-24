import {Component, Input, OnInit} from '@angular/core';
import {SearchLodging} from 'src/app/models/search-lodging';

@Component({
  selector: 'app-lodging-card-navs',
  templateUrl: './lodging-card-navs.component.html',
  styleUrls: ['./lodging-card-navs.component.css']
})
export class LodgingCardNavsComponent implements OnInit {
  @Input() lodging;
  @Input() bookingDetails: SearchLodging;
  active = 1;
  constructor() { }

  ngOnInit(): void {
  }

}
