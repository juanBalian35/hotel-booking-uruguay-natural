import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { Observable } from 'rxjs';
import { RegionDetailedInfo } from 'src/app/models/region-detailed-info';
import { TouristspotBasicInfo } from 'src/app/models/touristspot-basic-info';
import { LodgingService } from 'src/app/services/lodging/lodging.service';
import { RegionService } from 'src/app/services/region/region.service';
import { TouristSpotService } from 'src/app/services/tourist-spot/tourist-spot.service';

@Component({
  selector: 'app-create',
  templateUrl: './create.component.html',
  styleUrls: ['./create.component.css']
})
export class CreateComponent implements OnInit {
  filesToUpload: File[] = [];
  regions$: Observable<RegionDetailedInfo[]>;
  touristSpots$: Observable<TouristspotBasicInfo[]>;
  showErrors = false;
  showSuccess = false;

  lodgingForm = this.formBuilder.group({
    name: ['', [Validators.required]],
    address: ['', [Validators.required]],
    pricePerNight: ['', [Validators.required]],
    rating: ['', [Validators.required]],
    phone: ['', [Validators.required]],
    confirmationMessage: ['', [Validators.required]],
    description: ['', [Validators.required]],
    region: ['', [Validators.required]],
    touristSpot: ['', [Validators.required]]
  })

  handleFileInput(files: FileList) {
      for(var i = 0; i < files.length; ++i){
        this.filesToUpload.push(files.item(i));
      }
      if(this.filesToUpload.length == 1){
        document.getElementById('inputFile-label').innerHTML = files.item(0).name;
      }
      else{
        document.getElementById('inputFile-label').innerHTML = this.filesToUpload.length + " imagenes";
      }
  }
  
  constructor(private formBuilder: FormBuilder, 
    private regionService: RegionService, 
    private lodgingService: LodgingService,
    private touristSpotService: TouristSpotService) {
    this.regions$ = regionService.getAll();
  }

  ngOnInit(): void { }

  regionSelected(event){
    this.touristSpots$ = this.touristSpotService.getAll([], event.target.value, 1, 1000);
    this.lodgingForm.controls.region.setValue(event.target.value);
  }

  touristSpotSelected(event){
    this.lodgingForm.controls.touristSpot.setValue(event.target.value);
  }

  onSubmit(){
    if(!this.lodgingForm.valid){
      this.showErrors = true;
      this.showSuccess = false;
      return;
    }

    this.showErrors = false;

    let params = new FormData();
    for(var key in this.lodgingForm.controls) {
      params.append(key,this.lodgingForm.controls[key].value);
    }
    this.filesToUpload.forEach(x => params.append('images', x, x.name));

    this.lodgingService.create(params)
      .subscribe(c => this.showSuccess = true,
      err => {
        for(var key in err){
          this.showSuccess = false;
          this.showErrors = true;
          var styledKey = key[0].toLowerCase() + key.slice(1);
          if(styledKey in this.lodgingForm.controls) {
            this.lodgingForm.controls[styledKey].setErrors({apiErrors: err[key]})
          }
        }
      });
  }

}
