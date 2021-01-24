using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Context.Migrations
{
    public partial class CategoriesAndRegionEndpointsMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Regions",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "MapTransparent",
                table: "Regions",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "MapYellow",
                table: "Regions",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "VideoPath",
                table: "Regions",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FaIconName",
                table: "Categories",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Regions");

            migrationBuilder.DropColumn(
                name: "MapTransparent",
                table: "Regions");

            migrationBuilder.DropColumn(
                name: "MapYellow",
                table: "Regions");

            migrationBuilder.DropColumn(
                name: "VideoPath",
                table: "Regions");

            migrationBuilder.DropColumn(
                name: "FaIconName",
                table: "Categories");
        }
    }
}
