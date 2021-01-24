import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {NgbCalendar, NgbDate, NgbDateParserFormatter} from '@ng-bootstrap/ng-bootstrap';
import {SearchLodging} from '../../../../models/search-lodging';

@Component({
  selector: 'app-lodging-search-filter',
  templateUrl: './lodging-search-filter.component.html',
  styleUrls: ['./lodging-search-filter.component.css']
})
export class LodgingSearchFilterComponent {
  adultsQuantity = 1;
  childrenQuantity = 0;
  babiesQuantity = 0;
  retireeQuantity = 0;
  fromDate: NgbDate = null;
  toDate: NgbDate = null;
  dateValid = true;
  guestsValid = true;

  search: SearchLodging = {
    adults: 0,
    babies: 0,
    checkIn: '',
    checkOut: '',
    children: 0,
    page: 0,
    resultsPerPage: 0,
    retirees: 0,
    touristSpot: 0,
    isFull: false
  };
  @Output() searchChange = new EventEmitter<SearchLodging>();
  hoveredDate: NgbDate | null = null;
  errorMessage = '';

  constructor(public calendar: NgbCalendar,
              public formatter: NgbDateParserFormatter) { }
  onDateSelection(date: NgbDate) {
    if (!this.fromDate && !this.toDate) {
      this.fromDate = date;
    } else if (this.fromDate && !this.toDate && date && date.after(this.fromDate)) {
      this.toDate = date;
    } else {
      this.toDate = null;
      this.fromDate = date;
    }
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

    getPlaceHolderMessage() {
    let adults = ' Adultos - ';
    let kids = ' Niños - ';
    let babies = ' Bebés - ';
    let retirees = ' Jubilados';
    if (this.adultsQuantity === 1) {
      adults = ' Adulto - ';
    }
    if (this.childrenQuantity === 1) {
        kids =  ' Niño - ';
      }
    if (this.babiesQuantity === 1) {
      babies = ' Bebé - ';
    }
    if (this.retireeQuantity === 1) {
      retirees = ' Jubilado';
    }
     return this.adultsQuantity + adults + this.childrenQuantity  + kids + this.babiesQuantity +  babies + this.retireeQuantity +  retirees;
    }

  searchLodgings() {
    if (this.validateFields()) {
      this.search.retirees = this.retireeQuantity;
      this.search.children = this.childrenQuantity;
      this.search.babies = this.babiesQuantity;
      this.search.adults = this.adultsQuantity;
      this.search.checkIn = new Date(this.fromDate.year, this.fromDate.month - 1, this.fromDate.day).toJSON();
      this.search.checkOut = new Date(this.toDate.year, this.toDate.month - 1, this.toDate.day).toJSON();
    this.searchChange.emit(this.search);
    }
  }
  validateFields() {
    this.dateValid = true;
    this.guestsValid = true;
    this.errorMessage = '';
    if (this.fromDate === null || this.toDate === null) {
      this.errorMessage = 'Debe ingresar un rango de fechas';
      this.dateValid = false;
      return false;
    }
    if (this.adultsQuantity === 0 && this.retireeQuantity === 0) {
      this.errorMessage = 'Debe ingresar al menos un adulto o jubilado';
      this.guestsValid = false;
      return false;
    }
    return true;
  }
}
