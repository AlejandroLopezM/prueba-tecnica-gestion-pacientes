import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { PacientesService } from 'src/app/core/services/pacientes.service';
import { FiltroPacientesRequest, PacienteResponse } from 'src/app/core/models/paciente.model';

@Component({
  selector: 'app-lista-pacientes',
  templateUrl: './lista-pacientes.component.html',
  styleUrls: ['./lista-pacientes.component.scss']
})
export class ListaPacientesComponent implements OnInit {

  pacientes: PacienteResponse[] = [];
  cargando = false;
  error = '';

  filtro: FiltroPacientesRequest = {
    nombre: '',
    numeroDocumento: '',
    pagina: 1,
    tamanoPagina: 10
  };

  totalRegistros = 0;

  constructor(
    private pacientesService: PacientesService,
    private router: Router
  ) { }

  ngOnInit(): void {
    this.cargarPacientes();
  }

  cargarPacientes(): void {
    this.cargando = true;
    this.error = '';

    this.pacientesService.obtenerPaginado(this.filtro).subscribe({
      next: (respuesta) => {
        this.pacientes = respuesta.items;
        this.totalRegistros = respuesta.totalRegistros;
        this.cargando = false;
      },
      error: () => {
        this.error = 'No fue posible cargar los pacientes.';
        this.cargando = false;
      }
    });
  }

  buscar(): void {
    this.filtro.pagina = 1;
    this.cargarPacientes();
  }

  limpiarFiltros(): void {
    this.filtro = {
      nombre: '',
      numeroDocumento: '',
      pagina: 1,
      tamanoPagina: 10
    };
    this.cargarPacientes();
  }

  cambiarPagina(event: any): void {
    this.filtro.pagina = (event.first / event.rows) + 1;
    this.filtro.tamanoPagina = event.rows;
    this.cargarPacientes();
  }

  verDetalle(id: number): void {
    this.router.navigate(['/pacientes', id]);
  }

  editar(id: number): void {
    this.router.navigate(['/pacientes/editar', id]);
  }

  nuevo(): void {
    this.router.navigate(['/pacientes/nuevo']);
  }

  eliminar(id: number): void {
    const confirmado = confirm('¿Está seguro de eliminar este paciente?');

    if (!confirmado) {
      return;
    }

    this.pacientesService.eliminar(id).subscribe({
      next: () => {
        this.cargarPacientes();
      },
      error: () => {
        this.error = 'No fue posible eliminar el paciente.';
      }
    });
  }
}