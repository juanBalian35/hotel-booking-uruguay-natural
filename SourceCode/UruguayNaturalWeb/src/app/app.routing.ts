import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { TouristLayoutComponent } from './layouts/tourist-layout/tourist-layout.component';
import { LoginLayoutComponent } from './layouts/login-layout/login-layout.component';
import { AdminLayoutComponent } from './layouts/admin-layout/admin-layout.component';
import { AuthGuard } from './guards/auth/auth.guard';

const routes: Routes = [
    {
      path: '',
      component: TouristLayoutComponent,
      children: [
        {
          path: '',
          loadChildren: './layouts/tourist-layout/tourist-layout.module#TouristLayoutModule'
        }
      ]
    },
    { 
      path: 'login', 
      component: LoginLayoutComponent,
      children: [
        {
          path: '',
          loadChildren: './layouts/login-layout/login-layout.module#LoginLayoutModule'
        }
      ] 
    },
    {
      path: 'admin',
      component: AdminLayoutComponent,
      canActivate: [AuthGuard],
      children: [
        {
          path: '',
          loadChildren: './layouts/admin-layout/admin-layout.module#AdminLayoutModule'
        }
      ]
    }
];

@NgModule({
  imports: [
    RouterModule.forRoot(routes, {
        onSameUrlNavigation: 'reload'
    })
  ],
  exports: [RouterModule
  ],
})
export class AppRoutingModule { }
