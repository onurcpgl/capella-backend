using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    public partial class mig2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClassificationAttributeValues_Products_ProductId",
                table: "ClassificationAttributeValues");

            migrationBuilder.AlterColumn<int>(
                name: "ProductId",
                table: "ClassificationAttributeValues",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ClassificationAttributeValues_Products_ProductId",
                table: "ClassificationAttributeValues",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClassificationAttributeValues_Products_ProductId",
                table: "ClassificationAttributeValues");

            migrationBuilder.AlterColumn<int>(
                name: "ProductId",
                table: "ClassificationAttributeValues",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_ClassificationAttributeValues_Products_ProductId",
                table: "ClassificationAttributeValues",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id");
        }
    }
}
