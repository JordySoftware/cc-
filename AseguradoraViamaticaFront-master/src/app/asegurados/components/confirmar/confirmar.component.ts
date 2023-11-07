import { Component, Inject, inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { Asegurado } from '../../interfaces/asegurado.interface';

@Component({
  selector: 'app-confirmar',
  templateUrl: './confirmar.component.html',
  styleUrls: ['./confirmar.component.css']
})
export class ConfirmarComponent {


  constructor(private dialogRef: MatDialogRef<ConfirmarComponent>,@Inject(MAT_DIALOG_DATA) public data: Asegurado) { }

  cerrar() {
    this.dialogRef.close();
  }
  borrar() {
    this.dialogRef.close(true);
  }


}
