import { Component, OnInit } from '@angular/core';
import { Asegurado } from '../../interfaces/asegurado.interface';
import { AseguradosService } from '../../services/asegurados.service';
import { Seguro } from 'src/app/seguros/interfaces/seguro.interface';
import { MatDialog } from '@angular/material/dialog';
import { AgregarAseguradoComponent } from '../agregar-asegurado/agregar-asegurado.component';
import { ConfirmarComponent } from '../../components/confirmar/confirmar.component';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-listado-asegurados',
  templateUrl: './listado-asegurados.component.html',
  styleUrls: ['./listado-asegurados.component.css']
})
export class ListadoAseguradosComponent implements OnInit {
  asegurados: Asegurado[] = [];
  seguros: Seguro[] = [];
  cedula: string = '';
  error: string = '';
  asegurado!: Asegurado;

  constructor(private aseguradosService: AseguradosService, private dialog:MatDialog, private snackBar: MatSnackBar,
    private activatedRoute: ActivatedRoute,
    private router: Router) {}

  ngOnInit(): void {
    this.aseguradosService.getAsegurados().subscribe(asegurado => {
      this.asegurados = asegurado;
      console.log(this.asegurados);
    });

    
  }

  getSeguros() {
    this.error = '';

    if (this.cedula.trim() === '') {
      this.error = 'Ingrese un número de cédula.';
      return;
    }

    const aseguradoEncontrado = this.asegurados.find(a => a.cedula === this.cedula.trim());

    if (!aseguradoEncontrado) {
      this.error = 'El número de cédula no coincide con ningún asegurado.';
      return;
    }

    this.aseguradosService.getSegurosCedula(this.cedula.trim()).subscribe(
      seguros => {
        this.seguros = seguros;
        console.log(this.seguros);

        if (seguros.length === 0) {
          this.error = 'El asegurado con cédula ' + this.cedula + ' no tiene seguros asociados.';
        }
      },
      error => {
        this.error = 'Ocurrió un error al obtener los seguros.';
        console.error(error);
      }
    );
    this.aseguradosService.getAseguradoPorCedula(this.cedula)
    .subscribe(asegurado => {
      this.asegurado = asegurado;
      console.log("aseguradoDatos: ", this.asegurado);
    });
  }

  agregar(){
    const dialog =this.dialog.open(AgregarAseguradoComponent,{
      width: '1000px',
      height: '600px'
    });

  }
  eliminarAsegurado(asegurado: Asegurado): void {
    console.log(asegurado);
    const dialog = this.dialog.open(ConfirmarComponent, {
      width: '500px',
      data: asegurado
    });
    dialog.afterClosed().subscribe((result) => {
      if (result) {
        this.aseguradosService.eliminarAsegurado(asegurado.id).subscribe(() => {
          this.mostrarSnackbar('Registro Eliminado');
          // Actualizar la lista de asegurados después de eliminar
          this.asegurados = this.asegurados.filter(a => a.id !== asegurado.id);
        }, (error) => {
          console.log(error);
          // Manejar el error en caso de que ocurra
        });
      }
    });
  }
  
  

  mostrarSnackbar(mensaje: string): void {
    this.snackBar.open(mensaje, 'Ok!', {
      duration: 2500
    });
  }

}
