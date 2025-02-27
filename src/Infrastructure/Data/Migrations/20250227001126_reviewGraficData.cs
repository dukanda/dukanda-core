using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DukandaCore.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class reviewGraficData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Location",
                table: "TouristAttractions");

            migrationBuilder.AddColumn<double>(
                name: "Latitude",
                table: "TouristAttractions",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Longitude",
                table: "TouristAttractions",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.CreateTable(
                name: "IdentityRole<Guid>",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    NormalizedName = table.Column<string>(type: "text", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdentityRole<Guid>", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "IdentityRole<Guid>",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("232fe3c4-487d-4b08-b74c-abf32f0be7c3"), null, "Tourist", "TOURIST" },
                    { new Guid("2bd1ea02-f706-4db3-9be8-bebad5348089"), null, "TourAgency", "TOUR_AGENCY" },
                    { new Guid("8f2bdcde-592c-469c-a258-fc5a4fa39a95"), null, "Admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IdentityRole<Guid>");

            migrationBuilder.DropColumn(
                name: "Latitude",
                table: "TouristAttractions");

            migrationBuilder.DropColumn(
                name: "Longitude",
                table: "TouristAttractions");

            migrationBuilder.AddColumn<string>(
                name: "Location",
                table: "TouristAttractions",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
