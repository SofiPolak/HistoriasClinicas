﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using tp_nt1.Database;

namespace tp_nt1.Migrations
{
    [DbContext(typeof(HistoriaClinicaDbContext))]
    partial class HistoriaClinicaDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.14");

            modelBuilder.Entity("tp_nt1.Models.Diagnostico", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasMaxLength(250);

                    b.Property<Guid>("EpicrisisId")
                        .HasColumnType("TEXT");

                    b.Property<string>("Recomendacion")
                        .HasColumnType("TEXT")
                        .HasMaxLength(250);

                    b.HasKey("Id");

                    b.HasIndex("EpicrisisId")
                        .IsUnique();

                    b.ToTable("Diagnosticos");
                });

            modelBuilder.Entity("tp_nt1.Models.Empleado", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Apellido")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasMaxLength(100);

                    b.Property<string>("DNI")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasMaxLength(8);

                    b.Property<string>("Direccion")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasMaxLength(100);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasMaxLength(100);

                    b.Property<DateTime>("FechaAlta")
                        .HasColumnType("TEXT");

                    b.Property<string>("Legajo")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasMaxLength(8);

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasMaxLength(100);

                    b.Property<string>("NombreUsuario")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasMaxLength(100);

                    b.Property<byte[]>("Password")
                        .HasColumnType("BLOB");

                    b.Property<string>("Telefono")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasMaxLength(30);

                    b.HasKey("Id");

                    b.ToTable("Empleados");
                });

            modelBuilder.Entity("tp_nt1.Models.Epicrisis", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("EpisodioId")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("FechaYHora")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("ProfesionalId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("EpisodioId")
                        .IsUnique();

                    b.HasIndex("ProfesionalId");

                    b.ToTable("Epicrisis");
                });

            modelBuilder.Entity("tp_nt1.Models.Episodio", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasMaxLength(250);

                    b.Property<Guid>("EmpleadoId")
                        .HasColumnType("TEXT");

                    b.Property<bool>("EstadoAbierto")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime?>("FechaYHoraAlta")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("FechaYHoraCierre")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("FechaYHoraInicio")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("HistoriaId")
                        .HasColumnType("TEXT");

                    b.Property<string>("Motivo")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasMaxLength(100);

                    b.HasKey("Id");

                    b.HasIndex("EmpleadoId");

                    b.HasIndex("HistoriaId");

                    b.ToTable("Episodios");
                });

            modelBuilder.Entity("tp_nt1.Models.Especialidad", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<bool>("Activo")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.ToTable("Especialidades");
                });

            modelBuilder.Entity("tp_nt1.Models.Evolucion", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("DescripcionAtencion")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasMaxLength(250);

                    b.Property<Guid>("EpisodioId")
                        .HasColumnType("TEXT");

                    b.Property<bool>("EstadoAbierto")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime?>("FechaYHoraAlta")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("FechaYHoraCierre")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("FechaYHoraInicio")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("ProfesionalId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("EpisodioId");

                    b.HasIndex("ProfesionalId");

                    b.ToTable("Evoluciones");
                });

            modelBuilder.Entity("tp_nt1.Models.HistoriaClinica", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("PacienteId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("PacienteId")
                        .IsUnique();

                    b.ToTable("HistoriasClinicas");
                });

            modelBuilder.Entity("tp_nt1.Models.Nota", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("EmpleadoId")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("EvolucionId")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("FechaYHora")
                        .HasColumnType("TEXT");

                    b.Property<string>("Mensaje")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasMaxLength(250);

                    b.Property<Guid?>("ProfesionalId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("EmpleadoId");

                    b.HasIndex("EvolucionId");

                    b.HasIndex("ProfesionalId");

                    b.ToTable("Notas");
                });

            modelBuilder.Entity("tp_nt1.Models.ObraSocial", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<bool>("Activo")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.ToTable("ObrasSociales");
                });

            modelBuilder.Entity("tp_nt1.Models.Paciente", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Apellido")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasMaxLength(100);

                    b.Property<string>("DNI")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasMaxLength(8);

                    b.Property<string>("Direccion")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasMaxLength(100);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasMaxLength(100);

                    b.Property<DateTime>("FechaAlta")
                        .HasColumnType("TEXT");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasMaxLength(100);

                    b.Property<string>("NombreUsuario")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasMaxLength(100);

                    b.Property<Guid>("ObraSocialId")
                        .HasColumnType("TEXT");

                    b.Property<byte[]>("Password")
                        .HasColumnType("BLOB");

                    b.Property<string>("Telefono")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasMaxLength(30);

                    b.HasKey("Id");

                    b.HasIndex("ObraSocialId");

                    b.ToTable("Pacientes");
                });

            modelBuilder.Entity("tp_nt1.Models.Profesional", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Apellido")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasMaxLength(100);

                    b.Property<string>("DNI")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasMaxLength(8);

                    b.Property<string>("Direccion")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasMaxLength(100);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasMaxLength(100);

                    b.Property<Guid>("EspecialidadId")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("FechaAlta")
                        .HasColumnType("TEXT");

                    b.Property<string>("Matricula")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasMaxLength(8);

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasMaxLength(100);

                    b.Property<string>("NombreUsuario")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasMaxLength(100);

                    b.Property<byte[]>("Password")
                        .HasColumnType("BLOB");

                    b.Property<string>("Telefono")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasMaxLength(30);

                    b.HasKey("Id");

                    b.HasIndex("EspecialidadId");

                    b.ToTable("Profesionales");
                });

            modelBuilder.Entity("tp_nt1.Models.Diagnostico", b =>
                {
                    b.HasOne("tp_nt1.Models.Epicrisis", "Epicrisis")
                        .WithOne("Diagnostico")
                        .HasForeignKey("tp_nt1.Models.Diagnostico", "EpicrisisId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("tp_nt1.Models.Epicrisis", b =>
                {
                    b.HasOne("tp_nt1.Models.Episodio", "Episodio")
                        .WithOne("Epicrisis")
                        .HasForeignKey("tp_nt1.Models.Epicrisis", "EpisodioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("tp_nt1.Models.Profesional", "Profesional")
                        .WithMany("Epicrisis")
                        .HasForeignKey("ProfesionalId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("tp_nt1.Models.Episodio", b =>
                {
                    b.HasOne("tp_nt1.Models.Empleado", "EmpleadoRegistra")
                        .WithMany("Episodios")
                        .HasForeignKey("EmpleadoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("tp_nt1.Models.HistoriaClinica", "HistoriaClinica")
                        .WithMany("Episodios")
                        .HasForeignKey("HistoriaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("tp_nt1.Models.Evolucion", b =>
                {
                    b.HasOne("tp_nt1.Models.Episodio", "Episodio")
                        .WithMany("RegistroEvoluciones")
                        .HasForeignKey("EpisodioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("tp_nt1.Models.Profesional", "Profesional")
                        .WithMany("Evoluciones")
                        .HasForeignKey("ProfesionalId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("tp_nt1.Models.HistoriaClinica", b =>
                {
                    b.HasOne("tp_nt1.Models.Paciente", "Paciente")
                        .WithOne("HistoriaClinica")
                        .HasForeignKey("tp_nt1.Models.HistoriaClinica", "PacienteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("tp_nt1.Models.Nota", b =>
                {
                    b.HasOne("tp_nt1.Models.Empleado", "Empleado")
                        .WithMany("Notas")
                        .HasForeignKey("EmpleadoId");

                    b.HasOne("tp_nt1.Models.Evolucion", "Evolucion")
                        .WithMany("Notas")
                        .HasForeignKey("EvolucionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("tp_nt1.Models.Profesional", "Profesional")
                        .WithMany("Notas")
                        .HasForeignKey("ProfesionalId");
                });

            modelBuilder.Entity("tp_nt1.Models.Paciente", b =>
                {
                    b.HasOne("tp_nt1.Models.ObraSocial", "ObraSocial")
                        .WithMany()
                        .HasForeignKey("ObraSocialId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("tp_nt1.Models.Profesional", b =>
                {
                    b.HasOne("tp_nt1.Models.Especialidad", "Especialidad")
                        .WithMany()
                        .HasForeignKey("EspecialidadId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
