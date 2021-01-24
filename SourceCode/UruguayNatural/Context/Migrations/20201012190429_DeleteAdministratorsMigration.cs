using Microsoft.EntityFrameworkCore.Migrations;

namespace Context.Migrations
{
    public partial class DeleteAdministratorsMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sessions_Administrators_AdministratorId",
                table: "Sessions");

            migrationBuilder.AlterColumn<int>(
                name: "AdministratorId",
                table: "Sessions",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Sessions_Administrators_AdministratorId",
                table: "Sessions",
                column: "AdministratorId",
                principalTable: "Administrators",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sessions_Administrators_AdministratorId",
                table: "Sessions");

            migrationBuilder.AlterColumn<int>(
                name: "AdministratorId",
                table: "Sessions",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Sessions_Administrators_AdministratorId",
                table: "Sessions",
                column: "AdministratorId",
                principalTable: "Administrators",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
