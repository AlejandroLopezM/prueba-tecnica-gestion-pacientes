import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { HttpErrorResponse } from '@angular/common/http';
import {
  ActualizarPacienteRequest,
  CrearPacienteRequest,
  DetallePacienteResponse
} from 'src/app/core/models/paciente.model';
import { PacientesService } from 'src/app/core/services/pacientes.service';

@Component({
  selector: 'app-formulario-paciente',
  templateUrl: './formulario-paciente.component.html',
  styleUrls: ['./formulario-paciente.component.scss']
})
export class FormularioPacienteComponent implements OnInit {

  formulario!: FormGroup;
  pacienteId = 0;
  esEdicion = false;
  cargando = false;
  guardando = false;
  error = '';

  constructor(
    private fb: FormBuilder,
    private route: ActivatedRoute,
    private router: Router,
    private pacientesService: PacientesService
  ) { }

  ngOnInit(): void {
    this.inicializarFormulario();

    const id = this.route.snapshot.paramMap.get('id');

    if (id) {
      this.esEdicion = true;
      this.pacienteId = Number(id);
      this.cargarPaciente();
    }
  }

  inicializarFormulario(): void {
    this.formulario = this.fb.group({
      tipoDocumento: ['', [Validators.required, Validators.maxLength(20)]],
      numeroDocumento: ['', [Validators.required, Validators.maxLength(20)]],
      nombres: ['', [Validators.required, Validators.maxLength(100)]],
      apellidos: ['', [Validators.required, Validators.maxLength(100)]],
      fechaNacimiento: ['', [Validators.required]],
      telefono: ['', [Validators.maxLength(20)]],
      correoElectronico: ['', [Validators.email, Validators.maxLength(150)]]
    });
  }

  cargarPaciente(): void {
    this.cargando = true;
    this.error = '';

    this.pacientesService.obtenerPorId(this.pacienteId).subscribe({
      next: (paciente: DetallePacienteResponse) => {
        this.formulario.patchValue({
          tipoDocumento: paciente.tipoDocumento,
          numeroDocumento: paciente.numeroDocumento,
          nombres: paciente.nombres,
          apellidos: paciente.apellidos,
          fechaNacimiento: paciente.fechaNacimiento?.substring(0, 10),
          telefono: paciente.telefono,
          correoElectronico: paciente.correoElectronico
        });

        this.cargando = false;
      },
      error: () => {
        this.error = 'No fue posible cargar el paciente para edición.';
        this.cargando = false;
      }
    });
  }

  guardar(): void {
    this.error = '';

    if (this.formulario.invalid) {
      this.formulario.markAllAsTouched();
      return;
    }

    this.guardando = true;

    const request: CrearPacienteRequest | ActualizarPacienteRequest = {
      tipoDocumento: this.formulario.value.tipoDocumento,
      numeroDocumento: this.formulario.value.numeroDocumento,
      nombres: this.formulario.value.nombres,
      apellidos: this.formulario.value.apellidos,
      fechaNacimiento: this.formulario.value.fechaNacimiento,
      telefono: this.formulario.value.telefono || null,
      correoElectronico: this.formulario.value.correoElectronico || null
    };

    if (this.esEdicion) {
      this.pacientesService.actualizar(this.pacienteId, request).subscribe({
        next: (respuesta) => {
          this.guardando = false;
          this.router.navigate(['/pacientes', respuesta.pacienteId]);
        },
        error: (error: HttpErrorResponse) => {
          this.guardando = false;
          this.error = this.obtenerMensajeError(error, 'No fue posible actualizar el paciente.');
        }
      });
    } else {
      this.pacientesService.crear(request).subscribe({
        next: (respuesta) => {
          this.guardando = false;
          this.router.navigate(['/pacientes', respuesta.pacienteId]);
        },
        error: (error: HttpErrorResponse) => {
          this.guardando = false;
          this.error = this.obtenerMensajeError(error, 'No fue posible crear el paciente.');
        }
      });
    }
  }

  volver(): void {
    if (this.esEdicion) {
      this.router.navigate(['/pacientes', this.pacienteId]);
      return;
    }

    this.router.navigate(['/pacientes']);
  }

  campoInvalido(nombreCampo: string): boolean {
    const campo = this.formulario.get(nombreCampo);
    return !!campo && campo.invalid && campo.touched;
  }

  private obtenerMensajeError(error: HttpErrorResponse, mensajePorDefecto: string): string {
    const respuesta = error.error;

    if (respuesta?.mensaje) {
      return respuesta.mensaje;
    }

    if (respuesta?.detalles?.length) {
      return respuesta.detalles[0];
    }

    return mensajePorDefecto;
  }
}