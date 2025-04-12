using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DukandaCore.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class addMigrations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Banner_BannerType_BannerTypeId",
                table: "Banner");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Banner",
                table: "Banner");

            migrationBuilder.DeleteData(
                table: "IdentityRole<Guid>",
                keyColumn: "Id",
                keyValue: new Guid("0df81c57-6bc4-4863-b327-48dac0d91dae"));

            migrationBuilder.DeleteData(
                table: "IdentityRole<Guid>",
                keyColumn: "Id",
                keyValue: new Guid("13bbd941-6da6-4173-9777-18847f7c8cc2"));

            migrationBuilder.DeleteData(
                table: "IdentityRole<Guid>",
                keyColumn: "Id",
                keyValue: new Guid("2fc86872-cd18-4051-86ab-a123df302995"));

            migrationBuilder.RenameTable(
                name: "Banner",
                newName: "Banners");

            migrationBuilder.RenameIndex(
                name: "IX_Banner_BannerTypeId",
                table: "Banners",
                newName: "IX_Banners_BannerTypeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Banners",
                table: "Banners",
                column: "Id");

            migrationBuilder.InsertData(
                table: "IdentityRole<Guid>",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("2792df5f-b796-4187-8ffa-53aace46e68d"), null, "TourAgency", "TOUR_AGENCY" },
                    { new Guid("4810f588-ba1d-4a41-9c98-dc860444e24c"), null, "Admin", "ADMIN" },
                    { new Guid("eff1933a-bddf-46b1-aca6-f57b478b5a07"), null, "Tourist", "TOURIST" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Banners_BannerType_BannerTypeId",
                table: "Banners",
                column: "BannerTypeId",
                principalTable: "BannerType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Banners_BannerType_BannerTypeId",
                table: "Banners");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Banners",
                table: "Banners");

            migrationBuilder.DeleteData(
                table: "IdentityRole<Guid>",
                keyColumn: "Id",
                keyValue: new Guid("2792df5f-b796-4187-8ffa-53aace46e68d"));

            migrationBuilder.DeleteData(
                table: "IdentityRole<Guid>",
                keyColumn: "Id",
                keyValue: new Guid("4810f588-ba1d-4a41-9c98-dc860444e24c"));

            migrationBuilder.DeleteData(
                table: "IdentityRole<Guid>",
                keyColumn: "Id",
                keyValue: new Guid("eff1933a-bddf-46b1-aca6-f57b478b5a07"));

            migrationBuilder.RenameTable(
                name: "Banners",
                newName: "Banner");

            migrationBuilder.RenameIndex(
                name: "IX_Banners_BannerTypeId",
                table: "Banner",
                newName: "IX_Banner_BannerTypeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Banner",
                table: "Banner",
                column: "Id");

            migrationBuilder.InsertData(
                table: "IdentityRole<Guid>",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("0df81c57-6bc4-4863-b327-48dac0d91dae"), null, "TourAgency", "TOUR_AGENCY" },
                    { new Guid("13bbd941-6da6-4173-9777-18847f7c8cc2"), null, "Admin", "ADMIN" },
                    { new Guid("2fc86872-cd18-4051-86ab-a123df302995"), null, "Tourist", "TOURIST" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Banner_BannerType_BannerTypeId",
                table: "Banner",
                column: "BannerTypeId",
                principalTable: "BannerType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
