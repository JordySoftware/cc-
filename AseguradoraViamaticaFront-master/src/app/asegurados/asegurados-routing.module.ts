import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeAseguradosComponent } from './pages/home-asegurados/home-asegurados.component';
import { ListadoAseguradosComponent } from './pages/listado-asegurados/listado-asegurados.component';
import { AgregarAseguradoComponent } from './pages/agregar-asegurado/agregar-asegurado.component';
import { AseguradosComponent } from './pages/asegurados/asegurados.component';

const routes: Routes = [
  {
    path: '',
    component: HomeAseguradosComponent,
    children: [
      {
        path: 'listado',
        component: ListadoAseguradosComponent
      },
      {
        path: 'agregar',
        component: AgregarAseguradoComponent
      },
      {
        path: 'editar/:id',
        component: AgregarAseguradoComponent,
      },
      {
        path: ':id',
        component: AseguradosComponent
      },
      {
        path: '**',
        component: HomeAseguradosComponent
      }
    ]
  }
];

@NgModule({
  declarations: [],
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AseguradosRoutingModule { }
