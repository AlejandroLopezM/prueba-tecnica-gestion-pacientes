import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ListaPacientesComponent } from './pages/lista-pacientes/lista-pacientes.component';
import { FormularioPacienteComponent } from './pages/formulario-paciente/formulario-paciente.component';
import { DetallePacienteComponent } from './pages/detalle-paciente/detalle-paciente.component';

const routes: Routes = [
  {
    path: '',
    component: ListaPacientesComponent
  },
  {
    path: 'nuevo',
    component: FormularioPacienteComponent
  },
  {
    path: 'editar/:id',
    component: FormularioPacienteComponent
  },
  {
    path: ':id',
    component: DetallePacienteComponent
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class PacientesRoutingModule { }