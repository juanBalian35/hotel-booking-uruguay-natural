import { Component, Input, OnInit, TemplateRef, ViewChild } from '@angular/core';
import { SearchLodging } from 'src/app/models/search-lodging'
import {BookingService} from 'src/app/services/booking/booking.service';
import {BookingBasicInfo} from 'src/app/models/booking-basic-info';
import {ModalDismissReasons, NgbModal} from '@ng-bootstrap/ng-bootstrap';
import {LodgingSearchBasicInfo} from 'src/app/models/lodging-search-basic-info';
import {FormBuilder, Validators} from '@angular/forms';

@Component({
  selector: 'app-booking',
  templateUrl: './booking.component.html',
  styleUrls: ['./booking.component.css']
})
export class BookingComponent implements OnInit {
@Input() bookingDetails: SearchLodging;
@Input() lodging: LodgingSearchBasicInfo;

  private confirmationMessage: BookingBasicInfo = {
    confirmationMessage: '',
    id: 0,
    phone: ''
  };
  bookingForm;
  extraErrors = [];
  showErrors = false;
  @ViewChild('message') modalMessage: TemplateRef<any>;
  @ViewChild('confirmation') modalConfirmation: TemplateRef<any>;

  closeResult = '';
  constructor(private bookingService: BookingService,
              private modalService: NgbModal,
              private formBuilder: FormBuilder) {
    this.bookingForm = formBuilder.group({
      email: ['', [Validators.required, Validators.pattern(/^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$/)]],
      name: ['', [Validators.required]],
      lastname: ['', [Validators.required]],
    });
  }

  ngOnInit(): void {
  }
  createBooking() {
    const bookingParams = {
      'checkin': this.bookingDetails.checkIn,
      'checkout': this.bookingDetails.checkOut ,
      'retirees': this.bookingDetails.retirees ,
      'adults': this.bookingDetails.adults ,
      'babies': this.bookingDetails.babies ,
      'children': this.bookingDetails.children ,
      'lodging': this.lodging.id ,
      'name': this.bookingForm.controls.name.value ,
      'lastname': this.bookingForm.controls.lastname.value ,
      'email': this.bookingForm.controls.email.value ,
    };
    this.bookingService.create(bookingParams).subscribe(confirmation => this.confirmationMessage = confirmation);
  }


  openConfirmationModal(content) {
    this.extraErrors = [];
    if (!this.bookingForm.valid) {
      this.showErrors = true;
      return;
    }

    this.showErrors = false;
    this.modalService.open(content, {ariaLabelledBy: 'modal-basic-title', size: 'lg', centered: true}).result.then((result) => {
      this.closeResult = `${result}`;
      if (this.closeResult) {
        this.createBooking();
        this.openMessageModal(this.modalMessage);
      }
    }, (reason) => {
      this.closeResult = `Dismissed ${this.getDismissReason(reason)}`;
    });
  }

  private getDismissReason(reason: any): string {
    if (reason === ModalDismissReasons.ESC) {
      return 'by pressing ESC';
    } else if (reason === ModalDismissReasons.BACKDROP_CLICK) {
      return 'by clicking on a backdrop';
    } else {
      return `with: ${reason}`;
    }
  }
  openMessageModal(content) {
    this.modalService.open(content, {ariaLabelledBy: 'modal-message', centered: true, size: 'lg'}).result.then((result) => {
      this.closeResult = `${result}`;
    }, (reason) => {
      this.closeResult = `Dismissed ${this.getDismissReason(reason)}`;
    });
  }
}
