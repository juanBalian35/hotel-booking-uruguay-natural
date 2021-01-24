import {Component, EventEmitter, Input, OnChanges, OnInit, Output, SimpleChanges} from '@angular/core';

@Component({
  selector: 'app-navigation',
  templateUrl: './pagination.component.html',
  styleUrls: ['./pagination.component.css']
})
export class PaginationComponent implements OnInit {
    private _itemsLength: number;

    @Input() set itemsLength(value: number) {

        this._itemsLength = value;

    }

    get itemsLength(): number {

        return this._itemsLength;

    }
    @Input() page: number;
    @Output() pageChange = new EventEmitter<number>();
   @Input() itemsPerPage: number;

  constructor() { }

    ngOnInit(): void {
    }

  previousPage() {
    this.page--;
    this.pageChange.emit(this.page);
  }

  nextPage() {
    this.page++;
    this.pageChange.emit(this.page);
  }


}
