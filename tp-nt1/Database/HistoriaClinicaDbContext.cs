using System;
using Microsoft.EntityFrameworkCore;
using tp_nt1.Models;

namespace tp_nt1.Database
{
    public class HistoriaClinicaDbContext: DbContext
    {
        public HistoriaClinicaDbContext(DbContextOptions<HistoriaClinicaDbContext> opciones) : base(opciones)
        {
        }

        public DbSet<Diagnostico> Diagnosticos { get; set; }
        public DbSet<Empleado> Empleados { get; set; }
        public DbSet<Epicrisis> Epicrisis { get; set; }
        public DbSet<Episodio> Episodios { get; set; }
        public DbSet<Evolucion> Evoluciones { get; set; }
        public DbSet<HistoriaClinica> HistoriasClinicas { get; set; }
        public DbSet<Nota> Notas { get; set; }
        public DbSet<Paciente> Pacientes { get; set; }
        public DbSet<Profesional> Profesionales { get; set; }

        public DbSet<ObraSocial> ObrasSociales { get; set; }
        public DbSet<Especialidad> Especialidades { get; set; }

    }
}
