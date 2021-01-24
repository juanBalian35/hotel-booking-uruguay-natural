import {Component, Input, OnInit, ViewEncapsulation} from '@angular/core';
import {LodgingSearchBasicInfo} from '../../../../../../models/lodging-search-basic-info';
import {LodgingImageBasicInfo} from '../../../../../../models/lodging-image-basic-info';

@Component({
  selector: 'app-image-gallery',
  templateUrl: './image-gallery.component.html',
  styleUrls: ['./image-gallery.component.css'],
  encapsulation: ViewEncapsulation.None
})
export class ImageGalleryComponent implements OnInit {
  @Input() images: LodgingImageBasicInfo[];
  constructor() { }

  ngOnInit(): void {
  }

  charArrayToPng(uints: Uint8Array) {
    return 'data:image/png;base64,' + uints;
  }
}
