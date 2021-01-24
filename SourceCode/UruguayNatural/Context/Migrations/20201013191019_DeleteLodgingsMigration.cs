using Microsoft.EntityFrameworkCore.Migrations;

namespace Context.Migrations
{
    public partial class DeleteLodgingsMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Lodgings_Address",
                table: "Lodgings");

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "Lodgings",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Lodgings",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Lodgings");

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "Lodgings",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Lodgings_Address",
                table: "Lodgings",
                column: "Address",
                unique: true,
                filter: "[Address] IS NOT NULL");
        }
    }
}
