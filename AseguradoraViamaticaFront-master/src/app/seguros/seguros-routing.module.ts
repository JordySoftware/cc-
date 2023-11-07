import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './pages/home/home.component';
import { ListadoSegurosComponent } from './pages/listado-seguros/listado-seguros.component';
import { AgregarSeguroComponent } from './pages/agregar-seguro/agregar-seguro.component';
import { SeguroComponent } from './pages/seguro/seguro.component';

const routes: Routes = [
  {
    path: '',
    component: HomeComponent,
    children: [
      {
        path: 'listado',
        component: ListadoSegurosComponent
      },
      {
        path: 'agregar',
        component: AgregarSeguroComponent
      },
      {
        path: 'editar/:id',
        component: AgregarSeguroComponent,
      },
      {
        path: ':id',
        component: SeguroComponent 
      },
      {
        path: '**',
        component: HomeComponent 
      },
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class SegurosRoutingModule { }
