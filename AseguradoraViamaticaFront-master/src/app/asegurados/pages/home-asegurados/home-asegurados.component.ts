import { Component, OnInit } from '@angular/core';
import { Asegurado } from '../../interfaces/asegurado.interface';
import { AseguradosService } from '../../services/asegurados.service';
import { Seguro } from 'src/app/seguros/interfaces/seguro.interface';
import { MatDialog } from '@angular/material/dialog';
import { AgregarAseguradoComponent } from '../agregar-asegurado/agregar-asegurado.component';

@Component({
  selector: 'app-home-asegurados',
  templateUrl: './home-asegurados.component.html',
  styleUrls: ['./home-asegurados.component.css']
})
export class HomeAseguradosComponent  {
  

}
