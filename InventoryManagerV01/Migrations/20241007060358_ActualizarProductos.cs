using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InventoryManagerV01.Migrations
{
    /// <inheritdoc />
    public partial class ActualizarProductos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RutaIMagen",
                table: "Productos",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RutaLocalIMagen",
                table: "Productos",
                type: "nvarchar(max)",
                nullable: true);
        }
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RutaIMagen",
                table: "Productos");

            migrationBuilder.DropColumn(
                name: "RutaLocalIMagen",
                table: "Productos");
        }

    }
}
