import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { SessionService } from 'src/app/services/session/session.service';

declare interface RouteInfo {
    path: string;
    title: string;
    icon: string;
    class: string;
}
export const ROUTES: RouteInfo[] = [
    { path: '/admin/report', title: 'Reportes',  icon:'ni-tv-2 text-primary', class: '' },
    { path: '/admin/lodging/create', title: 'Hospedajes',  icon:'ni-pin-3 text-orange', class: '' },
    { path: '/admin/administrator/create', title: 'Administradores',  icon:'ni-single-02 text-yellow', class: '' },
    { path: '/admin/touristspot', title: 'Puntos turisticos',  icon:'ni-single-02 text-yellow', class: '' },
    { path: '/admin/bookingstate', title: 'Estado de reservas',  icon:'ni-bullet-list-67 text-red', class: '' },
    { path: '/admin/bulkimport', title: 'Importaciones en masa',  icon:'ni-key-25 text-info', class: '' }
];

@Component({
  selector: 'app-sidebar',
  templateUrl: './sidebar.component.html',
  styleUrls: ['./sidebar.component.scss']
})
export class SidebarComponent implements OnInit {

  public menuItems: any[];
  public isCollapsed = true;

  constructor(private sessionService: SessionService, private router: Router) { }

  ngOnInit() {
    this.menuItems = ROUTES.filter(menuItem => menuItem);
    this.router.events.subscribe((event) => {
      this.isCollapsed = true;
   });
  }

  logOut(){
    this.sessionService.logOut();
    this.router.navigateByUrl('/');
  }
}
