using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DukandaCore.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddCoverImageUrlTour : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tours_TouristAttractions_TouristAttractionId",
                table: "Tours");

            migrationBuilder.RenameColumn(
                name: "TouristAttractionId",
                table: "Tours",
                newName: "PublishedById");

            migrationBuilder.RenameIndex(
                name: "IX_Tours_TouristAttractionId",
                table: "Tours",
                newName: "IX_Tours_PublishedById");

            migrationBuilder.AddColumn<string>(
                name: "CoverImageUrl",
                table: "Tours",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "PublishedAt",
                table: "Tours",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "TourItineraries",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TourId = table.Column<Guid>(type: "uuid", nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    DisplayOrder = table.Column<int>(type: "integer", nullable: false),
                    Created = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    CreatedById = table.Column<Guid>(type: "uuid", nullable: true),
                    LastModified = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    LastModifiedById = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TourItineraries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TourItineraries_Tours_TourId",
                        column: x => x.TourId,
                        principalTable: "Tours",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TourTouristAttraction",
                columns: table => new
                {
                    AttractionsId = table.Column<Guid>(type: "uuid", nullable: false),
                    ToursId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TourTouristAttraction", x => new { x.AttractionsId, x.ToursId });
                    table.ForeignKey(
                        name: "FK_TourTouristAttraction_TouristAttractions_AttractionsId",
                        column: x => x.AttractionsId,
                        principalTable: "TouristAttractions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TourTouristAttraction_Tours_ToursId",
                        column: x => x.ToursId,
                        principalTable: "Tours",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TourItineraries_TourId",
                table: "TourItineraries",
                column: "TourId");

            migrationBuilder.CreateIndex(
                name: "IX_TourTouristAttraction_ToursId",
                table: "TourTouristAttraction",
                column: "ToursId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tours_Users_PublishedById",
                table: "Tours",
                column: "PublishedById",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tours_Users_PublishedById",
                table: "Tours");

            migrationBuilder.DropTable(
                name: "TourItineraries");

            migrationBuilder.DropTable(
                name: "TourTouristAttraction");

            migrationBuilder.DropColumn(
                name: "CoverImageUrl",
                table: "Tours");

            migrationBuilder.DropColumn(
                name: "PublishedAt",
                table: "Tours");

            migrationBuilder.RenameColumn(
                name: "PublishedById",
                table: "Tours",
                newName: "TouristAttractionId");

            migrationBuilder.RenameIndex(
                name: "IX_Tours_PublishedById",
                table: "Tours",
                newName: "IX_Tours_TouristAttractionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tours_TouristAttractions_TouristAttractionId",
                table: "Tours",
                column: "TouristAttractionId",
                principalTable: "TouristAttractions",
                principalColumn: "Id");
        }
    }
}
