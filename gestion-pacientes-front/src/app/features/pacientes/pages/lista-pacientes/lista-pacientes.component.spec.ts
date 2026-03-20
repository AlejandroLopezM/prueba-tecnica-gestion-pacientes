import { ComponentFixture, TestBed } from '@angular/core/testing';
import { of } from 'rxjs';
import { RouterTestingModule } from '@angular/router/testing';
import { NO_ERRORS_SCHEMA } from '@angular/core';

import { ListaPacientesComponent } from './lista-pacientes.component';
import { PacientesService } from 'src/app/core/services/pacientes.service';

describe('ListaPacientesComponent', () => {
  let component: ListaPacientesComponent;
  let fixture: ComponentFixture<ListaPacientesComponent>;
  let pacientesServiceSpy: jasmine.SpyObj<PacientesService>;

  beforeEach(async () => {
    const spy = jasmine.createSpyObj('PacientesService', ['obtenerPaginado', 'eliminar']);

    await TestBed.configureTestingModule({
      declarations: [ListaPacientesComponent],
      imports: [RouterTestingModule],
      providers: [
        { provide: PacientesService, useValue: spy }
      ],
      schemas: [NO_ERRORS_SCHEMA]
    }).compileComponents();

    fixture = TestBed.createComponent(ListaPacientesComponent);
    component = fixture.componentInstance;
    pacientesServiceSpy = TestBed.inject(PacientesService) as jasmine.SpyObj<PacientesService>;
  });

  it('debe cargar pacientes al iniciar', () => {
    pacientesServiceSpy.obtenerPaginado.and.returnValue(of({
      items: [
        {
          pacienteId: 1,
          tipoDocumento: 'CC',
          numeroDocumento: '123',
          nombres: 'Alejandro',
          apellidos: 'Lopez',
          telefono: '3001234567',
          correoElectronico: 'alejandro@email.com',
          fechaCreacion: '2026-03-20T00:00:00'
        }
      ],
      totalRegistros: 1,
      paginaActual: 1,
      tamanoPagina: 10,
      totalPaginas: 1
    }));

    component.ngOnInit();

    expect(pacientesServiceSpy.obtenerPaginado).toHaveBeenCalled();
    expect(component.pacientes.length).toBe(1);
    expect(component.totalRegistros).toBe(1);
  });
});