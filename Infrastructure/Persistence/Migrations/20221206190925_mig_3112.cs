using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Persistence.Migrations
{
    public partial class mig_3112 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClassificationAttributes_Units_UnitId",
                table: "ClassificationAttributes");

            migrationBuilder.AddColumn<int>(
                name: "GalleryId",
                table: "Medias",
                type: "integer",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "UnitId",
                table: "ClassificationAttributes",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "ClassificationAttributes",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.CreateTable(
                name: "Gallery",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Code = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gallery", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MediaFormats",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Code = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Height = table.Column<int>(type: "integer", nullable: false),
                    Width = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MediaFormats", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductsMedias",
                columns: table => new
                {
                    MediasId = table.Column<int>(type: "integer", nullable: false),
                    ProductsId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductsMedias", x => new { x.MediasId, x.ProductsId });
                    table.ForeignKey(
                        name: "FK_ProductsMedias_Medias_MediasId",
                        column: x => x.MediasId,
                        principalTable: "Medias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductsMedias_Products_ProductsId",
                        column: x => x.ProductsId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GalleryProduct",
                columns: table => new
                {
                    GalleriesId = table.Column<int>(type: "integer", nullable: false),
                    ProductsId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GalleryProduct", x => new { x.GalleriesId, x.ProductsId });
                    table.ForeignKey(
                        name: "FK_GalleryProduct_Gallery_GalleriesId",
                        column: x => x.GalleriesId,
                        principalTable: "Gallery",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GalleryProduct_Products_ProductsId",
                        column: x => x.ProductsId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Medias_GalleryId",
                table: "Medias",
                column: "GalleryId");

            migrationBuilder.CreateIndex(
                name: "IX_GalleryProduct_ProductsId",
                table: "GalleryProduct",
                column: "ProductsId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductsMedias_ProductsId",
                table: "ProductsMedias",
                column: "ProductsId");

            migrationBuilder.AddForeignKey(
                name: "FK_ClassificationAttributes_Units_UnitId",
                table: "ClassificationAttributes",
                column: "UnitId",
                principalTable: "Units",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Medias_Gallery_GalleryId",
                table: "Medias",
                column: "GalleryId",
                principalTable: "Gallery",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClassificationAttributes_Units_UnitId",
                table: "ClassificationAttributes");

            migrationBuilder.DropForeignKey(
                name: "FK_Medias_Gallery_GalleryId",
                table: "Medias");

            migrationBuilder.DropTable(
                name: "GalleryProduct");

            migrationBuilder.DropTable(
                name: "MediaFormats");

            migrationBuilder.DropTable(
                name: "ProductsMedias");

            migrationBuilder.DropTable(
                name: "Gallery");

            migrationBuilder.DropIndex(
                name: "IX_Medias_GalleryId",
                table: "Medias");

            migrationBuilder.DropColumn(
                name: "GalleryId",
                table: "Medias");

            migrationBuilder.AlterColumn<int>(
                name: "UnitId",
                table: "ClassificationAttributes",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "ClassificationAttributes",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ClassificationAttributes_Units_UnitId",
                table: "ClassificationAttributes",
                column: "UnitId",
                principalTable: "Units",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
