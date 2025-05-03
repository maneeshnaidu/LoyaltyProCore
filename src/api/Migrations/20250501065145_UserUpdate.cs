using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace api.Migrations
{
    /// <inheritdoc />
    public partial class UserUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1dd17b53-e620-4590-a810-9e49a64bdef5");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d32bb953-8b67-4c00-adff-1d229697a938");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f691cfc4-31ca-46b9-bc03-00dbaae0bad2");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fe3e9843-9363-4071-9b0d-e0a65b80a0df");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedOn",
                table: "Outlets",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "18ab1c1c-d4d8-415e-8038-ccbbba60649c", null, "SuperAdmin", "SUPERADMIN" },
                    { "51c0937e-76a9-42b8-b65d-fecebcfbd7c4", null, "Staff", "STAFF" },
                    { "9e4dc7d1-81e8-459e-b005-44d5a8904803", null, "Admin", "ADMIN" },
                    { "fdc59026-0fb6-4ea2-b67d-e0da3d5565bd", null, "User", "USER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "18ab1c1c-d4d8-415e-8038-ccbbba60649c");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "51c0937e-76a9-42b8-b65d-fecebcfbd7c4");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9e4dc7d1-81e8-459e-b005-44d5a8904803");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fdc59026-0fb6-4ea2-b67d-e0da3d5565bd");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedOn",
                table: "Outlets",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "1dd17b53-e620-4590-a810-9e49a64bdef5", null, "User", "USER" },
                    { "d32bb953-8b67-4c00-adff-1d229697a938", null, "SuperAdmin", "SUPERADMIN" },
                    { "f691cfc4-31ca-46b9-bc03-00dbaae0bad2", null, "Admin", "ADMIN" },
                    { "fe3e9843-9363-4071-9b0d-e0a65b80a0df", null, "Staff", "STAFF" }
                });
        }
    }
}
