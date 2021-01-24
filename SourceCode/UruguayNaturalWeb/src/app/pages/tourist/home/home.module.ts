import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';

import { HomeComponent } from './home.component';
import { RegionColComponent} from './region-col/region-col.component';
import {RegionService} from '../../../services/region/region.service';
import { HttpClient, HttpClientModule } from '@angular/common/http';

@NgModule({
    imports: [
        CommonModule,
        RouterModule,
        HttpClientModule
    ],
    declarations: [
        HomeComponent,
        RegionColComponent
    ],
    exports: [ HomeComponent ],
    providers: [ RegionService ]
})
export class HomeModule { }
