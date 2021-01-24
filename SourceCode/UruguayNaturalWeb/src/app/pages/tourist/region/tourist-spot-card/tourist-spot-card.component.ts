import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {TouristspotBasicInfo} from '../../../../models/touristspot-basic-info';
import {CategoryBasicInfo} from '../../../../models/category-basic-info';
import {CategoryDetailedInfo} from '../../../../models/category-detailed-info';


@Component({
  selector: 'app-tourist-spot',
  templateUrl: './tourist-spot-card.component.html',
  styleUrls: ['./tourist-spot-card.component.css']
})
export class TouristSpotCardComponent implements OnInit {
  @Input() touristspot: TouristspotBasicInfo;
  @Input() activeCategories;
  @Input() categories: CategoryDetailedInfo[];
  constructor() { }
  ngOnInit(): void {
  }
  charArrayToPng(uints: Uint8Array) {
    return 'data:image/png;base64,' + uints;
  }

  findIconCategory(cat: CategoryBasicInfo) {
   return this.categories.find( cate => cate.id === cat.id).faIconName;
  }
  isCategoryActive(cat: CategoryBasicInfo) {
    return this.activeCategories.includes(cat.id);
  }
}
