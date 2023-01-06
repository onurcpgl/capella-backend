using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    public partial class mig_variant_item_relation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClassificationAttributeValues_VariantItems_VariantItemId",
                table: "ClassificationAttributeValues");

            migrationBuilder.DropForeignKey(
                name: "FK_Gallery_VariantItems_VariantItemId",
                table: "Gallery");

            migrationBuilder.DropIndex(
                name: "IX_Gallery_VariantItemId",
                table: "Gallery");

            migrationBuilder.DropIndex(
                name: "IX_ClassificationAttributeValues_VariantItemId",
                table: "ClassificationAttributeValues");

            migrationBuilder.DropColumn(
                name: "VariantItemId",
                table: "Gallery");

            migrationBuilder.DropColumn(
                name: "VariantItemId",
                table: "ClassificationAttributeValues");

            migrationBuilder.CreateTable(
                name: "ClassificationAttributeValuesVariantItems",
                columns: table => new
                {
                    ClassificationAttributeValuesId = table.Column<int>(type: "integer", nullable: false),
                    VariantItemsId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClassificationAttributeValuesVariantItems", x => new { x.ClassificationAttributeValuesId, x.VariantItemsId });
                    table.ForeignKey(
                        name: "FK_ClassificationAttributeValuesVariantItems_ClassificationAtt~",
                        column: x => x.ClassificationAttributeValuesId,
                        principalTable: "ClassificationAttributeValues",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClassificationAttributeValuesVariantItems_VariantItems_Vari~",
                        column: x => x.VariantItemsId,
                        principalTable: "VariantItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GalleriesVariantItems",
                columns: table => new
                {
                    GalleriesId = table.Column<int>(type: "integer", nullable: false),
                    VariantItemsId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GalleriesVariantItems", x => new { x.GalleriesId, x.VariantItemsId });
                    table.ForeignKey(
                        name: "FK_GalleriesVariantItems_Gallery_GalleriesId",
                        column: x => x.GalleriesId,
                        principalTable: "Gallery",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GalleriesVariantItems_VariantItems_VariantItemsId",
                        column: x => x.VariantItemsId,
                        principalTable: "VariantItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClassificationAttributeValuesVariantItems_VariantItemsId",
                table: "ClassificationAttributeValuesVariantItems",
                column: "VariantItemsId");

            migrationBuilder.CreateIndex(
                name: "IX_GalleriesVariantItems_VariantItemsId",
                table: "GalleriesVariantItems",
                column: "VariantItemsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClassificationAttributeValuesVariantItems");

            migrationBuilder.DropTable(
                name: "GalleriesVariantItems");

            migrationBuilder.AddColumn<int>(
                name: "VariantItemId",
                table: "Gallery",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "VariantItemId",
                table: "ClassificationAttributeValues",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Gallery_VariantItemId",
                table: "Gallery",
                column: "VariantItemId");

            migrationBuilder.CreateIndex(
                name: "IX_ClassificationAttributeValues_VariantItemId",
                table: "ClassificationAttributeValues",
                column: "VariantItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_ClassificationAttributeValues_VariantItems_VariantItemId",
                table: "ClassificationAttributeValues",
                column: "VariantItemId",
                principalTable: "VariantItems",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Gallery_VariantItems_VariantItemId",
                table: "Gallery",
                column: "VariantItemId",
                principalTable: "VariantItems",
                principalColumn: "Id");
        }
    }
}
