import { ComponentFixture, TestBed } from '@angular/core/testing';
import { ReactiveFormsModule } from '@angular/forms';
import { NO_ERRORS_SCHEMA } from '@angular/core';
import { RouterTestingModule } from '@angular/router/testing';
import { ActivatedRoute } from '@angular/router';
import { of } from 'rxjs';

import { FormularioPacienteComponent } from './formulario-paciente.component';
import { PacientesService } from 'src/app/core/services/pacientes.service';

describe('FormularioPacienteComponent', () => {
  let component: FormularioPacienteComponent;
  let fixture: ComponentFixture<FormularioPacienteComponent>;
  let pacientesServiceSpy: jasmine.SpyObj<PacientesService>;

  beforeEach(async () => {
    const spy = jasmine.createSpyObj('PacientesService', ['crear', 'actualizar', 'obtenerPorId']);

    await TestBed.configureTestingModule({
      declarations: [FormularioPacienteComponent],
      imports: [ReactiveFormsModule, RouterTestingModule],
      providers: [
        { provide: PacientesService, useValue: spy },
        {
          provide: ActivatedRoute,
          useValue: {
            snapshot: {
              paramMap: {
                get: () => null
              }
            }
          }
        }
      ],
      schemas: [NO_ERRORS_SCHEMA]
    }).compileComponents();

    fixture = TestBed.createComponent(FormularioPacienteComponent);
    component = fixture.componentInstance;
    pacientesServiceSpy = TestBed.inject(PacientesService) as jasmine.SpyObj<PacientesService>;
    fixture.detectChanges();
  });

  it('no debe guardar si el formulario es inválido', () => {
    spyOn(component.formulario, 'markAllAsTouched');

    component.guardar();

    expect(component.formulario.markAllAsTouched).toHaveBeenCalled();
    expect(pacientesServiceSpy.crear).not.toHaveBeenCalled();
    expect(pacientesServiceSpy.actualizar).not.toHaveBeenCalled();
  });
});