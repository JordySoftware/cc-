import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { SegurosRoutingModule } from './seguros-routing.module';
import { HomeComponent } from './pages/home/home.component';
import { ListadoSegurosComponent } from './pages/listado-seguros/listado-seguros.component';
import { SeguroComponent } from './pages/seguro/seguro.component';
import { AgregarSeguroComponent } from './pages/agregar-seguro/agregar-seguro.component';
import { MaterialModule } from '../material/material.module';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ConfirmarEliminacionComponent } from './components/confirmar-eliminacion/confirmar-eliminacion.component';



@NgModule({
  declarations: [
    HomeComponent,
    ListadoSegurosComponent,
    SeguroComponent,
    AgregarSeguroComponent,
    ConfirmarEliminacionComponent,
  ],
  imports: [
    CommonModule,
    SegurosRoutingModule,
    MaterialModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule
  ],
  
})
export class SegurosModule { }
