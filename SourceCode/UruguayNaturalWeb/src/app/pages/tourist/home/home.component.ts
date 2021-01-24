import { Component, OnInit } from '@angular/core';
import {RegionDetailedInfo} from '../../../models/region-detailed-info';
import {RegionService} from '../../../services/region/region.service';

@Component({
    selector: 'app-home',
    templateUrl: './home.component.html',
    styleUrls: ['./home.component.css']
})

export class HomeComponent implements OnInit {

    regions: RegionDetailedInfo [] = [];
    constructor(private regionService: RegionService) { }

    ngOnInit(): void {
        this.regionService.getAll().subscribe(regions => this.regions = regions);
    }
}
