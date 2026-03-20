using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionPacientes.Dominio.Entidades
{
    public class Cita
    {
        public int CitaId { get; set; }
        public int PacienteId { get; set; }
        public DateTime FechaCita { get; set; }
        public string? Observacion { get; set; }

        public Paciente Paciente { get; set; } = null!;
    }
}
