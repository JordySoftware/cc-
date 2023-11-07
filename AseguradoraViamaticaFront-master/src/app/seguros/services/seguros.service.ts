import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Seguro } from '../interfaces/seguro.interface';
import { Asegurado } from 'src/app/asegurados/interfaces/asegurado.interface';
import { SeguroCreacion } from '../interfaces/seguro-creacion.interface';

@Injectable({
  providedIn: 'root'
})
export class SegurosService {

  constructor(private http: HttpClient) { }

  getSeguros(): Observable<Seguro[]> {
    return this.http.get<Seguro[]>('https://localhost:7279/api/seguros');
  }
  getAseguradosPorCodigo(codigo: number): Observable<Asegurado[]>{
    return this.http.get<Asegurado[]>(`https://localhost:7279/api/seguros/asegurados/${codigo}`);
  }
  getSeguroPorCodigo(codigo: number):Observable<Seguro[]>{
    return this.http.get<Seguro[]>(`https://localhost:7279/api/seguros/codigo/${codigo}`);
  }
  getSeguroPorId(id: number):Observable<Seguro>{
    return this.http.get<Seguro>(`https://localhost:7279/api/seguros/${id}`);
  }

  agregarSeguro(seguro:SeguroCreacion):Observable<SeguroCreacion>{
    return this.http.post<SeguroCreacion>('https://localhost:7279/api/seguros', seguro);
  }

  editarSeguro(seguro:SeguroCreacion, id: number):Observable<SeguroCreacion>{
    return this.http.put<SeguroCreacion>(`https://localhost:7279/api/seguros/${id}`, seguro);
  }

  eliminarSeguro(id: number):Observable<any>{
    return this.http.delete<any>(`https://localhost:7279/api/seguros/${id}`);
  }

}
