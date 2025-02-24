using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DukandaCore.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class adjustModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AttractionImage_TouristAttractions_TouristAttractionId",
                table: "AttractionImage");

            migrationBuilder.DropForeignKey(
                name: "FK_Benefit_Package_PackageId",
                table: "Benefit");

            migrationBuilder.DropForeignKey(
                name: "FK_Booking_BookingStatus_BookingStatusId",
                table: "Booking");

            migrationBuilder.DropForeignKey(
                name: "FK_Booking_Package_PackageId",
                table: "Booking");

            migrationBuilder.DropForeignKey(
                name: "FK_Booking_Users_UserId",
                table: "Booking");

            migrationBuilder.DropForeignKey(
                name: "FK_Package_Tour_TourId",
                table: "Package");

            migrationBuilder.DropForeignKey(
                name: "FK_Review_Tour_TourId",
                table: "Review");

            migrationBuilder.DropForeignKey(
                name: "FK_Tour_Cities_CityId",
                table: "Tour");

            migrationBuilder.DropForeignKey(
                name: "FK_Tour_TourAgency_AgencyId",
                table: "Tour");

            migrationBuilder.DropForeignKey(
                name: "FK_Tour_TouristAttractions_TouristAttractionId",
                table: "Tour");

            migrationBuilder.DropForeignKey(
                name: "FK_TourAgency_TourAgencyType_TourAgencyTypeId",
                table: "TourAgency");

            migrationBuilder.DropForeignKey(
                name: "FK_TourAgency_Users_UserId",
                table: "TourAgency");

            migrationBuilder.DropForeignKey(
                name: "FK_TourTourType_TourType_TourTypesId",
                table: "TourTourType");

            migrationBuilder.DropForeignKey(
                name: "FK_TourTourType_Tour_ToursId",
                table: "TourTourType");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TourType",
                table: "TourType");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TourAgencyType",
                table: "TourAgencyType");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TourAgency",
                table: "TourAgency");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tour",
                table: "Tour");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Package",
                table: "Package");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Booking",
                table: "Booking");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Benefit",
                table: "Benefit");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AttractionImage",
                table: "AttractionImage");

            migrationBuilder.RenameTable(
                name: "TourType",
                newName: "TourTypes");

            migrationBuilder.RenameTable(
                name: "TourAgencyType",
                newName: "AgencyTypes");

            migrationBuilder.RenameTable(
                name: "TourAgency",
                newName: "TourAgencies");

            migrationBuilder.RenameTable(
                name: "Tour",
                newName: "Tours");

            migrationBuilder.RenameTable(
                name: "Package",
                newName: "Packages");

            migrationBuilder.RenameTable(
                name: "Booking",
                newName: "Bookings");

            migrationBuilder.RenameTable(
                name: "Benefit",
                newName: "Benefits");

            migrationBuilder.RenameTable(
                name: "AttractionImage",
                newName: "AttractionImages");

            migrationBuilder.RenameIndex(
                name: "IX_TourAgency_TourAgencyTypeId",
                table: "TourAgencies",
                newName: "IX_TourAgencies_TourAgencyTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_Tour_TouristAttractionId",
                table: "Tours",
                newName: "IX_Tours_TouristAttractionId");

            migrationBuilder.RenameIndex(
                name: "IX_Tour_CityId",
                table: "Tours",
                newName: "IX_Tours_CityId");

            migrationBuilder.RenameIndex(
                name: "IX_Tour_AgencyId",
                table: "Tours",
                newName: "IX_Tours_AgencyId");

            migrationBuilder.RenameIndex(
                name: "IX_Package_TourId",
                table: "Packages",
                newName: "IX_Packages_TourId");

            migrationBuilder.RenameIndex(
                name: "IX_Booking_UserId",
                table: "Bookings",
                newName: "IX_Bookings_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Booking_PackageId",
                table: "Bookings",
                newName: "IX_Bookings_PackageId");

            migrationBuilder.RenameIndex(
                name: "IX_Booking_BookingStatusId",
                table: "Bookings",
                newName: "IX_Bookings_BookingStatusId");

            migrationBuilder.RenameIndex(
                name: "IX_Benefit_PackageId",
                table: "Benefits",
                newName: "IX_Benefits_PackageId");

            migrationBuilder.RenameIndex(
                name: "IX_AttractionImage_TouristAttractionId",
                table: "AttractionImages",
                newName: "IX_AttractionImages_TouristAttractionId");

            migrationBuilder.AlterColumn<string>(
                name: "AvatarUrl",
                table: "Users",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TourTypes",
                table: "TourTypes",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AgencyTypes",
                table: "AgencyTypes",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TourAgencies",
                table: "TourAgencies",
                column: "UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tours",
                table: "Tours",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Packages",
                table: "Packages",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Bookings",
                table: "Bookings",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Benefits",
                table: "Benefits",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AttractionImages",
                table: "AttractionImages",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AttractionImages_TouristAttractions_TouristAttractionId",
                table: "AttractionImages",
                column: "TouristAttractionId",
                principalTable: "TouristAttractions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Benefits_Packages_PackageId",
                table: "Benefits",
                column: "PackageId",
                principalTable: "Packages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_BookingStatus_BookingStatusId",
                table: "Bookings",
                column: "BookingStatusId",
                principalTable: "BookingStatus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_Packages_PackageId",
                table: "Bookings",
                column: "PackageId",
                principalTable: "Packages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_Users_UserId",
                table: "Bookings",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Packages_Tours_TourId",
                table: "Packages",
                column: "TourId",
                principalTable: "Tours",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Review_Tours_TourId",
                table: "Review",
                column: "TourId",
                principalTable: "Tours",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TourAgencies_AgencyTypes_TourAgencyTypeId",
                table: "TourAgencies",
                column: "TourAgencyTypeId",
                principalTable: "AgencyTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TourAgencies_Users_UserId",
                table: "TourAgencies",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tours_Cities_CityId",
                table: "Tours",
                column: "CityId",
                principalTable: "Cities",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Tours_TourAgencies_AgencyId",
                table: "Tours",
                column: "AgencyId",
                principalTable: "TourAgencies",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tours_TouristAttractions_TouristAttractionId",
                table: "Tours",
                column: "TouristAttractionId",
                principalTable: "TouristAttractions",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TourTourType_TourTypes_TourTypesId",
                table: "TourTourType",
                column: "TourTypesId",
                principalTable: "TourTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TourTourType_Tours_ToursId",
                table: "TourTourType",
                column: "ToursId",
                principalTable: "Tours",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AttractionImages_TouristAttractions_TouristAttractionId",
                table: "AttractionImages");

            migrationBuilder.DropForeignKey(
                name: "FK_Benefits_Packages_PackageId",
                table: "Benefits");

            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_BookingStatus_BookingStatusId",
                table: "Bookings");

            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_Packages_PackageId",
                table: "Bookings");

            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_Users_UserId",
                table: "Bookings");

            migrationBuilder.DropForeignKey(
                name: "FK_Packages_Tours_TourId",
                table: "Packages");

            migrationBuilder.DropForeignKey(
                name: "FK_Review_Tours_TourId",
                table: "Review");

            migrationBuilder.DropForeignKey(
                name: "FK_TourAgencies_AgencyTypes_TourAgencyTypeId",
                table: "TourAgencies");

            migrationBuilder.DropForeignKey(
                name: "FK_TourAgencies_Users_UserId",
                table: "TourAgencies");

            migrationBuilder.DropForeignKey(
                name: "FK_Tours_Cities_CityId",
                table: "Tours");

            migrationBuilder.DropForeignKey(
                name: "FK_Tours_TourAgencies_AgencyId",
                table: "Tours");

            migrationBuilder.DropForeignKey(
                name: "FK_Tours_TouristAttractions_TouristAttractionId",
                table: "Tours");

            migrationBuilder.DropForeignKey(
                name: "FK_TourTourType_TourTypes_TourTypesId",
                table: "TourTourType");

            migrationBuilder.DropForeignKey(
                name: "FK_TourTourType_Tours_ToursId",
                table: "TourTourType");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TourTypes",
                table: "TourTypes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tours",
                table: "Tours");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TourAgencies",
                table: "TourAgencies");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Packages",
                table: "Packages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Bookings",
                table: "Bookings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Benefits",
                table: "Benefits");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AttractionImages",
                table: "AttractionImages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AgencyTypes",
                table: "AgencyTypes");

            migrationBuilder.RenameTable(
                name: "TourTypes",
                newName: "TourType");

            migrationBuilder.RenameTable(
                name: "Tours",
                newName: "Tour");

            migrationBuilder.RenameTable(
                name: "TourAgencies",
                newName: "TourAgency");

            migrationBuilder.RenameTable(
                name: "Packages",
                newName: "Package");

            migrationBuilder.RenameTable(
                name: "Bookings",
                newName: "Booking");

            migrationBuilder.RenameTable(
                name: "Benefits",
                newName: "Benefit");

            migrationBuilder.RenameTable(
                name: "AttractionImages",
                newName: "AttractionImage");

            migrationBuilder.RenameTable(
                name: "AgencyTypes",
                newName: "TourAgencyType");

            migrationBuilder.RenameIndex(
                name: "IX_Tours_TouristAttractionId",
                table: "Tour",
                newName: "IX_Tour_TouristAttractionId");

            migrationBuilder.RenameIndex(
                name: "IX_Tours_CityId",
                table: "Tour",
                newName: "IX_Tour_CityId");

            migrationBuilder.RenameIndex(
                name: "IX_Tours_AgencyId",
                table: "Tour",
                newName: "IX_Tour_AgencyId");

            migrationBuilder.RenameIndex(
                name: "IX_TourAgencies_TourAgencyTypeId",
                table: "TourAgency",
                newName: "IX_TourAgency_TourAgencyTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_Packages_TourId",
                table: "Package",
                newName: "IX_Package_TourId");

            migrationBuilder.RenameIndex(
                name: "IX_Bookings_UserId",
                table: "Booking",
                newName: "IX_Booking_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Bookings_PackageId",
                table: "Booking",
                newName: "IX_Booking_PackageId");

            migrationBuilder.RenameIndex(
                name: "IX_Bookings_BookingStatusId",
                table: "Booking",
                newName: "IX_Booking_BookingStatusId");

            migrationBuilder.RenameIndex(
                name: "IX_Benefits_PackageId",
                table: "Benefit",
                newName: "IX_Benefit_PackageId");

            migrationBuilder.RenameIndex(
                name: "IX_AttractionImages_TouristAttractionId",
                table: "AttractionImage",
                newName: "IX_AttractionImage_TouristAttractionId");

            migrationBuilder.AlterColumn<string>(
                name: "AvatarUrl",
                table: "Users",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_TourType",
                table: "TourType",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tour",
                table: "Tour",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TourAgency",
                table: "TourAgency",
                column: "UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Package",
                table: "Package",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Booking",
                table: "Booking",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Benefit",
                table: "Benefit",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AttractionImage",
                table: "AttractionImage",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TourAgencyType",
                table: "TourAgencyType",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AttractionImage_TouristAttractions_TouristAttractionId",
                table: "AttractionImage",
                column: "TouristAttractionId",
                principalTable: "TouristAttractions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Benefit_Package_PackageId",
                table: "Benefit",
                column: "PackageId",
                principalTable: "Package",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Booking_BookingStatus_BookingStatusId",
                table: "Booking",
                column: "BookingStatusId",
                principalTable: "BookingStatus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Booking_Package_PackageId",
                table: "Booking",
                column: "PackageId",
                principalTable: "Package",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Booking_Users_UserId",
                table: "Booking",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Package_Tour_TourId",
                table: "Package",
                column: "TourId",
                principalTable: "Tour",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Review_Tour_TourId",
                table: "Review",
                column: "TourId",
                principalTable: "Tour",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Tour_Cities_CityId",
                table: "Tour",
                column: "CityId",
                principalTable: "Cities",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Tour_TourAgency_AgencyId",
                table: "Tour",
                column: "AgencyId",
                principalTable: "TourAgency",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tour_TouristAttractions_TouristAttractionId",
                table: "Tour",
                column: "TouristAttractionId",
                principalTable: "TouristAttractions",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TourAgency_TourAgencyType_TourAgencyTypeId",
                table: "TourAgency",
                column: "TourAgencyTypeId",
                principalTable: "TourAgencyType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TourAgency_Users_UserId",
                table: "TourAgency",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TourTourType_TourType_TourTypesId",
                table: "TourTourType",
                column: "TourTypesId",
                principalTable: "TourType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TourTourType_Tour_ToursId",
                table: "TourTourType",
                column: "ToursId",
                principalTable: "Tour",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
