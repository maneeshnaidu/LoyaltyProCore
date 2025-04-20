using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace api.Migrations
{
    /// <inheritdoc />
    public partial class VendorUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vendors_AspNetUsers_AdminId",
                table: "Vendors");

            migrationBuilder.DropIndex(
                name: "IX_Vendors_AdminId",
                table: "Vendors");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "34997ffb-ee92-45ed-8c83-85d3d28dff2c");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3d32dc2b-1cf8-4a9f-ba6e-d0388c7ac0af");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8330d038-f391-4b18-ae16-41ef55ad0d58");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "cee16247-3ea4-4f50-af57-4ec282064b0a");

            migrationBuilder.DropColumn(
                name: "AdminId",
                table: "Vendors");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "0cbcf455-a539-4991-b4b9-5d9a6c3baca5", null, "User", "USER" },
                    { "461efe22-49fe-4fd4-bc6b-d6c3f071724b", null, "Admin", "ADMIN" },
                    { "78c86abc-3422-4308-af50-747f3cd0a5f5", null, "Staff", "STAFF" },
                    { "cbe7a7ee-7c8e-483d-9e9c-64a13f89cf4c", null, "SuperAdmin", "SUPERADMIN" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_VendorId",
                table: "AspNetUsers",
                column: "VendorId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Vendors_VendorId",
                table: "AspNetUsers",
                column: "VendorId",
                principalTable: "Vendors",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Vendors_VendorId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_VendorId",
                table: "AspNetUsers");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0cbcf455-a539-4991-b4b9-5d9a6c3baca5");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "461efe22-49fe-4fd4-bc6b-d6c3f071724b");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "78c86abc-3422-4308-af50-747f3cd0a5f5");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "cbe7a7ee-7c8e-483d-9e9c-64a13f89cf4c");

            migrationBuilder.AddColumn<string>(
                name: "AdminId",
                table: "Vendors",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "34997ffb-ee92-45ed-8c83-85d3d28dff2c", null, "Admin", "ADMIN" },
                    { "3d32dc2b-1cf8-4a9f-ba6e-d0388c7ac0af", null, "User", "USER" },
                    { "8330d038-f391-4b18-ae16-41ef55ad0d58", null, "Staff", "STAFF" },
                    { "cee16247-3ea4-4f50-af57-4ec282064b0a", null, "SuperAdmin", "SUPERADMIN" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Vendors_AdminId",
                table: "Vendors",
                column: "AdminId");

            migrationBuilder.AddForeignKey(
                name: "FK_Vendors_AspNetUsers_AdminId",
                table: "Vendors",
                column: "AdminId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
