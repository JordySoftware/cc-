
import { Component, Inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { Seguro } from '../../interfaces/seguro.interface';

@Component({
  selector: 'app-confirmar-eliminacion',
  templateUrl: './confirmar-eliminacion.component.html',
  styleUrls: ['./confirmar-eliminacion.component.css']
})
export class ConfirmarEliminacionComponent {

  constructor(private dialogRef: MatDialogRef<ConfirmarEliminacionComponent>,
    @Inject(MAT_DIALOG_DATA) public data:Seguro){}

  cerrar() {
    this.dialogRef.close();
  }
  borrar() {
    this.dialogRef.close(true);
  }

}
