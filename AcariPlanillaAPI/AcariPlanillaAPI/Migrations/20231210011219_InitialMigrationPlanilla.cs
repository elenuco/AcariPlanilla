using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AcariPlanillaAPI.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigrationPlanilla : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    UseId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UseName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.UseId);
                });

            migrationBuilder.CreateTable(
                name: "Boletas",
                columns: table => new
                {
                    BoletaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UsuariosUseId = table.Column<int>(type: "int", nullable: false),
                    CodigoEmpleado = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Corte = table.Column<DateOnly>(type: "date", nullable: false),
                    DescuentoISSS = table.Column<float>(type: "real", nullable: false),
                    DescuentoAFP = table.Column<float>(type: "real", nullable: false),
                    DescuentoRenta = table.Column<float>(type: "real", nullable: false),
                    SueldoNeto = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Boletas", x => x.BoletaId);
                    table.ForeignKey(
                        name: "FK_Boletas_Usuarios_UsuariosUseId",
                        column: x => x.UsuariosUseId,
                        principalTable: "Usuarios",
                        principalColumn: "UseId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Boletas_UsuariosUseId",
                table: "Boletas",
                column: "UsuariosUseId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Boletas");

            migrationBuilder.DropTable(
                name: "Usuarios");
        }
    }
}
