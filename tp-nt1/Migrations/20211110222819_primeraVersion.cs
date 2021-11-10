using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace tp_nt1.Migrations
{
    public partial class primeraVersion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Empleados",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    NombreUsuario = table.Column<string>(maxLength: 100, nullable: false),
                    Nombre = table.Column<string>(maxLength: 100, nullable: false),
                    Apellido = table.Column<string>(maxLength: 100, nullable: false),
                    Email = table.Column<string>(maxLength: 100, nullable: false),
                    FechaAlta = table.Column<DateTime>(nullable: false),
                    Password = table.Column<byte[]>(nullable: true),
                    DNI = table.Column<string>(maxLength: 8, nullable: false),
                    Telefono = table.Column<string>(maxLength: 30, nullable: false),
                    Direccion = table.Column<string>(maxLength: 100, nullable: false),
                    Legajo = table.Column<string>(maxLength: 8, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Empleados", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Especialidades",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Nombre = table.Column<string>(maxLength: 50, nullable: false),
                    Activo = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Especialidades", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ObrasSociales",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Nombre = table.Column<string>(maxLength: 50, nullable: false),
                    Activo = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ObrasSociales", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Profesionales",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    NombreUsuario = table.Column<string>(maxLength: 100, nullable: false),
                    Nombre = table.Column<string>(maxLength: 100, nullable: false),
                    Apellido = table.Column<string>(maxLength: 100, nullable: false),
                    Email = table.Column<string>(maxLength: 100, nullable: false),
                    FechaAlta = table.Column<DateTime>(nullable: false),
                    Password = table.Column<byte[]>(nullable: true),
                    DNI = table.Column<string>(maxLength: 8, nullable: false),
                    Telefono = table.Column<string>(maxLength: 30, nullable: false),
                    Direccion = table.Column<string>(maxLength: 100, nullable: false),
                    Matricula = table.Column<string>(maxLength: 8, nullable: false),
                    EspecialidadId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Profesionales", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Profesionales_Especialidades_EspecialidadId",
                        column: x => x.EspecialidadId,
                        principalTable: "Especialidades",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Pacientes",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    NombreUsuario = table.Column<string>(maxLength: 100, nullable: false),
                    Nombre = table.Column<string>(maxLength: 100, nullable: false),
                    Apellido = table.Column<string>(maxLength: 100, nullable: false),
                    Email = table.Column<string>(maxLength: 100, nullable: false),
                    FechaAlta = table.Column<DateTime>(nullable: false),
                    Password = table.Column<byte[]>(nullable: true),
                    DNI = table.Column<string>(maxLength: 8, nullable: false),
                    Telefono = table.Column<string>(maxLength: 30, nullable: false),
                    Direccion = table.Column<string>(maxLength: 100, nullable: false),
                    ObraSocialId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pacientes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pacientes_ObrasSociales_ObraSocialId",
                        column: x => x.ObraSocialId,
                        principalTable: "ObrasSociales",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HistoriasClinicas",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    PacienteId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistoriasClinicas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HistoriasClinicas_Pacientes_PacienteId",
                        column: x => x.PacienteId,
                        principalTable: "Pacientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Episodios",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Motivo = table.Column<string>(maxLength: 100, nullable: false),
                    Descripcion = table.Column<string>(maxLength: 250, nullable: false),
                    FechaYHoraInicio = table.Column<DateTime>(nullable: false),
                    FechaYHoraAlta = table.Column<DateTime>(nullable: true),
                    FechaYHoraCierre = table.Column<DateTime>(nullable: true),
                    EstadoAbierto = table.Column<bool>(nullable: false),
                    EmpleadoId = table.Column<Guid>(nullable: false),
                    HistoriaId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Episodios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Episodios_Empleados_EmpleadoId",
                        column: x => x.EmpleadoId,
                        principalTable: "Empleados",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Episodios_HistoriasClinicas_HistoriaId",
                        column: x => x.HistoriaId,
                        principalTable: "HistoriasClinicas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Epicrisis",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    FechaYHora = table.Column<DateTime>(nullable: false),
                    EpisodioId = table.Column<Guid>(nullable: false),
                    ProfesionalId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Epicrisis", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Epicrisis_Episodios_EpisodioId",
                        column: x => x.EpisodioId,
                        principalTable: "Episodios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Epicrisis_Profesionales_ProfesionalId",
                        column: x => x.ProfesionalId,
                        principalTable: "Profesionales",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Evoluciones",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    FechaYHoraInicio = table.Column<DateTime>(nullable: false),
                    FechaYHoraAlta = table.Column<DateTime>(nullable: true),
                    FechaYHoraCierre = table.Column<DateTime>(nullable: true),
                    DescripcionAtencion = table.Column<string>(maxLength: 250, nullable: false),
                    EstadoAbierto = table.Column<bool>(nullable: false),
                    EpisodioId = table.Column<Guid>(nullable: false),
                    ProfesionalId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Evoluciones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Evoluciones_Episodios_EpisodioId",
                        column: x => x.EpisodioId,
                        principalTable: "Episodios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Evoluciones_Profesionales_ProfesionalId",
                        column: x => x.ProfesionalId,
                        principalTable: "Profesionales",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Diagnosticos",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Descripcion = table.Column<string>(maxLength: 250, nullable: false),
                    Recomendacion = table.Column<string>(maxLength: 250, nullable: true),
                    EpicrisisId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Diagnosticos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Diagnosticos_Epicrisis_EpicrisisId",
                        column: x => x.EpicrisisId,
                        principalTable: "Epicrisis",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Notas",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Mensaje = table.Column<string>(maxLength: 250, nullable: false),
                    FechaYHora = table.Column<DateTime>(nullable: false),
                    EmpleadoId = table.Column<Guid>(nullable: true),
                    ProfesionalId = table.Column<Guid>(nullable: true),
                    EvolucionId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Notas_Empleados_EmpleadoId",
                        column: x => x.EmpleadoId,
                        principalTable: "Empleados",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Notas_Evoluciones_EvolucionId",
                        column: x => x.EvolucionId,
                        principalTable: "Evoluciones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Notas_Profesionales_ProfesionalId",
                        column: x => x.ProfesionalId,
                        principalTable: "Profesionales",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Diagnosticos_EpicrisisId",
                table: "Diagnosticos",
                column: "EpicrisisId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Epicrisis_EpisodioId",
                table: "Epicrisis",
                column: "EpisodioId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Epicrisis_ProfesionalId",
                table: "Epicrisis",
                column: "ProfesionalId");

            migrationBuilder.CreateIndex(
                name: "IX_Episodios_EmpleadoId",
                table: "Episodios",
                column: "EmpleadoId");

            migrationBuilder.CreateIndex(
                name: "IX_Episodios_HistoriaId",
                table: "Episodios",
                column: "HistoriaId");

            migrationBuilder.CreateIndex(
                name: "IX_Evoluciones_EpisodioId",
                table: "Evoluciones",
                column: "EpisodioId");

            migrationBuilder.CreateIndex(
                name: "IX_Evoluciones_ProfesionalId",
                table: "Evoluciones",
                column: "ProfesionalId");

            migrationBuilder.CreateIndex(
                name: "IX_HistoriasClinicas_PacienteId",
                table: "HistoriasClinicas",
                column: "PacienteId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Notas_EmpleadoId",
                table: "Notas",
                column: "EmpleadoId");

            migrationBuilder.CreateIndex(
                name: "IX_Notas_EvolucionId",
                table: "Notas",
                column: "EvolucionId");

            migrationBuilder.CreateIndex(
                name: "IX_Notas_ProfesionalId",
                table: "Notas",
                column: "ProfesionalId");

            migrationBuilder.CreateIndex(
                name: "IX_Pacientes_ObraSocialId",
                table: "Pacientes",
                column: "ObraSocialId");

            migrationBuilder.CreateIndex(
                name: "IX_Profesionales_EspecialidadId",
                table: "Profesionales",
                column: "EspecialidadId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Diagnosticos");

            migrationBuilder.DropTable(
                name: "Notas");

            migrationBuilder.DropTable(
                name: "Epicrisis");

            migrationBuilder.DropTable(
                name: "Evoluciones");

            migrationBuilder.DropTable(
                name: "Episodios");

            migrationBuilder.DropTable(
                name: "Profesionales");

            migrationBuilder.DropTable(
                name: "Empleados");

            migrationBuilder.DropTable(
                name: "HistoriasClinicas");

            migrationBuilder.DropTable(
                name: "Especialidades");

            migrationBuilder.DropTable(
                name: "Pacientes");

            migrationBuilder.DropTable(
                name: "ObrasSociales");
        }
    }
}
