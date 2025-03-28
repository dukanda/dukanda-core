using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DukandaCore.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class initialDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AttractionGallery_TouristAttractions_TouristAttractionId",
                table: "AttractionGallery");

            migrationBuilder.DropForeignKey(
                name: "FK_NewsGallery_News_NewsId",
                table: "NewsGallery");

            migrationBuilder.DropForeignKey(
                name: "FK_TourGallerys_Tours_TourId",
                table: "TourGallerys");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TourGallerys",
                table: "TourGallerys");

            migrationBuilder.DropPrimaryKey(
                name: "PK_NewsGallery",
                table: "NewsGallery");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AttractionGallery",
                table: "AttractionGallery");

            migrationBuilder.DeleteData(
                table: "IdentityRole<Guid>",
                keyColumn: "Id",
                keyValue: new Guid("08f3537b-f7d8-446a-b543-c0a91349f632"));

            migrationBuilder.DeleteData(
                table: "IdentityRole<Guid>",
                keyColumn: "Id",
                keyValue: new Guid("7f44c461-e928-4bc8-aef5-90f1de8ca6d9"));

            migrationBuilder.DeleteData(
                table: "IdentityRole<Guid>",
                keyColumn: "Id",
                keyValue: new Guid("921f7ff7-d173-401e-be3a-de4cfee27c61"));

            migrationBuilder.RenameTable(
                name: "TourGallerys",
                newName: "TourGalleries");

            migrationBuilder.RenameTable(
                name: "NewsGallery",
                newName: "NewsGalleries");

            migrationBuilder.RenameTable(
                name: "AttractionGallery",
                newName: "AttractionGalleries");

            migrationBuilder.RenameIndex(
                name: "IX_TourGallerys_TourId",
                table: "TourGalleries",
                newName: "IX_TourGalleries_TourId");

            migrationBuilder.RenameIndex(
                name: "IX_NewsGallery_NewsId",
                table: "NewsGalleries",
                newName: "IX_NewsGalleries_NewsId");

            migrationBuilder.RenameIndex(
                name: "IX_AttractionGallery_TouristAttractionId",
                table: "AttractionGalleries",
                newName: "IX_AttractionGalleries_TouristAttractionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TourGalleries",
                table: "TourGalleries",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_NewsGalleries",
                table: "NewsGalleries",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AttractionGalleries",
                table: "AttractionGalleries",
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
                name: "FK_AttractionGalleries_TouristAttractions_TouristAttractionId",
                table: "AttractionGalleries",
                column: "TouristAttractionId",
                principalTable: "TouristAttractions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_NewsGalleries_News_NewsId",
                table: "NewsGalleries",
                column: "NewsId",
                principalTable: "News",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TourGalleries_Tours_TourId",
                table: "TourGalleries",
                column: "TourId",
                principalTable: "Tours",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AttractionGalleries_TouristAttractions_TouristAttractionId",
                table: "AttractionGalleries");

            migrationBuilder.DropForeignKey(
                name: "FK_NewsGalleries_News_NewsId",
                table: "NewsGalleries");

            migrationBuilder.DropForeignKey(
                name: "FK_TourGalleries_Tours_TourId",
                table: "TourGalleries");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TourGalleries",
                table: "TourGalleries");

            migrationBuilder.DropPrimaryKey(
                name: "PK_NewsGalleries",
                table: "NewsGalleries");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AttractionGalleries",
                table: "AttractionGalleries");

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
                name: "TourGalleries",
                newName: "TourGallerys");

            migrationBuilder.RenameTable(
                name: "NewsGalleries",
                newName: "NewsGallery");

            migrationBuilder.RenameTable(
                name: "AttractionGalleries",
                newName: "AttractionGallery");

            migrationBuilder.RenameIndex(
                name: "IX_TourGalleries_TourId",
                table: "TourGallerys",
                newName: "IX_TourGallerys_TourId");

            migrationBuilder.RenameIndex(
                name: "IX_NewsGalleries_NewsId",
                table: "NewsGallery",
                newName: "IX_NewsGallery_NewsId");

            migrationBuilder.RenameIndex(
                name: "IX_AttractionGalleries_TouristAttractionId",
                table: "AttractionGallery",
                newName: "IX_AttractionGallery_TouristAttractionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TourGallerys",
                table: "TourGallerys",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_NewsGallery",
                table: "NewsGallery",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AttractionGallery",
                table: "AttractionGallery",
                column: "Id");

            migrationBuilder.InsertData(
                table: "IdentityRole<Guid>",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("08f3537b-f7d8-446a-b543-c0a91349f632"), null, "Admin", "ADMIN" },
                    { new Guid("7f44c461-e928-4bc8-aef5-90f1de8ca6d9"), null, "Tourist", "TOURIST" },
                    { new Guid("921f7ff7-d173-401e-be3a-de4cfee27c61"), null, "TourAgency", "TOUR_AGENCY" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_AttractionGallery_TouristAttractions_TouristAttractionId",
                table: "AttractionGallery",
                column: "TouristAttractionId",
                principalTable: "TouristAttractions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_NewsGallery_News_NewsId",
                table: "NewsGallery",
                column: "NewsId",
                principalTable: "News",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TourGallerys_Tours_TourId",
                table: "TourGallerys",
                column: "TourId",
                principalTable: "Tours",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
