using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace api.Migrations
{
    /// <inheritdoc />
    public partial class TokenUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2d7fc7fd-e4ba-4a75-98b7-60e6bada9960");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "841d2be2-ab69-435b-b7df-1ca50c2b7fbb");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "aaf8cb96-e308-4cd3-a7c6-8f7fcc29627a");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "eb03eb60-7b85-4201-9757-dab072a56d05");

            migrationBuilder.AddColumn<string>(
                name: "RefreshToken",
                table: "AspNetUsers",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "RefreshTokenExpiryTime",
                table: "AspNetUsers",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "154023f4-7c3e-4076-83b5-090e0a5abff6", null, "SuperAdmin", "SUPERADMIN" },
                    { "3259a927-2f95-4e00-bd29-77430cf3c6c4", null, "Staff", "STAFF" },
                    { "6ddb101c-76aa-4e57-9878-49522e1035c1", null, "User", "USER" },
                    { "d856f8f5-1b64-48df-966d-05420d0d19fd", null, "Admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "154023f4-7c3e-4076-83b5-090e0a5abff6");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3259a927-2f95-4e00-bd29-77430cf3c6c4");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6ddb101c-76aa-4e57-9878-49522e1035c1");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d856f8f5-1b64-48df-966d-05420d0d19fd");

            migrationBuilder.DropColumn(
                name: "RefreshToken",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "RefreshTokenExpiryTime",
                table: "AspNetUsers");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "2d7fc7fd-e4ba-4a75-98b7-60e6bada9960", null, "User", "USER" },
                    { "841d2be2-ab69-435b-b7df-1ca50c2b7fbb", null, "Staff", "STAFF" },
                    { "aaf8cb96-e308-4cd3-a7c6-8f7fcc29627a", null, "SuperAdmin", "SUPERADMIN" },
                    { "eb03eb60-7b85-4201-9757-dab072a56d05", null, "Admin", "ADMIN" }
                });
        }
    }
}
