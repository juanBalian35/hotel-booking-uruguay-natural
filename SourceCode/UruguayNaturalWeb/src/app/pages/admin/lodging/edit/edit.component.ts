import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { LodgingSearchBasicInfo } from 'src/app/models/lodging-search-basic-info';
import { RegionBasicInfo } from 'src/app/models/region-basic-info';
import { TouristspotBasicInfo } from 'src/app/models/touristspot-basic-info';
import { SearchLodging } from 'src/app/models/search-lodging';
import { LodgingService } from 'src/app/services/lodging/lodging.service';
import { RegionService } from 'src/app/services/region/region.service';
import { TouristSpotService } from 'src/app/services/tourist-spot/tourist-spot.service';
import { FormArray, FormBuilder, FormControl } from '@angular/forms';

@Component({
  selector: 'app-edit',
  templateUrl: './edit.component.html',
  styleUrls: ['./edit.component.css']
})
export class EditComponent implements OnInit {
  showSuccess: boolean = false;
  selectedLodging: LodgingSearchBasicInfo = null;
  selectedTouristSpot: TouristspotBasicInfo = null;
  regions$: Observable<RegionBasicInfo[]>;
  touristSpots$: Observable<TouristspotBasicInfo[]>;
  lodgings$: Observable<LodgingSearchBasicInfo[]>;
  isFullCheckbox = [
    { id: 1, name: 'Esta ocupado?' }
  ];

  lodgingForm = this.formBuilder.group({
    isFull: new FormArray(this.isFullCheckbox.map(control => new FormControl(false))),
    touristSpot: [],
    lodging: []
  })

  constructor(private formBuilder: FormBuilder,
    private lodgingService: LodgingService, 
    private regionService: RegionService, 
    private touristSpotService: TouristSpotService) {
   }

  ngOnInit(): void { 
    this.regions$ = this.regionService.getAll();
  }

  selectRegion($event){
    this.showSuccess = false
    this.touristSpots$ = this.touristSpotService.getAll([], $event.target.value,1, 1000);
    this.selectedLodging = null;
    this.selectedTouristSpot = null;
    this.lodgings$ = null;  
  }

  selectTouristSpot(value){
    this.showSuccess = false
    if(!value) return;
    let lodging: SearchLodging = {
      adults: 0, babies: 0, retirees: 0, children: 0, checkIn: '2019-01-06T17:16:40', 
      checkOut: '2019-01-08T17:16:40', page: 1, resultsPerPage: 1000, touristSpot: value.id,
      isFull: null
    };
    this.lodgings$ = this.lodgingService.getAll(lodging);
    this.selectedTouristSpot = value;
    this.selectedLodging = null;
  }

  selectLodging(value: LodgingSearchBasicInfo){
    this.showSuccess = false
    if(!value) return;

    console.log(value)
    this.selectedLodging = value;
    this.lodgingForm.controls.isFull['controls'][0].setValue(this.selectedLodging.isFull);
  }

  onSubmit(value){
    this.showSuccess = true;
    if(value.isFull[0] != this.selectedLodging.isFull){
      this.lodgingService.update(this.selectedLodging.id).subscribe(val => {
        this.selectedLodging.isFull = val.isFull
      }, err => this.showSuccess = false);
    }
  }
}
