import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { SegurosService } from '../../services/seguros.service';
import { Seguro } from '../../interfaces/seguro.interface';
import { SeguroCreacion } from '../../interfaces/seguro-creacion.interface';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-agregar-seguro',
  templateUrl: './agregar-seguro.component.html',
  styleUrls: ['./agregar-seguro.component.css']
})
export class AgregarSeguroComponent implements OnInit {

  seguroForm: FormGroup;
  nombreControl: FormControl;
  codigoControl: FormControl;
  sumaControl: FormControl;
  primaControl: FormControl;

  nombre: string = '';
  codigo!: number;
  suma!: number;
  prima!: number;
  id!: number;

  constructor(private formBuilder: FormBuilder, private segurosService: SegurosService,
    private snackBar: MatSnackBar,
    private activatedRoute: ActivatedRoute,
    private router: Router
  ) {
    this.nombreControl = this.formBuilder.control('', [
      Validators.required,
      Validators.maxLength(50),
      this.noNumerosValidator.bind(this)
    ]);
    this.codigoControl = this.formBuilder.control('', Validators.required);
    this.sumaControl = this.formBuilder.control(null, Validators.required);
    this.primaControl = this.formBuilder.control(null, Validators.required);

    this.seguroForm = this.formBuilder.group({
      nombre: this.nombreControl,
      codigo: this.codigoControl,
      suma: this.sumaControl,
      prima: this.primaControl
    });
  }


  ngOnInit(): void {
    if (this.router.url.includes('editar')) {
      this.activatedRoute.params.subscribe(({ id }) => {
        this.segurosService.getSeguroPorId(id).subscribe((seguro: Seguro) => {
          this.id = id;
          this.seguroForm.patchValue({
            nombre: seguro.nombreSeguro,
            codigo: seguro.codigoSeguro,
            suma: seguro.sumadaAsegurada,
            prima: seguro.prima,
            
          });
        });
      });
    }
  }


  agregarSeguro() {
    if (this.seguroForm.valid) {
      this.nombre = this.nombreControl.value;
      this.codigo = this.codigoControl.value;
      this.suma = this.sumaControl.value;
      this.prima = this.primaControl.value;

      const seguroNuevo: SeguroCreacion = {
        nombreSeguro: this.nombre,
        codigoSeguro: this.codigo,
        sumadaAsegurada: this.suma,
        prima: this.prima,
      };

      console.log(seguroNuevo);

      if (this.router.url.includes('editar')) {
        // Llamar al servicio editarSeguro
        this.segurosService.editarSeguro(seguroNuevo, this.id).subscribe((seguro) => {
          console.log(seguro);
          this.seguroForm.reset(); // Limpia el formulario
          this.router.navigate(['/seguros/listado']);
          this.mostrarSnackbar('Registro editado');
        });
      } else {
        // Llamar al servicio agregarSeguro
        this.segurosService.agregarSeguro(seguroNuevo).subscribe((seguro) => {
          console.log(seguro);
          this.seguroForm.reset(); // Limpia el formulario
          this.mostrarSnackbar('Registro creado');
        });
      }
    } else {
      this.seguroForm.markAllAsTouched();
    }
  }

  mostrarSnackbar(mensaje: string): void {
    this.snackBar.open(mensaje, 'Ok!', {
      duration: 2500
    });
  }




  noNumerosValidator(control: FormControl): { [key: string]: any } | null {
    const value = control.value;
    const hasNumbers = /\d/.test(value);
    return hasNumbers ? { noNumeros: true } : null;
  }
}
