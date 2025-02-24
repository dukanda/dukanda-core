using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DukandaCore.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class addCityInTour : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tours_Cities_CityId",
                table: "Tours");

            migrationBuilder.AlterColumn<int>(
                name: "CityId",
                table: "Tours",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsFeatured",
                table: "Tours",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddForeignKey(
                name: "FK_Tours_Cities_CityId",
                table: "Tours",
                column: "CityId",
                principalTable: "Cities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tours_Cities_CityId",
                table: "Tours");

            migrationBuilder.DropColumn(
                name: "IsFeatured",
                table: "Tours");

            migrationBuilder.AlterColumn<int>(
                name: "CityId",
                table: "Tours",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_Tours_Cities_CityId",
                table: "Tours",
                column: "CityId",
                principalTable: "Cities",
                principalColumn: "Id");
        }
    }
}
