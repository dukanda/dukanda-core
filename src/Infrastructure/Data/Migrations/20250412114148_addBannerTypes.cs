using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DukandaCore.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class addBannerTypes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Banners_BannerType_BannerTypeId",
                table: "Banners");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BannerType",
                table: "BannerType");

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
                name: "BannerType",
                newName: "BannerTypes");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BannerTypes",
                table: "BannerTypes",
                column: "Id");

            migrationBuilder.InsertData(
                table: "IdentityRole<Guid>",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("003afa0a-b7fd-45fe-a78a-b39599586b0b"), null, "TourAgency", "TOUR_AGENCY" },
                    { new Guid("12df24c0-ab53-4907-b761-5f5349897ca6"), null, "Admin", "ADMIN" },
                    { new Guid("58a13939-4b99-4752-a750-51b4e69437dd"), null, "Tourist", "TOURIST" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Banners_BannerTypes_BannerTypeId",
                table: "Banners",
                column: "BannerTypeId",
                principalTable: "BannerTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Banners_BannerTypes_BannerTypeId",
                table: "Banners");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BannerTypes",
                table: "BannerTypes");

            migrationBuilder.DeleteData(
                table: "IdentityRole<Guid>",
                keyColumn: "Id",
                keyValue: new Guid("003afa0a-b7fd-45fe-a78a-b39599586b0b"));

            migrationBuilder.DeleteData(
                table: "IdentityRole<Guid>",
                keyColumn: "Id",
                keyValue: new Guid("12df24c0-ab53-4907-b761-5f5349897ca6"));

            migrationBuilder.DeleteData(
                table: "IdentityRole<Guid>",
                keyColumn: "Id",
                keyValue: new Guid("58a13939-4b99-4752-a750-51b4e69437dd"));

            migrationBuilder.RenameTable(
                name: "BannerTypes",
                newName: "BannerType");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BannerType",
                table: "BannerType",
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
    }
}
