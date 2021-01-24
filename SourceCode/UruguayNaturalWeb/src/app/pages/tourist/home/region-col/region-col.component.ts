import {Component, ElementRef, Input, OnInit, ViewChild} from '@angular/core';

@Component({
  selector: 'app-region-col',
  templateUrl: './region-col.component.html',
  styleUrls: ['./region-col.component.css']
})
export class RegionColComponent implements OnInit {

  constructor() { }
  active = false;

  @Input() region: string;
  @Input() video: string;
  @Input() map;

  @ViewChild('videoPlayer') videoplayer: ElementRef;

  ngOnInit(): void {
  }
  playVideo() {
    this.active = true;
    this.videoplayer.nativeElement.play();
    this.videoplayer.nativeElement.muted = 'muted';

  }
  pauseVideo() {
    this.active = false;
    this.videoplayer.nativeElement.pause();
  }
  void() {}

  charArrayToPng(uints: Uint8Array) {
    return 'data:image/png;base64,' + uints;
  }
}
