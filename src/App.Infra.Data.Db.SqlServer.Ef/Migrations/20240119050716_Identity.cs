using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace App.Infra.Data.Db.SqlServer.Ef.Migrations
{
    /// <inheritdoc />
    public partial class Identity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DeleteBy",
                schema: "dbo",
                table: "Products",
                newName: "AspNetUserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AspNetUserId",
                schema: "dbo",
                table: "Products",
                newName: "DeleteBy");
        }
    }
}
