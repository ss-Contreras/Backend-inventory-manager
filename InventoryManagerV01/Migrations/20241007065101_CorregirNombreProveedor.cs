using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InventoryManagerV01.Migrations
{
    /// <inheritdoc />
    public partial class CorregirNombreProveedor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Comprobar si existe la clave foránea antigua que podría haber sido creada incorrectamente
            migrationBuilder.Sql(@"
                IF EXISTS (SELECT 1 FROM sys.foreign_keys WHERE name = 'FK_Productos_Proveedor_ProveedorID')
                BEGIN
                    ALTER TABLE [Productos] DROP CONSTRAINT [FK_Productos_Proveedor_ProveedorID];
                END
            ");

            // Agregar la nueva clave foránea correcta que apunta a la tabla Proveedores
            migrationBuilder.AddForeignKey(
                name: "FK_Productos_Proveedores_ProveedorID",
                table: "Productos",
                column: "ProveedorID",
                principalTable: "Proveedores",
                principalColumn: "ProveedorID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Comprobar si existe la clave foránea que vamos a eliminar para restaurar la situación anterior
            migrationBuilder.Sql(@"
                IF EXISTS (SELECT 1 FROM sys.foreign_keys WHERE name = 'FK_Productos_Proveedores_ProveedorID')
                BEGIN
                    ALTER TABLE [Productos] DROP CONSTRAINT [FK_Productos_Proveedores_ProveedorID];
                END
            ");

            // Restaurar la clave foránea antigua (si fuera necesario) apuntando a la tabla Proveedor
            migrationBuilder.AddForeignKey(
                name: "FK_Productos_Proveedor_ProveedorID",
                table: "Productos",
                column: "ProveedorID",
                principalTable: "Proveedores",
                principalColumn: "ProveedorID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
