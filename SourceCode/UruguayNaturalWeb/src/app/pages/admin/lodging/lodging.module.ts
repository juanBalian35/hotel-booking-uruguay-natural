import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LodgingComponent } from './lodging.component';
import { CreateComponent } from './create/create.component';
import { RouterModule } from '@angular/router';
import { LodgingRoutes } from './lodging.routing';
import { EditComponent } from './edit/edit.component';
import { DeleteComponent } from './delete/delete.component';
import { TouristSpotService } from 'src/app/services/tourist-spot/tourist-spot.service';
import { RegionService } from 'src/app/services/region/region.service';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { AuthInterceptor } from 'src/app/services/auth-interceptor/auth-interceptor';
import { SessionService } from 'src/app/services/session/session.service';

@NgModule({
  declarations: [LodgingComponent, CreateComponent, EditComponent, DeleteComponent],
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    RouterModule.forChild(LodgingRoutes)
  ],
  providers: [TouristSpotService, RegionService, SessionService,
    {
      provide: HTTP_INTERCEPTORS,
      useClass: AuthInterceptor,
      multi: true
    }
  ]
})
export class LodgingModule { }
