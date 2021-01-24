import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReportComponent } from './report.component';
import { RouterModule } from '@angular/router';
import {ReactiveFormsModule} from '@angular/forms';
import {NgbDatepickerModule} from '@ng-bootstrap/ng-bootstrap';
import {FontAwesomeModule} from '@fortawesome/angular-fontawesome';
import {ReportService} from '../../../services/report/report.service';

@NgModule({
  declarations: [ReportComponent],
    imports: [
        RouterModule,
        CommonModule,
        ReactiveFormsModule,
        NgbDatepickerModule,
        FontAwesomeModule
    ],
    providers: [ReportService]

})
export class ReportModule { }
