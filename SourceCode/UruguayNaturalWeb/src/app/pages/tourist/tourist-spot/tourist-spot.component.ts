import { Component, OnInit } from '@angular/core';
import {TouristSpotService} from '../../../services/tourist-spot/tourist-spot.service';
import {ActivatedRoute} from '@angular/router';
import {FaIconLibrary} from '@fortawesome/angular-fontawesome';
import {fas} from '@fortawesome/free-solid-svg-icons';
import {TouristspotDetailedInfo} from '../../../models/touristspot-detailed-info';
import { Observable } from 'rxjs';
import {LodgingSearchBasicInfo} from '../../../models/lodging-search-basic-info';
import {LodgingService} from '../../../services/lodging/lodging.service';
import {SearchLodging} from '../../../models/search-lodging';

@Component({
  selector: 'app-tourist-spot',
  templateUrl: './tourist-spot.component.html',
  styleUrls: ['./tourist-spot.component.css']
})
export class TouristSpotComponent implements OnInit {
  private touristspotId: number;
  $touristSpot: Observable<TouristspotDetailedInfo>;
  lodgings: LodgingSearchBasicInfo[] = [];
  private search: SearchLodging;
  private page = 1;
  private resultsPerPage = 5;
  private lastPage = false;
  private hasSearched = false;
  constructor(private touristspotService: TouristSpotService,
              private lodgingService: LodgingService,
              private route: ActivatedRoute,
              library: FaIconLibrary) {
    library.addIconPacks(fas); }

  ngOnInit(): void {
    this.route.params.subscribe( params => {
      this.touristspotId = params ['id'];
      this.$touristSpot = this.touristspotService.get(this.touristspotId);
    } );

  }

  charArrayToPng(uints: Uint8Array) {
    return 'data:image/png;base64,' + uints;
  }

  searchLodgings($event: SearchLodging) {
    this.lastPage = false;
    $event.page = this.page;
    $event.resultsPerPage = this.resultsPerPage;
    $event.touristSpot = this.touristspotId;
    this.search = $event;
    this.lodgingService.getAll(this.search).subscribe(lodgings => this.lodgings = lodgings);
    this.hasSearched = true;
  }

  loadPage() {
      this.search.page++;
      this.lodgingService.getAll(this.search).subscribe(lodgings => {
        if (lodgings.length === 0) {
          this.lastPage = true;
        }
        this.lodgings = this.lodgings.concat(lodgings);
      });
  }
}
