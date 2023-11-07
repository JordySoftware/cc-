import { Component, OnInit } from '@angular/core';
import { Seguro } from '../../interfaces/seguro.interface';
import { SegurosService } from '../../services/seguros.service';
import { Asegurado } from 'src/app/asegurados/interfaces/asegurado.interface';
import { MatDialog } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ConfirmarEliminacionComponent } from '../../components/confirmar-eliminacion/confirmar-eliminacion.component';

@Component({
  selector: 'app-listado-seguros',
  templateUrl: './listado-seguros.component.html',
  styleUrls: ['./listado-seguros.component.css']
})
export class ListadoSegurosComponent implements OnInit {
  asegurados: Asegurado[] = [];
  seguros: Seguro[] = [];
  codigo!: number;
  error: string = '';
  seguroDetalle: Seguro[] =[];

  constructor(private segurosService: SegurosService, private dialog:MatDialog, private snackBar: MatSnackBar,) { }

  ngOnInit(): void {
    this.segurosService.getSeguros().subscribe(seguros => {
      this.seguros = seguros;
      console.log(this.seguros);
    });
  }

  getAsegurados() {
    if (this.codigo <= 0) {
      this.error = 'Ingrese un código válido.';
      return;
    }

    this.segurosService.getAseguradosPorCodigo(this.codigo).subscribe(
      asegurados => {
        this.asegurados = asegurados;
        this.error = '';

        this.segurosService.getSeguroPorCodigo(this.codigo).subscribe(
          seguro => {
            this.seguroDetalle = seguro;
            console.log(this.seguroDetalle);
          },
          error => {
            console.error(error);
            this.error = 'Ocurrió un error al obtener los datos del seguro.';
          }
        );
      },
      error => {
        this.asegurados = [];
        this.error = 'No se encontraron asegurados para el código ingresado.';
      }
    );
  }

  eliminarSeguro(seguro: Seguro): void {
    const dialog = this.dialog.open(ConfirmarEliminacionComponent, {
      width: '500px',
      data: seguro
    });
    dialog.afterClosed().subscribe((result) => {
      if (result) {
        this.segurosService.eliminarSeguro(seguro.id).subscribe(() => {
          this.mostrarSnackbar('Registro Eliminado');
          // Actualizar la lista de asegurados después de eliminar
          this.seguros = this.seguros.filter(a => a.id !== seguro.id);
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

