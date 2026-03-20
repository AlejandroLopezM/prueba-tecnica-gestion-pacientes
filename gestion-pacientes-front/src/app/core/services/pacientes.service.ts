import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import {
  ActualizarPacienteRequest,
  CrearPacienteRequest,
  DetallePacienteResponse,
  FiltroPacientesRequest,
  PacienteResponse,
  ResultadoPaginado
} from '../models/paciente.model';

@Injectable({
  providedIn: 'root'
})
export class PacientesService {

  private readonly baseUrl = `${environment.apiUrl}/patients`;

  constructor(private http: HttpClient) { }

  obtenerPaginado(filtro: FiltroPacientesRequest): Observable<ResultadoPaginado<PacienteResponse>> {
    let params = new HttpParams()
      .set('pagina', filtro.pagina)
      .set('tamanoPagina', filtro.tamanoPagina);

    if (filtro.nombre) {
      params = params.set('nombre', filtro.nombre);
    }

    if (filtro.numeroDocumento) {
      params = params.set('numeroDocumento', filtro.numeroDocumento);
    }

    return this.http.get<ResultadoPaginado<PacienteResponse>>(this.baseUrl, { params });
  }

  obtenerPorId(id: number): Observable<DetallePacienteResponse> {
    return this.http.get<DetallePacienteResponse>(`${this.baseUrl}/${id}`);
  }

  crear(request: CrearPacienteRequest): Observable<PacienteResponse> {
    return this.http.post<PacienteResponse>(this.baseUrl, request);
  }

  actualizar(id: number, request: ActualizarPacienteRequest): Observable<DetallePacienteResponse> {
    return this.http.put<DetallePacienteResponse>(`${this.baseUrl}/${id}`, request);
  }

  eliminar(id: number): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/${id}`);
  }

  obtenerCreadosDespuesDe(fechaDesde: string): Observable<PacienteResponse[]> {
    const params = new HttpParams().set('fechaDesde', fechaDesde);
    return this.http.get<PacienteResponse[]>(`${this.baseUrl}/created-after`, { params });
  }
}