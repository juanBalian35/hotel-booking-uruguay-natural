import { Component, OnInit } from '@angular/core';
import {TouristspotBasicInfo} from '../../../models/touristspot-basic-info';
import {Observable} from 'rxjs';
import {RegionBasicInfo} from '../../../models/region-basic-info';
import {FormArray, FormBuilder, FormControl, Validators} from '@angular/forms';
import {LodgingService} from '../../../services/lodging/lodging.service';
import {RegionService} from '../../../services/region/region.service';
import {SearchLodging} from '../../../models/search-lodging';
import {TouristSpotService} from '../../../services/tourist-spot/tourist-spot.service';
import {NgbCalendar, NgbDate, NgbDateParserFormatter} from '@ng-bootstrap/ng-bootstrap';
import {FaIconLibrary} from '@fortawesome/angular-fontawesome';
import {fas} from '@fortawesome/free-solid-svg-icons';
import {Admin} from '../../../models/admin';
import {ReportService} from '../../../services/report/report.service';
import {ReportBasicInfo} from '../../../models/report-basic-info';

@Component({
  selector: 'app-report',
  templateUrl: './report.component.html',
  styleUrls: ['./report.component.css']
})
export class ReportComponent implements OnInit {
  selectedTouristSpot: TouristspotBasicInfo = null;
  selectedRegion: RegionBasicInfo = null;
  regions$: Observable<RegionBasicInfo[]>;
  touristSpots$: Observable<TouristspotBasicInfo[]>;
  fromDate: NgbDate = null;
  toDate: NgbDate = null;
  dateValid = true;
  hoveredDate: NgbDate | null = null;
  reports: ReportBasicInfo[] = [];
  showEmpty = false;
  showErrors = false;
  showSuccess = false;

  constructor(private formBuilder: FormBuilder,
              private touristspotService: TouristSpotService,
              private regionService: RegionService,
              private reportService: ReportService,
              public calendar: NgbCalendar,
              public formatter: NgbDateParserFormatter,
              library: FaIconLibrary) {
    library.addIconPacks(fas);
  }

  reportForm = this.formBuilder.group({
    region: ['', [Validators.required]],
    touristSpot: ['', [Validators.required]],
    dateFrom: ['', [Validators.required]],
    dateTo: ['', [Validators.required]],

  });

  ngOnInit(): void {
    this.regions$ = this.regionService.getAll();
  }

  selectRegion($event) {
    this.showSuccess = false;
    this.touristSpots$ = this.touristspotService.getAll([], $event.target.value, 1, 1000);
    this.selectedTouristSpot = null;
  }

  selectTouristSpot(value) {
    this.showSuccess = false;
    if (!value) {
      return;
    }
    this.selectedTouristSpot = value;
  }

  onDateSelection(date: NgbDate) {
    if (!this.fromDate && !this.toDate) {
      this.fromDate = date;
    } else if (this.fromDate && !this.toDate && date && date.after(this.fromDate)) {
      this.toDate = date;
    } else {
      this.toDate = null;
      this.fromDate = date;
    }
    this.reportForm.controls.dateFrom.setValue(this.formatter.format(this.fromDate));
    this.reportForm.controls.dateTo.setValue(this.formatter.format(this.toDate));
  }

  isHovered(date: NgbDate) {
    return this.fromDate && !this.toDate && this.hoveredDate && date.after(this.fromDate) && date.before(this.hoveredDate);
  }

  isInside(date: NgbDate) {
    return this.toDate && date.after(this.fromDate) && date.before(this.toDate);
  }

  isRange(date: NgbDate) {
    return date.equals(this.fromDate) || (this.toDate && date.equals(this.toDate)) || this.isInside(date) || this.isHovered(date);
  }

  validateInput(currentValue: NgbDate | null, input: string): NgbDate | null {
    const parsed = this.formatter.parse(input);
    return parsed && this.calendar.isValid(NgbDate.from(parsed)) ? NgbDate.from(parsed) : currentValue;
  }

  onSubmit(value: any) {
    if (this.reportForm.invalid) {
      this.showErrors = true;
      this.showSuccess = false;
      return;
    }
    this.showErrors = false;
    this.showSuccess = true;

    const fromDate = new Date(this.fromDate.year, this.fromDate.month - 1, this.fromDate.day).toJSON();
    const toDate = new Date(this.toDate.year, this.toDate.month - 1, this.toDate.day).toJSON();

    this.reportService.getReport(fromDate, toDate, this.selectedTouristSpot.id)
      .subscribe(reports => {
        this.showEmpty = (reports.length === 0);
        this.reports = reports;
      }, err => {
        this.showErrors = true;
        this.showSuccess = false;

        for(var key in err){
          this.showErrors = true;
          var styledKey = key[0].toLowerCase() + key.slice(1);
          console.log(key, err)
          if(styledKey in this.reportForm.controls) {
            this.reportForm.controls[styledKey].setErrors({apiErrors: err[key]})
          }
        }
      });
  }
}
