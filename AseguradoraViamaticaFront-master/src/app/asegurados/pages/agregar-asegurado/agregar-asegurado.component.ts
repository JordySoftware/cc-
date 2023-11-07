import { Component, OnInit } from '@angular/core';
import { AseguradoCreacion, SeguroElementCreacion } from '../../interfaces/aseguradoCreacion.interface';
import { Asegurado, SeguroElement } from '../../interfaces/asegurado.interface';
import { AseguradosService } from '../../services/asegurados.service';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, Router } from '@angular/router';
import { Seguro } from 'src/app/seguros/interfaces/seguro.interface';
import { SegurosService } from 'src/app/seguros/services/seguros.service';

@Component({
  selector: 'app-agregar-asegurado',
  templateUrl: './agregar-asegurado.component.html',
  styleUrls: ['./agregar-asegurado.component.css']
})
export class AgregarAseguradoComponent implements OnInit {
  seguros: Seguro[] = [];
  seguroSeleccionado: Seguro | null = null;
  cedula: string = '';
  nombre: string = '';
  telefono: string = '';
  edad: number | null = null;
  showErrorMessage: boolean = false;
  aseguradoEditar!: Asegurado;
  segurosSeleccionados: SeguroElementCreacion[] = [];
  segurosVisual: SeguroElement[] = [];

  telefonoError: boolean = false;
  telefonoErrorMessage: string = '';
  edadError: boolean = false;
  edadErrorMessage: string = '';
  cedulaError: boolean = false;
  cedulaErrorMessage: string = '';

  constructor(
    private segurosService: SegurosService,
    private aseguradosService: AseguradosService,
    private snackBar: MatSnackBar,
    private activatedRoute: ActivatedRoute,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.segurosService.getSeguros().subscribe((seguros) => {
      this.seguros = seguros;
    });
  
    if (this.router.url.includes('agregar')) {
      // Código para agregar un asegurado
    }
  
    if (this.router.url.includes('editar')) {
      this.activatedRoute.params.subscribe(({ id }) => {
        this.aseguradosService.getAseguradoPorId(id).subscribe((asegurado: Asegurado) => {
          this.aseguradoEditar = asegurado;
          this.cedula = asegurado.cedula;
          this.nombre = asegurado.nombre;
          this.telefono = asegurado.telefono;
          this.edad = asegurado.edad;
          
          // Establecer los seguros seleccionados en segurosSeleccionados
          if (asegurado.seguros && asegurado.seguros.length > 0) {
            this.segurosVisual = asegurado.seguros;
            this.segurosSeleccionados = asegurado.seguros.map((seguro) => ({
              seguroId: seguro.id
            }));
            const seguroId = asegurado.seguros[0].id;
            this.seguroSeleccionado = this.seguros.find((seguro) => seguro.id === seguroId) || null;
            console.log(this.segurosSeleccionados);
          } else {
            this.segurosVisual = [];
            this.segurosSeleccionados = [];
            this.seguroSeleccionado = null;

          }
        });
      });
    }
  }
  
  

  agregarSeguros(id: number) {
    console.log('Seguro seleccionado: ', id);
    this.segurosSeleccionados.push({ seguroId: id });
  }

  agregarSegurosVisual(id: number): void {
    const seguro = this.seguros.find((seguro) => seguro.id === id);
    if (seguro && !this.segurosVisual.includes(seguro)) {
      this.segurosVisual.push(seguro);
    }
    console.log('seguros visual:', this.segurosVisual);
  }

  eliminarSeguro(index: number): void {
    const seguroEliminado = this.segurosVisual[index];
    this.segurosVisual.splice(index, 1);
    this.segurosSeleccionados = this.segurosSeleccionados.filter(seguro => seguro.seguroId !== seguroEliminado.id);
    console.log(this.segurosSeleccionados);
  }
  

  seleccionarSeguro(event: any): void {
    const seguroId = event.target.value;
    this.seguroSeleccionado =
      this.seguros.find((seguro) => seguro.id === Number(seguroId)) || null;
  }

  agregarAsegurado(): void {
    this.validarTelefono();
    this.validarEdad();
    this.validarCedula();
  
    if (
      this.camposCompletos() &&
      !this.telefonoError &&
      !this.cedulaError &&
      !this.edadError
    ) {
      const asegurado: AseguradoCreacion = {
        cedula: this.cedula,
        nombre: this.nombre,
        telefono: this.telefono,
        edad: this.edad!,
        seguros: this.segurosSeleccionados
      };
  
      if (this.router.url.includes('editar') && this.aseguradoEditar?.id) {
        this.aseguradosService.editarAsegurado(this.aseguradoEditar.id, asegurado).subscribe(() => {
          this.router.navigate(['/asegurados/listado']);
          this.mostrarSnackbar('Registro actualizado');
        });
      } else {
        this.aseguradosService.agregarAsegurado(asegurado).subscribe(() => {
          this.router.navigate(['/asegurados/listado']);
          this.mostrarSnackbar('Registro creado');
        });
      }
  
      // Restablecer los campos del formulario
      this.cedula = '';
      this.nombre = '';
      this.telefono = '';
      this.edad = null;
      this.seguroSeleccionado = null;
      this.segurosVisual = [];
      // Restablecer el estado de los mensajes de error
      this.showErrorMessage = false;
    } else {
      this.showErrorMessage = true;
    }
  }
  

  camposCompletos(): boolean {
    return (
      this.cedula !== '' &&
      this.nombre !== '' &&
      this.telefono !== '' &&
      this.edad !== null &&
      this.seguroSeleccionado !== null
    );
  }

  mostrarSnackbar(mensaje: string): void {
    this.snackBar.open(mensaje, 'Ok!', {
      duration: 2500
    });
  }

  validarTelefono(): void {
    const pattern = /^\d+$/;
    if (this.telefono !== '') {
      if (!pattern.test(this.telefono)) {
        this.telefonoError = true;
        this.telefonoErrorMessage = 'Ingrese un número de teléfono válido.';
      } else {
        this.telefonoError = false;
        this.telefonoErrorMessage = '';
      }
    } else {
      this.telefonoError = false;
      this.telefonoErrorMessage = '';
    }
  }

  validarEdad(): void {
    if (this.edad !== null) {
      if (isNaN(this.edad) || this.edad < 0) {
        this.edadError = true;
        this.edadErrorMessage = 'Ingrese una edad válida.';
      } else {
        this.edadError = false;
        this.edadErrorMessage = '';
      }
    } else {
      this.edadError = false;
      this.edadErrorMessage = '';
    }
  }

  validarCedula(): void {
    const pattern = /^\d+$/;
    if (this.cedula !== '') {
      if (!pattern.test(this.cedula) || this.cedula.length > 20) {
        this.cedulaError = true;
        this.cedulaErrorMessage = 'Ingrese una cédula válida (solo números y máximo 20 caracteres).';
      } else {
        this.cedulaError = false;
        this.cedulaErrorMessage = '';
      }
    } else {
      this.cedulaError = false;
      this.cedulaErrorMessage = '';
    }
  }
}
