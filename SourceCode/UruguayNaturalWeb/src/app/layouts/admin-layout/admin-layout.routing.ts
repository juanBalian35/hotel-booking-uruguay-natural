import { Routes } from '@angular/router';
import { ReportComponent } from 'src/app/pages/admin/report/report.component';
import { LodgingComponent } from 'src/app/pages/admin/lodging/lodging.component';
import { AdministratorComponent } from 'src/app/pages/admin/administrator/administrator.component';
import { TouristSpotComponent } from 'src/app/pages/admin/tourist-spot/tourist-spot.component';
import { BulkImportsComponent } from 'src/app/pages/admin/bulk-imports/bulk-imports.component';
import { BookingStateComponent } from 'src/app/pages/admin/booking-state/booking-state.component';

export const AdminLayoutRoutes: Routes = [
    { path: 'report', component: ReportComponent },
    { path: '', redirectTo: 'report' },
    { 
      path: 'lodging', component: LodgingComponent,
      children: [
        {
          path: '',
          loadChildren: 'src/app/pages/admin/lodging/lodging.module#LodgingModule'
        }
      ]
    },
    { 
      path: 'administrator', component: AdministratorComponent,
      children: [
        {
          path: '',
          loadChildren: 'src/app/pages/admin/administrator/administrator.module#AdministratorModule'
        }
      ]
    },
    { path: 'bulkimport', component: BulkImportsComponent },
    { path: 'touristspot', component: TouristSpotComponent },
    { path: 'bookingstate', component: BookingStateComponent },
  ]