using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace api.Migrations
{
    /// <inheritdoc />
    public partial class DateUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "RedeemedOn",
                table: "CustomerRewards",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "CustomerRewards",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "ExpiryDate",
                table: "CustomerRewards",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "ExternalId",
                table: "CustomerRewards",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OutletId",
                table: "CustomerRewards",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "VendorId",
                table: "CustomerRewards",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "CustomerRewards");

            migrationBuilder.DropColumn(
                name: "ExpiryDate",
                table: "CustomerRewards");

            migrationBuilder.DropColumn(
                name: "ExternalId",
                table: "CustomerRewards");

            migrationBuilder.DropColumn(
                name: "OutletId",
                table: "CustomerRewards");

            migrationBuilder.DropColumn(
                name: "VendorId",
                table: "CustomerRewards");

            migrationBuilder.AlterColumn<DateTime>(
                name: "RedeemedOn",
                table: "CustomerRewards",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);
        }
    }
}
