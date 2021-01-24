import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { Observable } from 'rxjs';
import { CategoryBasicInfo } from 'src/app/models/category-basic-info';
import { CategoryDetailedInfo } from 'src/app/models/category-detailed-info';
import { CategoryService } from 'src/app/services/category/category.service';
import {fas} from '@fortawesome/free-solid-svg-icons';
import { FaIconLibrary } from '@fortawesome/angular-fontawesome';
import { RegionBasicInfo } from 'src/app/models/region-basic-info';
import { RegionService } from 'src/app/services/region/region.service';
import { TouristSpotService } from 'src/app/services/tourist-spot/tourist-spot.service';
@Component({
  selector: 'app-tourist-spot',
  templateUrl: './tourist-spot.component.html',
  styleUrls: ['./tourist-spot.component.css']
})
export class TouristSpotComponent implements OnInit {
  showErrors = false;
  showSuccess = false;
  fileToUpload = null;
  regions$: Observable<RegionBasicInfo[]>;
  categories$: Observable<CategoryDetailedInfo[]>;
  spotForm = this.formBuilder.group({
    name: ['', [Validators.required]],
    description: ['', [Validators.required]],
    regionId: ['', [Validators.required]],
    image: ['', [Validators.required]]
  })
  selectedRegion = null;

  activeCategories = {}

  handleFileInput(files){
    this.fileToUpload = files.item(0);
    document.getElementById('inputFile-label').innerHTML = files.item(0).name;
  }

  constructor(private formBuilder: FormBuilder, 
    private touristSpotService: TouristSpotService,
    categoryService: CategoryService,
    regionService: RegionService,
    library: FaIconLibrary) {
    this.categories$ = categoryService.getAll();
    this.regions$ = regionService.getAll();

    library.addIconPacks(fas); 
   }

  ngOnInit(): void {
  }

  isCategoryValid(){
    return Object.keys(this.activeCategories).filter(k => this.activeCategories[k]).length != 0
  }

  onSubmit(){
    if(this.spotForm.invalid || !this.isCategoryValid()){
      this.showErrors = true;
      return;
    }

    let params = new FormData();
    for(var key in this.spotForm.controls) {
      params.append(key,this.spotForm.controls[key].value);
    }

    for(var category in this.activeCategories){
      if(!this.activeCategories[category]) continue
      params.append('categories', this.activeCategories[category].id)
    }
    params.append('image', this.fileToUpload)

    this.touristSpotService.create(params).subscribe(val => {
      this.showErrors = false;
      this.showSuccess = true;
    }, err => {
      this.showErrors = true;
      this.showSuccess = false;

      for(var key in err){
        this.showSuccess = false;
        this.showErrors = true;
        var styledKey = key[0].toLowerCase() + key.slice(1);
        console.log(key, err)
        if(styledKey in this.spotForm.controls) {
          this.spotForm.controls[styledKey].setErrors({apiErrors: err[key]})
        }
      }
    })
  }
}
