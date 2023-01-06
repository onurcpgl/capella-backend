using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    public partial class mig_clas_atr_value : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Options_ClassificationAttributeValues_ClassificationAttribu~",
                table: "Options");

            migrationBuilder.DropIndex(
                name: "IX_Options_ClassificationAttributeValueId",
                table: "Options");

            migrationBuilder.DropColumn(
                name: "ClassificationAttributeValueId",
                table: "Options");

            migrationBuilder.AlterColumn<string>(
                name: "Value",
                table: "ClassificationAttributeValues",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.CreateTable(
                name: "ClassificationAttributeValuesOptions",
                columns: table => new
                {
                    ClassificationAttributeValuesId = table.Column<int>(type: "integer", nullable: false),
                    OptionsId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClassificationAttributeValuesOptions", x => new { x.ClassificationAttributeValuesId, x.OptionsId });
                    table.ForeignKey(
                        name: "FK_ClassificationAttributeValuesOptions_ClassificationAttribut~",
                        column: x => x.ClassificationAttributeValuesId,
                        principalTable: "ClassificationAttributeValues",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClassificationAttributeValuesOptions_Options_OptionsId",
                        column: x => x.OptionsId,
                        principalTable: "Options",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClassificationAttributeValuesOptions_OptionsId",
                table: "ClassificationAttributeValuesOptions",
                column: "OptionsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClassificationAttributeValuesOptions");

            migrationBuilder.AddColumn<int>(
                name: "ClassificationAttributeValueId",
                table: "Options",
                type: "integer",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Value",
                table: "ClassificationAttributeValues",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Options_ClassificationAttributeValueId",
                table: "Options",
                column: "ClassificationAttributeValueId");

            migrationBuilder.AddForeignKey(
                name: "FK_Options_ClassificationAttributeValues_ClassificationAttribu~",
                table: "Options",
                column: "ClassificationAttributeValueId",
                principalTable: "ClassificationAttributeValues",
                principalColumn: "Id");
        }
    }
}
