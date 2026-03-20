export interface PacienteResponse {
  pacienteId: number;
  tipoDocumento: string;
  numeroDocumento: string;
  nombres: string;
  apellidos: string;
  telefono?: string | null;
  correoElectronico?: string | null;
  fechaCreacion: string;
}

export interface CitaResponse {
  citaId: number;
  fechaCita: string;
  observacion?: string | null;
}

export interface DetallePacienteResponse {
  pacienteId: number;
  tipoDocumento: string;
  numeroDocumento: string;
  nombres: string;
  apellidos: string;
  fechaNacimiento: string;
  telefono?: string | null;
  correoElectronico?: string | null;
  fechaCreacion: string;
  citas: CitaResponse[];
}

export interface CrearPacienteRequest {
  tipoDocumento: string;
  numeroDocumento: string;
  nombres: string;
  apellidos: string;
  fechaNacimiento: string;
  telefono?: string | null;
  correoElectronico?: string | null;
}

export interface ActualizarPacienteRequest {
  tipoDocumento: string;
  numeroDocumento: string;
  nombres: string;
  apellidos: string;
  fechaNacimiento: string;
  telefono?: string | null;
  correoElectronico?: string | null;
}

export interface FiltroPacientesRequest {
  nombre?: string;
  numeroDocumento?: string;
  pagina: number;
  tamanoPagina: number;
}

export interface ResultadoPaginado<T> {
  items: T[];
  totalRegistros: number;
  paginaActual: number;
  tamanoPagina: number;
  totalPaginas: number;
}

export interface RespuestaError {
  statusCode: number;
  mensaje: string;
  detalles: string[];
}