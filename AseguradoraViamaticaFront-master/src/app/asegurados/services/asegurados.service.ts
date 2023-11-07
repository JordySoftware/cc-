import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Asegurado } from '../interfaces/asegurado.interface';
import { Observable } from 'rxjs';
import { Seguro } from 'src/app/seguros/interfaces/seguro.interface';
import { AseguradoCreacion } from '../interfaces/aseguradoCreacion.interface';

@Injectable({
  providedIn: 'root'
})
export class AseguradosService {

  constructor(private http: HttpClient) { }
  getAseguradoPorId(id: number):Observable<Asegurado>{
    return this.http.get<Asegurado>(`https://localhost:7279/api/asegurados/${id}`);
  }
  editarAsegurado(id: number, asegurado:AseguradoCreacion):Observable<AseguradoCreacion>{
    return this.http.put<AseguradoCreacion>(`https://localhost:7279/api/asegurados/${id}`, asegurado);
  }
  getAsegurados(): Observable<Asegurado[]> {
    return this.http.get<Asegurado[]>('https://localhost:7279/api/asegurados');
  }
  getAseguradoPorCedula(cedula: string):Observable<Asegurado>{
    return this.http.get<Asegurado>(`https://localhost:7279/api/asegurados/cedula2/${cedula}`);
  }
  
  getSegurosCedula(cedula: string): Observable<Seguro[]>{
    return this.http.get<Seguro[]>(`https://localhost:7279/api/asegurados/cedula/${cedula}`);
  }

  agregarAsegurado(asegurado: AseguradoCreacion):Observable<AseguradoCreacion>{
    return this.http.post<AseguradoCreacion>('https://localhost:7279/api/asegurados', asegurado);

  }
  eliminarAsegurado(id: number): Observable<any>{
    return this.http.delete<any>(`https://localhost:7279/api/asegurados/${id}`);

  }
}
