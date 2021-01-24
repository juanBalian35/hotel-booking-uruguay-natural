import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { TouristLayoutRoutes } from './tourist-layout.routing';
import { SharedModule } from 'src/app/shared/shared.module';
import { HttpClientModule } from '@angular/common/http';
import { HomeModule } from 'src/app/pages/tourist/home/home.module';
import { RegionModule } from 'src/app/pages/tourist/region/region.module';
import { TouristSpotModule } from 'src/app/pages/tourist/tourist-spot/tourist-spot.module';

@NgModule({
  imports: [
    CommonModule,
    SharedModule,
    HttpClientModule,
    TouristSpotModule,
    HomeModule,
    RegionModule,
    RouterModule.forChild(TouristLayoutRoutes)
  ],
  declarations: []
})
export class TouristLayoutModule { }
