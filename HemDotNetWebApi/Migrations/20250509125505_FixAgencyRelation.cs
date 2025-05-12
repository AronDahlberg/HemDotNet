using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HemDotNetWebApi.Migrations
{
    /// <inheritdoc />
    public partial class FixAgencyRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_RealEstateAgencies_RealEstateAgency",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_RealEstateAgency",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "RealEstateAgency",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<int>(
                name: "RealEstateAgentAgencyId",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_RealEstateAgentAgencyId",
                table: "AspNetUsers",
                column: "RealEstateAgentAgencyId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_RealEstateAgencies_RealEstateAgentAgencyId",
                table: "AspNetUsers",
                column: "RealEstateAgentAgencyId",
                principalTable: "RealEstateAgencies",
                principalColumn: "RealEstateAgencyId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_RealEstateAgencies_RealEstateAgentAgencyId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_RealEstateAgentAgencyId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "RealEstateAgentAgencyId",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<int>(
                name: "RealEstateAgency",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_RealEstateAgency",
                table: "AspNetUsers",
                column: "RealEstateAgency");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_RealEstateAgencies_RealEstateAgency",
                table: "AspNetUsers",
                column: "RealEstateAgency",
                principalTable: "RealEstateAgencies",
                principalColumn: "RealEstateAgencyId");
        }
    }
}
