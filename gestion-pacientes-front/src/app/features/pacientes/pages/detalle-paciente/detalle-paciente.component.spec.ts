import { ComponentFixture, TestBed } from '@angular/core/testing';
import { of } from 'rxjs';
import { NO_ERRORS_SCHEMA } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { RouterTestingModule } from '@angular/router/testing';

import { DetallePacienteComponent } from './detalle-paciente.component';
import { PacientesService } from 'src/app/core/services/pacientes.service';

describe('DetallePacienteComponent', () => {
  let component: DetallePacienteComponent;
  let fixture: ComponentFixture<DetallePacienteComponent>;
  let pacientesServiceSpy: jasmine.SpyObj<PacientesService>;

  beforeEach(async () => {
    const spy = jasmine.createSpyObj('PacientesService', ['obtenerPorId']);

    await TestBed.configureTestingModule({
      declarations: [DetallePacienteComponent],
      imports: [RouterTestingModule],
      providers: [
        { provide: PacientesService, useValue: spy },
        {
          provide: ActivatedRoute,
          useValue: {
            snapshot: {
              paramMap: {
                get: () => '1'
              }
            }
          }
        }
      ],
      schemas: [NO_ERRORS_SCHEMA]
    }).compileComponents();

    fixture = TestBed.createComponent(DetallePacienteComponent);
    component = fixture.componentInstance;
    pacientesServiceSpy = TestBed.inject(PacientesService) as jasmine.SpyObj<PacientesService>;
  });

  it('debe cargar el paciente al iniciar', () => {
    pacientesServiceSpy.obtenerPorId.and.returnValue(of({
      pacienteId: 1,
      tipoDocumento: 'CC',
      numeroDocumento: '123456789',
      nombres: 'Alejandro',
      apellidos: 'Lopez',
      fechaNacimiento: '1995-05-10',
      telefono: '3001234567',
      correoElectronico: 'alejandro@email.com',
      fechaCreacion: '2026-03-20T00:00:00',
      citas: []
    }));

    component.ngOnInit();

    expect(pacientesServiceSpy.obtenerPorId).toHaveBeenCalledWith(1);
    expect(component.paciente).toBeTruthy();
    expect(component.paciente?.nombres).toBe('Alejandro');
  });
});