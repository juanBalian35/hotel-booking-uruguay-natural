import { Routes } from '@angular/router';
import { HomeComponent } from 'src/app/pages/tourist/home/home.component';
import { RegionComponent } from 'src/app/pages/tourist/region/region.component';
import { TouristSpotComponent } from 'src/app/pages/tourist/tourist-spot/tourist-spot.component';

export const TouristLayoutRoutes: Routes = [
    { path: '', component: HomeComponent },
    { path: 'region/:id', component: RegionComponent },
    { path: 'touristspot/:id', component: TouristSpotComponent },
  ]