import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AseguradosModule } from './asegurados/asegurados.module';

const routes: Routes = [
  {
    path: 'asegurados',
    loadChildren: () => import('./asegurados/asegurados.module').then(m => m.AseguradosModule)
  },
  {
    path: 'seguros',
    loadChildren: () => import('./seguros/seguros.module').then(m => m.SegurosModule)
  },

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
