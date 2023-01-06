using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Persistence.Migrations
{
    public partial class mig_variant : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DiscountPrice",
                table: "VariantItems");

            migrationBuilder.DropColumn(
                name: "SalePrice",
                table: "VariantItems");

            migrationBuilder.AddColumn<int>(
                name: "VariantItemId",
                table: "ClassificationAttributeValues",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "VariantValues",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Code = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    VariantId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VariantValues", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VariantValues_Variants_VariantId",
                        column: x => x.VariantId,
                        principalTable: "Variants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClassificationAttributeValues_VariantItemId",
                table: "ClassificationAttributeValues",
                column: "VariantItemId");

            migrationBuilder.CreateIndex(
                name: "IX_VariantValues_VariantId",
                table: "VariantValues",
                column: "VariantId");

            migrationBuilder.AddForeignKey(
                name: "FK_ClassificationAttributeValues_VariantItems_VariantItemId",
                table: "ClassificationAttributeValues",
                column: "VariantItemId",
                principalTable: "VariantItems",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClassificationAttributeValues_VariantItems_VariantItemId",
                table: "ClassificationAttributeValues");

            migrationBuilder.DropTable(
                name: "VariantValues");

            migrationBuilder.DropIndex(
                name: "IX_ClassificationAttributeValues_VariantItemId",
                table: "ClassificationAttributeValues");

            migrationBuilder.DropColumn(
                name: "VariantItemId",
                table: "ClassificationAttributeValues");

            migrationBuilder.AddColumn<int>(
                name: "DiscountPrice",
                table: "VariantItems",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SalePrice",
                table: "VariantItems",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
