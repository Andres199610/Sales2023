using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sales.API.Migrations
{
    /// <inheritdoc />
    public partial class Database : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProdCategory_Categories_CategoryId",
                table: "ProdCategory");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProdCategory",
                table: "ProdCategory");

            migrationBuilder.DropIndex(
                name: "IX_ProdCategory_Name",
                table: "ProdCategory");

            migrationBuilder.RenameTable(
                name: "ProdCategory",
                newName: "ProdCategories");

            migrationBuilder.RenameIndex(
                name: "IX_ProdCategory_CategoryId",
                table: "ProdCategories",
                newName: "IX_ProdCategories_CategoryId");

            migrationBuilder.AlterColumn<int>(
                name: "CategoryId",
                table: "ProdCategories",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProdCategories",
                table: "ProdCategories",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    ProdCategoryId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_ProdCategories_ProdCategoryId",
                        column: x => x.ProdCategoryId,
                        principalTable: "ProdCategories",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_ProdCategoryId",
                table: "Products",
                column: "ProdCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProdCategories_Categories_CategoryId",
                table: "ProdCategories",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProdCategories_Categories_CategoryId",
                table: "ProdCategories");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProdCategories",
                table: "ProdCategories");

            migrationBuilder.RenameTable(
                name: "ProdCategories",
                newName: "ProdCategory");

            migrationBuilder.RenameIndex(
                name: "IX_ProdCategories_CategoryId",
                table: "ProdCategory",
                newName: "IX_ProdCategory_CategoryId");

            migrationBuilder.AlterColumn<int>(
                name: "CategoryId",
                table: "ProdCategory",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProdCategory",
                table: "ProdCategory",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_ProdCategory_Name",
                table: "ProdCategory",
                column: "Name",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ProdCategory_Categories_CategoryId",
                table: "ProdCategory",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
