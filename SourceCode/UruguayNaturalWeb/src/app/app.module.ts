import { BrowserModule } from '@angular/platform-browser';
import {LOCALE_ID, NgModule} from '@angular/core';
import { FormsModule } from '@angular/forms';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { RouterModule } from '@angular/router';
import { AppRoutingModule } from './app.routing';
import { AppComponent } from './app.component';
import { SharedModule } from './shared/shared.module';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { TouristLayoutComponent } from './layouts/tourist-layout/tourist-layout.component';
import { LoginLayoutComponent } from './layouts/login-layout/login-layout.component';
import { AdminLayoutComponent } from './layouts/admin-layout/admin-layout.component';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { AuthInterceptor } from './services/auth-interceptor/auth-interceptor';
import {registerLocaleData} from '@angular/common';
import { BulkImportsComponent } from './pages/admin/bulk-imports/bulk-imports.component';
import {HoverClassDirective} from './directives/hover-class.directive';
import localeEs from '@angular/common/locales/es-UY';
registerLocaleData(localeEs);

@NgModule({
  declarations: [
    AppComponent,
    TouristLayoutComponent,
    LoginLayoutComponent,
    AdminLayoutComponent,
    HoverClassDirective
  ],
  imports: [
    BrowserModule,
    NgbModule,
    FormsModule,
    HttpClientModule,
    RouterModule,
    AppRoutingModule,
    SharedModule,
    FontAwesomeModule
  ],
  providers: [
    {provide: HTTP_INTERCEPTORS, useClass: AuthInterceptor, multi: true},
    { provide: LOCALE_ID, useValue: 'es-UY' }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
