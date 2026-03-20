import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { DetallePacienteResponse } from 'src/app/core/models/paciente.model';
import { PacientesService } from 'src/app/core/services/pacientes.service';

@Component({
  selector: 'app-detalle-paciente',
  templateUrl: './detalle-paciente.component.html',
  styleUrls: ['./detalle-paciente.component.scss']
})
export class DetallePacienteComponent implements OnInit {

  paciente: DetallePacienteResponse | null = null;
  cargando = false;
  error = '';
  pacienteId = 0;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private pacientesService: PacientesService
  ) { }

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('id');

    if (!id) {
      this.error = 'No se recibió el id del paciente.';
      return;
    }

    this.pacienteId = Number(id);
    this.cargarPaciente();
  }

  cargarPaciente(): void {
    this.cargando = true;
    this.error = '';

    this.pacientesService.obtenerPorId(this.pacienteId).subscribe({
      next: (respuesta) => {
        this.paciente = respuesta;
        this.cargando = false;
      },
      error: () => {
        this.error = 'No fue posible cargar el detalle del paciente.';
        this.cargando = false;
      }
    });
  }

  volver(): void {
    this.router.navigate(['/pacientes']);
  }

  editar(): void {
    this.router.navigate(['/pacientes/editar', this.pacienteId]);
  }
}
