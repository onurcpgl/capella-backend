using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    public partial class mig_variant_item : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VariantItems_Variants_VariantId",
                table: "VariantItems");

            migrationBuilder.DropTable(
                name: "ProductsVariants");

            migrationBuilder.DropIndex(
                name: "IX_VariantItems_VariantId",
                table: "VariantItems");

            migrationBuilder.DropColumn(
                name: "VariantId",
                table: "VariantItems");

            migrationBuilder.AddColumn<int>(
                name: "VariantId",
                table: "Products",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ProductsVariantItems",
                columns: table => new
                {
                    ProductsId = table.Column<int>(type: "integer", nullable: false),
                    VariantItemsId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductsVariantItems", x => new { x.ProductsId, x.VariantItemsId });
                    table.ForeignKey(
                        name: "FK_ProductsVariantItems_Products_ProductsId",
                        column: x => x.ProductsId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductsVariantItems_VariantItems_VariantItemsId",
                        column: x => x.VariantItemsId,
                        principalTable: "VariantItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VariantValuesVariantItems",
                columns: table => new
                {
                    VariantItemsId = table.Column<int>(type: "integer", nullable: false),
                    VariantValuesId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VariantValuesVariantItems", x => new { x.VariantItemsId, x.VariantValuesId });
                    table.ForeignKey(
                        name: "FK_VariantValuesVariantItems_VariantItems_VariantItemsId",
                        column: x => x.VariantItemsId,
                        principalTable: "VariantItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VariantValuesVariantItems_VariantValues_VariantValuesId",
                        column: x => x.VariantValuesId,
                        principalTable: "VariantValues",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_VariantId",
                table: "Products",
                column: "VariantId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductsVariantItems_VariantItemsId",
                table: "ProductsVariantItems",
                column: "VariantItemsId");

            migrationBuilder.CreateIndex(
                name: "IX_VariantValuesVariantItems_VariantValuesId",
                table: "VariantValuesVariantItems",
                column: "VariantValuesId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Variants_VariantId",
                table: "Products",
                column: "VariantId",
                principalTable: "Variants",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Variants_VariantId",
                table: "Products");

            migrationBuilder.DropTable(
                name: "ProductsVariantItems");

            migrationBuilder.DropTable(
                name: "VariantValuesVariantItems");

            migrationBuilder.DropIndex(
                name: "IX_Products_VariantId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "VariantId",
                table: "Products");

            migrationBuilder.AddColumn<int>(
                name: "VariantId",
                table: "VariantItems",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ProductsVariants",
                columns: table => new
                {
                    ProductsId = table.Column<int>(type: "integer", nullable: false),
                    VariantsId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductsVariants", x => new { x.ProductsId, x.VariantsId });
                    table.ForeignKey(
                        name: "FK_ProductsVariants_Products_ProductsId",
                        column: x => x.ProductsId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductsVariants_Variants_VariantsId",
                        column: x => x.VariantsId,
                        principalTable: "Variants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_VariantItems_VariantId",
                table: "VariantItems",
                column: "VariantId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductsVariants_VariantsId",
                table: "ProductsVariants",
                column: "VariantsId");

            migrationBuilder.AddForeignKey(
                name: "FK_VariantItems_Variants_VariantId",
                table: "VariantItems",
                column: "VariantId",
                principalTable: "Variants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
