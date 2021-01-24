using Microsoft.EntityFrameworkCore.Migrations;

namespace Context.Migrations
{
    public partial class ReviewsAverageMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "ReviewAverage",
                table: "Lodgings",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "ReviewsQuantity",
                table: "Lodgings",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReviewAverage",
                table: "Lodgings");

            migrationBuilder.DropColumn(
                name: "ReviewsQuantity",
                table: "Lodgings");
        }
    }
}
