import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LoginLayoutComponent } from './login-layout.component';
import { SharedModule } from 'src/app/shared/shared.module';
import { RouterModule } from '@angular/router';
import { LoginLayoutRoutes } from './login-layout.routing';
import { LoginModule } from 'src/app/pages/admin/login/login.module';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';

@NgModule({
  imports: [
    CommonModule,
    SharedModule,
    HttpClientModule,
    LoginModule,
    RouterModule.forChild(LoginLayoutRoutes)
  ],
  declarations: [ ]
})
export class LoginLayoutModule { }
