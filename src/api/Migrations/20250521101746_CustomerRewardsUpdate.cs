using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace api.Migrations
{
    /// <inheritdoc />
    public partial class CustomerRewardsUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2375c89f-7857-4078-a96c-0bc616423c72");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4c2dbfe7-4cc2-4dda-8bcb-8134d7b24eac");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9cd12acf-5a09-4192-9c6b-66fc2b2c69f8");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fa4accf7-6a8f-4369-867a-999419413419");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "53e7a1a2-82eb-405a-83e3-5093f655f799", null, "Staff", "STAFF" },
                    { "919c85a4-f3f6-4cdf-b28d-6b56a745f7f6", null, "SuperAdmin", "SUPERADMIN" },
                    { "9e4e63c7-a6b8-46c6-857f-eb85a09cd4bd", null, "User", "USER" },
                    { "d6b8225f-54d4-4b63-98e9-84293d9fe281", null, "Admin", "ADMIN" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_CustomerRewards_RewardId",
                table: "CustomerRewards",
                column: "RewardId");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerRewards_Rewards_RewardId",
                table: "CustomerRewards",
                column: "RewardId",
                principalTable: "Rewards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomerRewards_Rewards_RewardId",
                table: "CustomerRewards");

            migrationBuilder.DropIndex(
                name: "IX_CustomerRewards_RewardId",
                table: "CustomerRewards");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "53e7a1a2-82eb-405a-83e3-5093f655f799");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "919c85a4-f3f6-4cdf-b28d-6b56a745f7f6");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9e4e63c7-a6b8-46c6-857f-eb85a09cd4bd");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d6b8225f-54d4-4b63-98e9-84293d9fe281");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "2375c89f-7857-4078-a96c-0bc616423c72", null, "User", "USER" },
                    { "4c2dbfe7-4cc2-4dda-8bcb-8134d7b24eac", null, "SuperAdmin", "SUPERADMIN" },
                    { "9cd12acf-5a09-4192-9c6b-66fc2b2c69f8", null, "Staff", "STAFF" },
                    { "fa4accf7-6a8f-4369-867a-999419413419", null, "Admin", "ADMIN" }
                });
        }
    }
}
