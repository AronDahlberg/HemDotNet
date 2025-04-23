using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HemDotNetWebApi.Migrations
{
    /// <inheritdoc />
    public partial class SeededDefaultUsersAndRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_RealEstateAgencies_RealEstateAgency",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<int>(
                name: "RealEstateAgency",
                table: "AspNetUsers",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "24f60ffc-3f16-4815-83b0-bf191748018c", null, "Administator", "Administator" },
                    { "c0ef899a-9033-4d98-9851-cf7c051cc51d", null, "User", "User" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "RealEstateAgency", "RealEstateAgentEmail", "RealEstateAgentFirstName", "RealEstateAgentId", "RealEstateAgentImageUrl", "RealEstateAgentLastName", "RealEstateAgentPhoneNumber", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "a7d64e4d-a8e6-40da-a431-e75fd59ecbdb", 0, "d8e8dca8-0eb6-41d5-b18f-33189ce94b14", "user@hemdotnet.se", true, false, null, "USER@HEMDOTNET.SE", "USER@HEMDOTNET.SE", "AQAAAAIAAYagAAAAEGSwuOcO2xQlCnNl5CT7bG6UGEtAIpmTPWmXd/hGaMx1jbMgZT7MCsIrA6LZM7GBJg==", null, false, null, "anna@nordichomes.com", "Anna", 0, "/images/RealEstateAgentWoman.jpg", "Svensson", "+46 70 123 45 67", "81199af2-392d-47a3-a10d-94715f273d97", false, "user@hemdotnet.se" },
                    { "bca74173-1e33-41e8-88df-5a6454c4f900", 0, "566faf97-1fb3-47a2-b80a-15f140500d58", "admin@hemdotnet.se", true, false, null, "ADMIN@HEMDOTNET.SE", "ADMIN@HEMDOTNET.SE", "AQAAAAIAAYagAAAAEL60/seSnvMJZDuzy71Ei2RIO4YXCNIm3oiz+54ZNBT6z9SJm6iAijUQQwerphQCxw==", null, false, null, "mikael@nordichomes.com", "Mikael", 0, "/images/RealEstateAgentMan.jpg", "Strand", "+46 70 123 45 67", "baffd57d-b3c6-4a6e-a2be-b0513e147922", false, "admin@hemdotnet.se" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "c0ef899a-9033-4d98-9851-cf7c051cc51d", "a7d64e4d-a8e6-40da-a431-e75fd59ecbdb" },
                    { "24f60ffc-3f16-4815-83b0-bf191748018c", "bca74173-1e33-41e8-88df-5a6454c4f900" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_RealEstateAgencies_RealEstateAgency",
                table: "AspNetUsers",
                column: "RealEstateAgency",
                principalTable: "RealEstateAgencies",
                principalColumn: "RealEstateAgencyId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_RealEstateAgencies_RealEstateAgency",
                table: "AspNetUsers");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "c0ef899a-9033-4d98-9851-cf7c051cc51d", "a7d64e4d-a8e6-40da-a431-e75fd59ecbdb" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "24f60ffc-3f16-4815-83b0-bf191748018c", "bca74173-1e33-41e8-88df-5a6454c4f900" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "24f60ffc-3f16-4815-83b0-bf191748018c");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c0ef899a-9033-4d98-9851-cf7c051cc51d");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a7d64e4d-a8e6-40da-a431-e75fd59ecbdb");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "bca74173-1e33-41e8-88df-5a6454c4f900");

            migrationBuilder.AlterColumn<int>(
                name: "RealEstateAgency",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_RealEstateAgencies_RealEstateAgency",
                table: "AspNetUsers",
                column: "RealEstateAgency",
                principalTable: "RealEstateAgencies",
                principalColumn: "RealEstateAgencyId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
