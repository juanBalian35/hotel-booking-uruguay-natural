import { NgModule } from '@angular/core';
import {FooterComponent} from './layout/footer/footer.component';
import {NavbarComponent} from './layout/navbar/navbar.component';
import {RouterModule} from '@angular/router';
import { PaginationComponent } from './components/pagination/pagination.component';
import {CommonModule} from '@angular/common';
import {FontAwesomeModule} from '@fortawesome/angular-fontawesome';
import { HttpLoadingSpinnerComponent } from './components/http-loading-spinner/http-loading-spinner.component';
import { SidebarComponent } from './layout/sidebar/sidebar.component';

@NgModule({
    imports: [
        RouterModule,
        CommonModule,
        FontAwesomeModule,
    ],
    declarations: [
        FooterComponent,
        NavbarComponent,
        SidebarComponent,
        PaginationComponent,
        HttpLoadingSpinnerComponent,
    ],
    exports: [
        FooterComponent,
        NavbarComponent,
        SidebarComponent,
        PaginationComponent,
        HttpLoadingSpinnerComponent,
    ],
    providers: []
})
export class SharedModule { }
