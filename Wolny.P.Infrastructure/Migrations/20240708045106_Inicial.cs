using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Wolny.P.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Inicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Camiones",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Disponible = table.Column<bool>(type: "bit", nullable: false),
                    Patente = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Ubicacion = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Camiones", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Ciudades",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Distancias = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Ubicacion = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ciudades", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Recorridos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CamionId = table.Column<int>(type: "int", nullable: true),
                    FechaFin = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FechaInicio = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Finalizado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recorridos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Recorridos_Camiones_CamionId",
                        column: x => x.CamionId,
                        principalTable: "Camiones",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Pedidos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Entregado = table.Column<bool>(type: "bit", nullable: false),
                    CiudadId = table.Column<int>(type: "int", nullable: false),
                    RecorridoId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pedidos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pedidos_Ciudades_CiudadId",
                        column: x => x.CiudadId,
                        principalTable: "Ciudades",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Pedidos_Recorridos_RecorridoId",
                        column: x => x.RecorridoId,
                        principalTable: "Recorridos",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PlanRecorridos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FechaFin = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Finalizado = table.Column<bool>(type: "bit", nullable: false),
                    Prioridad = table.Column<int>(type: "int", nullable: false),
                    CiudadId = table.Column<int>(type: "int", nullable: false),
                    RecorridoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlanRecorridos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlanRecorridos_Ciudades_CiudadId",
                        column: x => x.CiudadId,
                        principalTable: "Ciudades",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlanRecorridos_Recorridos_RecorridoId",
                        column: x => x.RecorridoId,
                        principalTable: "Recorridos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Camiones",
                columns: new[] { "Id", "Disponible", "Patente", "Ubicacion" },
                values: new object[,]
                {
                    { 1, false, "XYZ0", "{\"latitud\":-26.95,\"longitud\":-58.83}" },
                    { 2, true, "ABC2", "{\"latitud\":-34.6,\"longitud\":-58.37}" },
                    { 3, true, "ABC3", "{\"latitud\":-34.6,\"longitud\":-58.37}" },
                    { 4, true, "ABC4", "{\"latitud\":-34.6,\"longitud\":-58.37}" }
                });

            migrationBuilder.InsertData(
                table: "Ciudades",
                columns: new[] { "Id", "Distancias", "Nombre", "Ubicacion" },
                values: new object[,]
                {
                    { 1, "{\"2\":646,\"3\":792,\"4\":933,\"5\":53,\"6\":986,\"7\":985,\"8\":989}", "CABA", "{\"latitud\":-34.6,\"longitud\":-58.37}" },
                    { 2, "{\"1\":646,\"3\":677,\"4\":824,\"5\":698,\"6\":340,\"7\":466,\"8\":907}", "Córdoba", "{\"latitud\":-31.42,\"longitud\":-64.18}" },
                    { 3, "{\"1\":792,\"2\":677,\"4\":157,\"5\":830,\"6\":814,\"7\":1131,\"8\":1534}", "Corrientes", "{\"latitud\":-27.47,\"longitud\":-58.83}" },
                    { 4, "{\"1\":933,\"2\":824,\"3\":157,\"5\":968,\"6\":927,\"7\":1269,\"8\":1690}", "Formosa", "{\"latitud\":-26.18,\"longitud\":-58.17}" },
                    { 5, "{\"1\":53,\"2\":698,\"3\":830,\"4\":968,\"6\":1038,\"7\":1029,\"8\":1005}", "La Plata", "{\"latitud\":-34.92,\"longitud\":-57.95}" },
                    { 6, "{\"1\":986,\"2\":340,\"3\":814,\"4\":927,\"5\":1038,\"7\":427,\"8\":1063}", "La Rioja", "{\"latitud\":-29.41,\"longitud\":-66.85}" },
                    { 7, "{\"1\":985,\"2\":466,\"3\":1131,\"4\":1269,\"5\":1029,\"6\":427,\"8\":676}", "Mendoza", "{\"latitud\":-32.88,\"longitud\":-68.84}" },
                    { 8, "{\"1\":989,\"2\":907,\"3\":1534,\"4\":1690,\"5\":1005,\"6\":1063,\"7\":676}", "Neuquén", "{\"latitud\":-38.95,\"longitud\":-68.05}" }
                });

            migrationBuilder.InsertData(
                table: "Pedidos",
                columns: new[] { "Id", "CiudadId", "Entregado", "RecorridoId" },
                values: new object[,]
                {
                    { 1, 1, true, null },
                    { 2, 2, true, null },
                    { 3, 3, true, null },
                    { 4, 4, true, null },
                    { 5, 5, true, null },
                    { 6, 6, true, null },
                    { 7, 7, true, null },
                    { 8, 8, true, null },
                    { 9, 1, true, null },
                    { 10, 2, true, null },
                    { 11, 3, true, null },
                    { 12, 4, true, null },
                    { 13, 5, true, null },
                    { 14, 6, true, null },
                    { 15, 7, true, null },
                    { 16, 8, true, null },
                    { 17, 1, true, null },
                    { 18, 2, true, null },
                    { 19, 3, true, null },
                    { 20, 4, false, null },
                    { 21, 5, false, null },
                    { 22, 6, false, null },
                    { 23, 7, false, null },
                    { 24, 8, false, null },
                    { 25, 1, false, null },
                    { 26, 2, false, null },
                    { 27, 3, false, null },
                    { 28, 4, false, null },
                    { 29, 5, false, null },
                    { 30, 6, false, null },
                    { 31, 7, false, null },
                    { 32, 8, false, null },
                    { 33, 1, false, null },
                    { 34, 2, false, null },
                    { 35, 3, false, null },
                    { 36, 4, false, null },
                    { 37, 5, false, null },
                    { 38, 6, false, null },
                    { 39, 7, false, null },
                    { 40, 8, false, null },
                    { 41, 1, false, null },
                    { 42, 2, false, null },
                    { 43, 3, false, null },
                    { 44, 4, false, null },
                    { 45, 5, false, null },
                    { 46, 6, false, null },
                    { 47, 7, false, null },
                    { 48, 8, false, null },
                    { 49, 1, false, null },
                    { 50, 2, false, null },
                    { 51, 3, false, null },
                    { 52, 4, false, null },
                    { 53, 5, false, null },
                    { 54, 6, false, null },
                    { 55, 7, false, null },
                    { 56, 8, false, null },
                    { 57, 1, false, null },
                    { 58, 2, false, null },
                    { 59, 3, false, null },
                    { 60, 4, false, null },
                    { 61, 5, false, null },
                    { 62, 6, false, null },
                    { 63, 7, false, null },
                    { 64, 8, false, null },
                    { 65, 1, false, null },
                    { 66, 2, false, null },
                    { 67, 3, false, null },
                    { 68, 4, false, null },
                    { 69, 5, false, null },
                    { 70, 6, false, null },
                    { 71, 7, false, null },
                    { 72, 8, false, null },
                    { 73, 1, false, null },
                    { 74, 2, false, null },
                    { 75, 3, false, null },
                    { 76, 4, false, null },
                    { 77, 5, false, null },
                    { 78, 6, false, null },
                    { 79, 7, false, null },
                    { 80, 8, false, null },
                    { 81, 1, false, null },
                    { 82, 2, false, null },
                    { 83, 3, false, null },
                    { 84, 4, false, null },
                    { 85, 5, false, null },
                    { 86, 6, false, null },
                    { 87, 7, false, null },
                    { 88, 8, false, null },
                    { 89, 1, false, null },
                    { 90, 2, false, null },
                    { 91, 3, false, null },
                    { 92, 4, false, null },
                    { 93, 5, false, null },
                    { 94, 6, false, null },
                    { 95, 7, false, null },
                    { 96, 8, false, null },
                    { 97, 1, false, null },
                    { 98, 2, false, null },
                    { 99, 3, false, null },
                    { 100, 4, false, null },
                    { 101, 5, false, null },
                    { 102, 6, false, null },
                    { 103, 7, false, null },
                    { 104, 8, false, null },
                    { 105, 1, false, null },
                    { 106, 2, false, null },
                    { 107, 3, false, null },
                    { 108, 4, false, null },
                    { 109, 5, false, null },
                    { 110, 6, false, null },
                    { 111, 7, false, null },
                    { 112, 8, false, null },
                    { 113, 1, false, null },
                    { 114, 2, false, null },
                    { 115, 3, false, null },
                    { 116, 4, false, null },
                    { 117, 5, false, null },
                    { 118, 6, false, null },
                    { 119, 7, false, null },
                    { 120, 8, false, null },
                    { 121, 1, false, null },
                    { 122, 2, false, null },
                    { 123, 3, false, null },
                    { 124, 4, false, null },
                    { 125, 5, false, null },
                    { 126, 6, false, null },
                    { 127, 7, false, null },
                    { 128, 8, false, null },
                    { 129, 1, false, null },
                    { 130, 2, false, null },
                    { 131, 3, false, null },
                    { 132, 4, false, null },
                    { 133, 5, false, null },
                    { 134, 6, false, null },
                    { 135, 7, false, null },
                    { 136, 8, false, null },
                    { 137, 1, false, null },
                    { 138, 2, false, null },
                    { 139, 3, false, null },
                    { 140, 4, false, null },
                    { 141, 5, false, null },
                    { 142, 6, false, null },
                    { 143, 7, false, null },
                    { 144, 8, false, null },
                    { 145, 1, false, null },
                    { 146, 2, false, null },
                    { 147, 3, false, null },
                    { 148, 4, false, null },
                    { 149, 5, false, null },
                    { 150, 6, false, null },
                    { 151, 7, false, null },
                    { 152, 8, false, null },
                    { 153, 1, false, null },
                    { 154, 2, false, null },
                    { 155, 3, false, null },
                    { 156, 4, false, null },
                    { 157, 5, false, null },
                    { 158, 6, false, null },
                    { 159, 7, false, null },
                    { 160, 8, false, null },
                    { 161, 1, false, null },
                    { 162, 2, false, null },
                    { 163, 3, false, null },
                    { 164, 4, false, null },
                    { 165, 5, false, null },
                    { 166, 6, false, null },
                    { 167, 7, false, null },
                    { 168, 8, false, null },
                    { 169, 1, false, null },
                    { 170, 2, false, null },
                    { 171, 3, false, null },
                    { 172, 4, false, null },
                    { 173, 5, false, null },
                    { 174, 6, false, null },
                    { 175, 7, false, null },
                    { 176, 8, false, null },
                    { 177, 1, false, null },
                    { 178, 2, false, null },
                    { 179, 3, false, null },
                    { 180, 4, false, null },
                    { 181, 5, false, null },
                    { 182, 6, false, null },
                    { 183, 7, false, null },
                    { 184, 8, false, null },
                    { 185, 1, false, null },
                    { 186, 2, false, null },
                    { 187, 3, false, null },
                    { 188, 4, false, null },
                    { 189, 5, false, null },
                    { 190, 6, false, null },
                    { 191, 7, false, null },
                    { 192, 8, false, null },
                    { 193, 1, false, null },
                    { 194, 2, false, null },
                    { 195, 3, false, null },
                    { 196, 4, false, null },
                    { 197, 5, false, null },
                    { 198, 6, false, null },
                    { 199, 7, false, null },
                    { 200, 8, false, null },
                    { 201, 1, false, null },
                    { 202, 2, false, null },
                    { 203, 3, false, null },
                    { 204, 4, false, null },
                    { 205, 5, false, null },
                    { 206, 6, false, null },
                    { 207, 7, false, null },
                    { 208, 8, false, null },
                    { 209, 1, false, null },
                    { 210, 2, false, null },
                    { 211, 3, false, null },
                    { 212, 4, false, null },
                    { 213, 5, false, null },
                    { 214, 6, false, null },
                    { 215, 7, false, null },
                    { 216, 8, false, null },
                    { 217, 1, false, null },
                    { 218, 2, false, null },
                    { 219, 3, false, null },
                    { 220, 4, false, null },
                    { 221, 5, false, null },
                    { 222, 6, false, null },
                    { 223, 7, false, null },
                    { 224, 8, false, null },
                    { 225, 1, false, null },
                    { 226, 2, false, null },
                    { 227, 3, false, null },
                    { 228, 4, false, null },
                    { 229, 5, false, null },
                    { 230, 6, false, null },
                    { 231, 7, false, null },
                    { 232, 8, false, null },
                    { 233, 1, false, null },
                    { 234, 2, false, null },
                    { 235, 3, false, null },
                    { 236, 4, false, null },
                    { 237, 5, false, null },
                    { 238, 6, false, null },
                    { 239, 7, false, null },
                    { 240, 8, false, null },
                    { 241, 1, false, null },
                    { 242, 2, false, null },
                    { 243, 3, false, null },
                    { 244, 4, false, null },
                    { 245, 5, false, null },
                    { 246, 6, false, null },
                    { 247, 7, false, null },
                    { 248, 8, false, null },
                    { 249, 1, false, null },
                    { 250, 2, false, null },
                    { 251, 3, false, null },
                    { 252, 4, false, null },
                    { 253, 5, false, null },
                    { 254, 6, false, null },
                    { 255, 7, false, null },
                    { 256, 8, false, null },
                    { 257, 1, false, null },
                    { 258, 2, false, null },
                    { 259, 3, false, null },
                    { 260, 4, false, null },
                    { 261, 5, false, null },
                    { 262, 6, false, null },
                    { 263, 7, false, null },
                    { 264, 8, false, null },
                    { 265, 1, false, null },
                    { 266, 2, false, null },
                    { 267, 3, false, null },
                    { 268, 4, false, null },
                    { 269, 5, false, null },
                    { 270, 6, false, null },
                    { 271, 7, false, null },
                    { 272, 8, false, null },
                    { 273, 1, false, null },
                    { 274, 2, false, null },
                    { 275, 3, false, null },
                    { 276, 4, false, null },
                    { 277, 5, false, null },
                    { 278, 6, false, null },
                    { 279, 7, false, null },
                    { 280, 8, false, null },
                    { 281, 1, false, null },
                    { 282, 2, false, null },
                    { 283, 3, false, null },
                    { 284, 4, false, null },
                    { 285, 5, false, null },
                    { 286, 6, false, null },
                    { 287, 7, false, null },
                    { 288, 8, false, null },
                    { 289, 1, false, null },
                    { 290, 2, false, null },
                    { 291, 3, false, null },
                    { 292, 4, false, null },
                    { 293, 5, false, null },
                    { 294, 6, false, null },
                    { 295, 7, false, null },
                    { 296, 8, false, null },
                    { 297, 1, false, null },
                    { 298, 2, false, null },
                    { 299, 3, false, null },
                    { 300, 4, false, null },
                    { 301, 5, false, null },
                    { 302, 6, false, null },
                    { 303, 7, false, null },
                    { 304, 8, false, null },
                    { 305, 1, false, null },
                    { 306, 2, false, null },
                    { 307, 3, false, null },
                    { 308, 4, false, null },
                    { 309, 5, false, null },
                    { 310, 6, false, null },
                    { 311, 7, false, null },
                    { 312, 8, false, null },
                    { 313, 1, false, null },
                    { 314, 2, false, null },
                    { 315, 3, false, null },
                    { 316, 4, false, null },
                    { 317, 5, false, null },
                    { 318, 6, false, null },
                    { 319, 7, false, null },
                    { 320, 8, false, null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Pedidos_CiudadId",
                table: "Pedidos",
                column: "CiudadId");

            migrationBuilder.CreateIndex(
                name: "IX_Pedidos_RecorridoId",
                table: "Pedidos",
                column: "RecorridoId");

            migrationBuilder.CreateIndex(
                name: "IX_PlanRecorridos_CiudadId",
                table: "PlanRecorridos",
                column: "CiudadId");

            migrationBuilder.CreateIndex(
                name: "IX_PlanRecorridos_RecorridoId",
                table: "PlanRecorridos",
                column: "RecorridoId");

            migrationBuilder.CreateIndex(
                name: "IX_Recorridos_CamionId",
                table: "Recorridos",
                column: "CamionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Pedidos");

            migrationBuilder.DropTable(
                name: "PlanRecorridos");

            migrationBuilder.DropTable(
                name: "Ciudades");

            migrationBuilder.DropTable(
                name: "Recorridos");

            migrationBuilder.DropTable(
                name: "Camiones");
        }
    }
}
