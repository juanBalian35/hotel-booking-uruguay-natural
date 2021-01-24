import {Component, OnInit, TemplateRef, ViewChild} from '@angular/core';
import {FormBuilder, FormControl, Validators} from '@angular/forms';
import {BookingService} from '../../../../../../services/booking/booking.service';
import {ReviewBasicInfo} from '../../../../../../models/review-basic-info';
import {ModalDismissReasons, NgbModal} from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-create-review',
  templateUrl: './create-review.component.html',
  styleUrls: ['./create-review.component.css']
})
export class CreateReviewComponent implements OnInit {
  rate = 1;
  reviewForm;
  extraErrors = [];
  showErrors = false;
  @ViewChild('message') modalMessage: TemplateRef<any>;
  @ViewChild('confirmation') modalConfirmation: TemplateRef<any>;
  hovered = 0;
  closeResult = '';
  confirmationMessage: ReviewBasicInfo;

  constructor(private bookingService: BookingService,
              private formBuilder: FormBuilder,
              private modalService: NgbModal) {
    this.reviewForm = formBuilder.group({
      bookingId: ['', [Validators.required]],
      commentary: ['', [Validators.required]]
    });
  }

  ngOnInit(): void {
  }

  openMessageModal(content) {
    this.extraErrors = [];
    if (!this.reviewForm.valid) {
      this.showErrors = true;
      return;
    }
    this.showErrors = false;
    this.createReview(content);
  }
  createReview(content) {
    const reviewParams = {
      'rating': this.rate,
      'commentary': this.reviewForm.controls.commentary.value
    };

    this.bookingService.createReview(this.reviewForm.controls.bookingId.value, reviewParams)
        .subscribe(confirmation => {this.confirmationMessage = confirmation;
          this.modalService.open(content, {ariaLabelledBy: 'modal-message', centered: true, size: 'lg'}).result.then((result) => {
            this.closeResult = `${result}`;
          }, (reason) => {
            this.closeResult = `Dismissed ${this.getDismissReason(reason)}`;
          });}  , err => {
          for( const key in err) {
            this.showErrors = true;
            const styledKey = key[0].toLowerCase() + key.slice(1);
            if (styledKey in this.reviewForm.controls) {
              this.reviewForm.controls[styledKey].setErrors({apiErrors: this.translateMessage(err[key])});
            }
          }
  });
}
 translateMessage(messages: string[]){
    console.log(messages);
    let spanishMessages: string[] = [];
    for (const msg in messages) {
      switch (messages[msg]) {
        case 'Was not found': spanishMessages.push('La reserva no se ha encontrado');
        break;
        case 'Has to be unique': spanishMessages.push('Ya existe una reseña para esta reserva');
        break;
      }
      console.log(spanishMessages);
      return spanishMessages;
    }

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
}
