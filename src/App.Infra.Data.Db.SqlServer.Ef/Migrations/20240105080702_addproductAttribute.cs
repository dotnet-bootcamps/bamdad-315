using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace App.Infra.Data.Db.SqlServer.Ef.Migrations
{
    /// <inheritdoc />
    public partial class addproductAttribute : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Attributes",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attributes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AttributeProductCategory",
                schema: "dbo",
                columns: table => new
                {
                    AttributesId = table.Column<int>(type: "int", nullable: false),
                    ProductCategoriesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AttributeProductCategory", x => new { x.AttributesId, x.ProductCategoriesId });
                    table.ForeignKey(
                        name: "FK_AttributeProductCategory_Attributes_AttributesId",
                        column: x => x.AttributesId,
                        principalSchema: "dbo",
                        principalTable: "Attributes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AttributeProductCategory_ProductCategories_ProductCategoriesId",
                        column: x => x.ProductCategoriesId,
                        principalSchema: "dbo",
                        principalTable: "ProductCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductAttributes",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    AttributeId = table.Column<int>(type: "int", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductAttributes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductAttributes_Attributes_AttributeId",
                        column: x => x.AttributeId,
                        principalSchema: "dbo",
                        principalTable: "Attributes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductAttributes_Products_ProductId",
                        column: x => x.ProductId,
                        principalSchema: "dbo",
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AttributeProductCategory_ProductCategoriesId",
                schema: "dbo",
                table: "AttributeProductCategory",
                column: "ProductCategoriesId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductAttributes_AttributeId",
                schema: "dbo",
                table: "ProductAttributes",
                column: "AttributeId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductAttributes_ProductId",
                schema: "dbo",
                table: "ProductAttributes",
                column: "ProductId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AttributeProductCategory",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "ProductAttributes",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Attributes",
                schema: "dbo");
        }
    }
}
