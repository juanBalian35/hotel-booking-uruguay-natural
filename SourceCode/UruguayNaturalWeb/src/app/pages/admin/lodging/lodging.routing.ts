import { Routes } from '@angular/router';
import { CreateComponent } from './create/create.component';
import { DeleteComponent } from './delete/delete.component';
import { EditComponent } from './edit/edit.component';

export const LodgingRoutes: Routes = [
    { path: 'create', component: CreateComponent },
    { path: 'edit', component: EditComponent },
    { path: 'delete', component: DeleteComponent },
  ]