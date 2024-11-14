using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Biodigestor.Migrations
{
    /// <inheritdoc />
    public partial class BiodigestorTop4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SensoresHumedad_BiodigestorEntities_IdBiodigestor",
                table: "SensoresHumedad");

            migrationBuilder.DropForeignKey(
                name: "FK_SensoresPresion_BiodigestorEntities_IdBiodigestor",
                table: "SensoresPresion");

            migrationBuilder.DropForeignKey(
                name: "FK_SensoresTemperatura_BiodigestorEntities_IdBiodigestor",
                table: "SensoresTemperatura");

            migrationBuilder.DropTable(
                name: "Alarmas");

            migrationBuilder.DropTable(
                name: "Alertas");

            migrationBuilder.DropIndex(
                name: "IX_SensoresPresion_IdBiodigestor",
                table: "SensoresPresion");

            migrationBuilder.DropIndex(
                name: "IX_SensoresHumedad_IdBiodigestor",
                table: "SensoresHumedad");

            migrationBuilder.DropColumn(
                name: "ValorLecturaT",
                table: "SensoresTemperatura");

            migrationBuilder.DropColumn(
                name: "ValorLecturaP",
                table: "SensoresPresion");

            migrationBuilder.DropColumn(
                name: "ValorLecturaH",
                table: "SensoresHumedad");

            migrationBuilder.DropColumn(
                name: "PasswordResetTokenExpiration",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "FechaHoraT",
                table: "SensoresTemperatura",
                newName: "FechaHora");

            migrationBuilder.RenameColumn(
                name: "IdValorLectura",
                table: "SensoresTemperatura",
                newName: "IdSensor");

            migrationBuilder.RenameColumn(
                name: "FechaHoraP",
                table: "SensoresPresion",
                newName: "FechaHora");

            migrationBuilder.RenameColumn(
                name: "IdSensorPresion",
                table: "SensoresPresion",
                newName: "IdSensor");

            migrationBuilder.RenameColumn(
                name: "FechaHoraH",
                table: "SensoresHumedad",
                newName: "FechaHora");

            migrationBuilder.RenameColumn(
                name: "IdSensorHumedad",
                table: "SensoresHumedad",
                newName: "IdSensor");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "FechaVencimiento",
                table: "Facturas",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "FechaEmision",
                table: "Facturas",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<bool>(
                name: "AcceptedTerms",
                table: "AspNetUsers",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.CreateTable(
                name: "Personal",
                columns: table => new
                {
                    Legajo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Rol = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DNI = table.Column<int>(type: "int", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Apellido = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Personal", x => x.Legajo);
                });

            migrationBuilder.CreateTable(
                name: "Registros",
                columns: table => new
                {
                    IdRegistro = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdSensor = table.Column<int>(type: "int", nullable: false),
                    IdBiodigestor = table.Column<int>(type: "int", nullable: false),
                    TipoSensor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaHora = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Alerta = table.Column<double>(type: "float", nullable: true),
                    Alarma = table.Column<double>(type: "float", nullable: true),
                    Normal = table.Column<double>(type: "float", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Registros", x => x.IdRegistro);
                });

            migrationBuilder.CreateTable(
                name: "UsuariosRegistrados",
                columns: table => new
                {
                    IdUsuarioRegistrado = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DNI = table.Column<int>(type: "int", nullable: false),
                    Rol = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsuariosRegistrados", x => x.IdUsuarioRegistrado);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_SensoresTemperatura_BiodigestorEntities_IdBiodigestor",
                table: "SensoresTemperatura",
                column: "IdBiodigestor",
                principalTable: "BiodigestorEntities",
                principalColumn: "IdBiodigestor",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SensoresTemperatura_BiodigestorEntities_IdBiodigestor",
                table: "SensoresTemperatura");

            migrationBuilder.DropTable(
                name: "Personal");

            migrationBuilder.DropTable(
                name: "Registros");

            migrationBuilder.DropTable(
                name: "UsuariosRegistrados");

            migrationBuilder.RenameColumn(
                name: "FechaHora",
                table: "SensoresTemperatura",
                newName: "FechaHoraT");

            migrationBuilder.RenameColumn(
                name: "IdSensor",
                table: "SensoresTemperatura",
                newName: "IdValorLectura");

            migrationBuilder.RenameColumn(
                name: "FechaHora",
                table: "SensoresPresion",
                newName: "FechaHoraP");

            migrationBuilder.RenameColumn(
                name: "IdSensor",
                table: "SensoresPresion",
                newName: "IdSensorPresion");

            migrationBuilder.RenameColumn(
                name: "FechaHora",
                table: "SensoresHumedad",
                newName: "FechaHoraH");

            migrationBuilder.RenameColumn(
                name: "IdSensor",
                table: "SensoresHumedad",
                newName: "IdSensorHumedad");

            migrationBuilder.AddColumn<decimal>(
                name: "ValorLecturaT",
                table: "SensoresTemperatura",
                type: "decimal(10,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "ValorLecturaP",
                table: "SensoresPresion",
                type: "decimal(10,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "ValorLecturaH",
                table: "SensoresHumedad",
                type: "decimal(10,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AlterColumn<DateTime>(
                name: "FechaVencimiento",
                table: "Facturas",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateOnly),
                oldType: "date");

            migrationBuilder.AlterColumn<DateTime>(
                name: "FechaEmision",
                table: "Facturas",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateOnly),
                oldType: "date");

            migrationBuilder.AlterColumn<bool>(
                name: "AcceptedTerms",
                table: "AspNetUsers",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "PasswordResetTokenExpiration",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Alarmas",
                columns: table => new
                {
                    IdAlarma = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FechaHora = table.Column<DateTime>(type: "datetime2", nullable: false),
                    HoraAlarma = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SensorAlarma = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Alarmas", x => x.IdAlarma);
                });

            migrationBuilder.CreateTable(
                name: "Alertas",
                columns: table => new
                {
                    IdAlerta = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FechaHora = table.Column<DateTime>(type: "datetime2", nullable: false),
                    HoraAlerta = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SensorAlerta = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Alertas", x => x.IdAlerta);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SensoresPresion_IdBiodigestor",
                table: "SensoresPresion",
                column: "IdBiodigestor");

            migrationBuilder.CreateIndex(
                name: "IX_SensoresHumedad_IdBiodigestor",
                table: "SensoresHumedad",
                column: "IdBiodigestor");

            migrationBuilder.AddForeignKey(
                name: "FK_SensoresHumedad_BiodigestorEntities_IdBiodigestor",
                table: "SensoresHumedad",
                column: "IdBiodigestor",
                principalTable: "BiodigestorEntities",
                principalColumn: "IdBiodigestor",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SensoresPresion_BiodigestorEntities_IdBiodigestor",
                table: "SensoresPresion",
                column: "IdBiodigestor",
                principalTable: "BiodigestorEntities",
                principalColumn: "IdBiodigestor",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SensoresTemperatura_BiodigestorEntities_IdBiodigestor",
                table: "SensoresTemperatura",
                column: "IdBiodigestor",
                principalTable: "BiodigestorEntities",
                principalColumn: "IdBiodigestor",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
