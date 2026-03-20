import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { PacientesRoutingModule } from './pacientes-routing.module';
import { ListaPacientesComponent } from './pages/lista-pacientes/lista-pacientes.component';
import { FormularioPacienteComponent } from './pages/formulario-paciente/formulario-paciente.component';
import { DetallePacienteComponent } from './pages/detalle-paciente/detalle-paciente.component';

import { TableModule } from 'primeng/table';
import { ButtonModule } from 'primeng/button';
import { InputTextModule } from 'primeng/inputtext';
import { CardModule } from 'primeng/card';
import { TagModule } from 'primeng/tag';
import { ProgressSpinnerModule } from 'primeng/progressspinner';

@NgModule({
  declarations: [
    ListaPacientesComponent,
    FormularioPacienteComponent,
    DetallePacienteComponent
  ],
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    PacientesRoutingModule,
    TableModule,
    ButtonModule,
    InputTextModule,
    CardModule,
    TagModule,
    ProgressSpinnerModule
  ]
})
export class PacientesModule { }