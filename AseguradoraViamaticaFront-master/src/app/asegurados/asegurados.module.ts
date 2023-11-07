import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { AseguradosRoutingModule } from './asegurados-routing.module';
import { HomeAseguradosComponent } from './pages/home-asegurados/home-asegurados.component';
import { AseguradosComponent } from './pages/asegurados/asegurados.component';
import { ListadoAseguradosComponent } from './pages/listado-asegurados/listado-asegurados.component';
import { AgregarAseguradoComponent } from './pages/agregar-asegurado/agregar-asegurado.component';
import { MaterialModule } from '../material/material.module';
import { FormsModule } from '@angular/forms';
import { ConfirmarComponent } from './components/confirmar/confirmar.component';




@NgModule({
  declarations: [
    HomeAseguradosComponent,
    AseguradosComponent,
    ListadoAseguradosComponent,
    AgregarAseguradoComponent,
    ConfirmarComponent,
  ],
  imports: [
    CommonModule,
    AseguradosRoutingModule,
    MaterialModule,
    FormsModule
  ]
})
export class AseguradosModule { }
