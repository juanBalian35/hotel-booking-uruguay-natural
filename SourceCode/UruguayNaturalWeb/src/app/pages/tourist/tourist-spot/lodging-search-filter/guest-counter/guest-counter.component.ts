import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';

@Component({
  selector: 'app-guest-counter',
  templateUrl: './guest-counter.component.html',
  styleUrls: ['./guest-counter.component.css']
})
export class GuestCounterComponent implements OnInit {
   @Input() guestType: string;
   @Input() quantity: number;
   @Input() ageRange: string;
   @Output() quantityChange = new EventEmitter<number>();

  constructor() { }

  ngOnInit(): void {
  }

  decrementQuantity() {
    if (this.quantity !== 0) {
      this.quantity--;
      this.quantityChange.emit(this.quantity);
    }
  }
  incrementQuantity() {
      this.quantity++;
    this.quantityChange.emit(this.quantity);

  }
}
