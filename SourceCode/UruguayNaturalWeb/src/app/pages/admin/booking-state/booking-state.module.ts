import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BookingStateComponent } from './booking-state.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BookingService } from 'src/app/services/booking/booking.service';



@NgModule({
  declarations: [BookingStateComponent],
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule
  ],
  providers: [BookingService]
})
export class BookingStateModule { }
