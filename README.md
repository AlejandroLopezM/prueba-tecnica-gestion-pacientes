# Prueba técnica - Gestión de Pacientes

## Autor
Alejandro López Castañeda

## Repositorio
Repositorio del proyecto:
https://github.com/AlejandroLopezM/prueba-tecnica-gestion-pacientes

## Descripción general
Esta solución corresponde a una prueba técnica full stack y de base de datos.

Se desarrolló una aplicación de gestión de pacientes compuesta por:

- Backend en .NET
- Frontend en Angular 16 + PrimeNG
- Base de datos SQL Server
- Pruebas unitarias en backend y frontend

La solución permite:

- listar pacientes con paginación y filtros
- crear pacientes
- editar pacientes
- eliminar pacientes
- visualizar el detalle del paciente
- visualizar citas asociadas en modo solo lectura
- consultar pacientes creados después de una fecha mediante stored procedure

---

## Tecnologías utilizadas

### Backend
- .NET
- ASP.NET Core Web API
- Entity Framework Core
- SQL Server

### Frontend
- Angular 16
- PrimeNG
- PrimeIcons
- PrimeFlex
- Reactive Forms
- Jasmine + Karma

### Base de datos
- SQL Server
- Stored Procedure

---

## Estructura de la solución

### Backend
- `GestionPacientes.Api`
- `GestionPacientes.Aplicacion`
- `GestionPacientes.Dominio`
- `GestionPacientes.Infraestructura`
- `GestionPacientes.Pruebas`

### Frontend
- `gestion-pacientes-front`

---

## Requisitos previos

Antes de ejecutar el proyecto se requiere tener instalado:

- Visual Studio 2022 o superior
- SQL Server
- Node.js v18.x
- Angular CLI 16
- Google Chrome

---

## Configuración de la base de datos

### Cadena de conexión
En el archivo:

`GestionPacientes.Api/appsettings.json`

configurar la cadena de conexión, por ejemplo:

```json
"ConnectionStrings": {
  "ConexionSqlServer": "Server=ALEJANDRO_LOPEZ;Database=GestionPacientesDb;Trusted_Connection=True;TrustServerCertificate=True;"
}