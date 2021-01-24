import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { BookingStateBasicInfo } from 'src/app/models/booking-state-basic-info';
import { BookingService } from 'src/app/services/booking/booking.service';

@Component({
  selector: 'app-booking-state',
  templateUrl: './booking-state.component.html',
  styleUrls: ['./booking-state.component.css']
})
export class BookingStateComponent implements OnInit {
  showErrors = false;
  showErrorsNew = false;
  showSuccess = false;
  stateForm = this.formBuilder.group({
    bookingId: ['', [Validators.required]]
  });
  newStateForm = this.formBuilder.group({
    name: ['', [Validators.required]],
    description: ['', [Validators.required]]
  });;
  stateInfo: BookingStateBasicInfo = null;
  selectedBookingId: number = null;
  constructor(private formBuilder: FormBuilder, private bookingService: BookingService) { 

  }

  ngOnInit(): void {
  }

  onStateSubmit(){
    this.showErrorsNew = false;
    if(this.stateForm.invalid){
      this.showErrors = true;
      this.stateInfo = null;
      this.showSuccess = false;
      return;
    }

    this.selectedBookingId = this.stateForm.controls.bookingId.value;
    this.bookingService.getStates(this.selectedBookingId).subscribe(val => {
        this.newStateForm.reset();
        this.stateInfo = val.sort(x => x.id)[val.length - 1]
      }, err => {
        this.showErrors = true;
        this.stateInfo = null;
  
        this.showSuccess = false;
        for(var key in err){
          this.showErrors = true;
          var styledKey = key[0].toLowerCase() + key.slice(1);
          console.log(key, err)
          if(styledKey in this.stateForm.controls) {
            this.stateForm.controls[styledKey].setErrors({apiErrors: err[key]})
          }
        }
      })
  }

  changeState() {
    if(this.newStateForm.invalid){
      this.showErrorsNew = true;
      this.showSuccess = false;
      return;
    }

    let params = {
      'state': this.newStateForm.controls.name.value,
      'description': this.newStateForm.controls.description.value
    }
    this.bookingService.createState(this.selectedBookingId, params).subscribe(val => {
      this.showSuccess = true;
    },err => {
      for(var key in err){
        var styledKey = key[0].toLowerCase() + key.slice(1);
        if(styledKey in this.newStateForm.controls){
          this.newStateForm.controls[styledKey].setErrors({apiErrors: err[key]})
        }
      }
      this.showErrorsNew = true;
      this.showSuccess = false;
  })
  }
}
