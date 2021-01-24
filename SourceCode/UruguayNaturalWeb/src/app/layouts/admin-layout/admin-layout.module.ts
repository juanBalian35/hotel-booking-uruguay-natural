import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { AdminLayoutRoutes } from './admin-layout.routing';
import { SharedModule } from 'src/app/shared/shared.module';
import { LodgingModule } from 'src/app/pages/admin/lodging/lodging.module';
import { ReportModule } from 'src/app/pages/admin/report/report.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { AuthInterceptor } from 'src/app/services/auth-interceptor/auth-interceptor';
import { TouristSpotModule } from 'src/app/pages/admin/tourist-spot/tourist-spot.module';
import { BulkImportsModule } from 'src/app/pages/admin/bulk-imports/bulk-imports.module';
import { BookingStateModule } from 'src/app/pages/admin/booking-state/booking-state.module';

@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    SharedModule,
    LodgingModule,
    ReportModule,
    TouristSpotModule,
    BulkImportsModule,
    BookingStateModule,
    RouterModule.forChild(AdminLayoutRoutes)
  ]
})
export class AdminLayoutModule { }
