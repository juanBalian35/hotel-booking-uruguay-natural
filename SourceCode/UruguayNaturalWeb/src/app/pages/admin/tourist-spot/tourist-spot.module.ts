import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TouristSpotComponent } from './tourist-spot.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { CategoryService } from 'src/app/services/category/category.service';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import {HoverClassDirective} from '../../../directives/hover-class.directive';



@NgModule({
  declarations: [
    TouristSpotComponent],
  imports: [
    FontAwesomeModule,
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
  ],
  providers: [CategoryService]
})
export class TouristSpotModule { }
