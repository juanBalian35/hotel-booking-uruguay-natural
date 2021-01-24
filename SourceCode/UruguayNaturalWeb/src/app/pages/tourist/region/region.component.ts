import {Component, ElementRef, OnChanges, OnInit, ViewChild} from '@angular/core';
import {TouristSpotService} from '../../../services/tourist-spot/tourist-spot.service';
import {TouristspotBasicInfo} from '../../../models/touristspot-basic-info';
import {ActivatedRoute, Router} from '@angular/router';
import {RegionDetailedInfo} from '../../../models/region-detailed-info';
import { fas } from '@fortawesome/free-solid-svg-icons';
import {FaIconLibrary} from '@fortawesome/angular-fontawesome';
import {CategoryDetailedInfo} from '../../../models/category-detailed-info';
import {RegionService} from '../../../services/region/region.service';
import { Observable } from 'rxjs';
import {CategoryService} from '../../../services/category/category.service';


@Component({
  selector: 'app-region',
  templateUrl: './region.component.html',
  styleUrls: ['./region.component.css']
})
export class RegionComponent implements OnInit {
  $regions: Observable<RegionDetailedInfo[]>;
  regionId: number;
  touristspots: TouristspotBasicInfo[] = [];
  categories: CategoryDetailedInfo[];
  activeCategories = [];
  page = 1;
  itemsPerPage = 8;

  constructor(private touristspotService: TouristSpotService,
              private regionService: RegionService,
              private categoryService: CategoryService,
              private route: ActivatedRoute,
              library: FaIconLibrary) {
    library.addIconPacks(fas);
  }


  ngOnInit(): void {
    this.route.params.subscribe( params => {
      this.regionId = params ['id'];
      this.getAllTouristSpot();
      this.$regions = this.regionService.getAll();
      this.categoryService.getAll().subscribe(categories => this.categories = categories);
    } );
  }

  getAllTouristSpot() {
    this.touristspotService.getAll(this.activeCategories, this.regionId, this.page, this.itemsPerPage)
        .subscribe(touristspots => this.touristspots = touristspots);
  }

    handleActiveCategories(cat: CategoryDetailedInfo) {
    if (this.isCategoryActive(cat)) {
      this.activeCategories.splice(this.activeCategories.indexOf(cat.id), 1);
    } else {
      this.activeCategories.push(cat.id);
    }
        this.page = 1;
        this.getAllTouristSpot();
    }

  isCategoryActive(cat: CategoryDetailedInfo) {
    return this.activeCategories.includes(cat.id);
  }

  deleteFilters() {
    this.activeCategories = [];
    this.getAllTouristSpot();

  }
  charArrayToPng(uints: Uint8Array) {
    return 'data:image/png;base64,' + uints;
  }
  changePage() {
    this.getAllTouristSpot();
  }
}
