using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HemDotNetWebApi.Migrations
{
    /// <inheritdoc />
    public partial class ExtendedRealEstateAgentTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MarketProperties_RealEstateAgents_RealEstateAgentId",
                table: "MarketProperties");

            migrationBuilder.DropTable(
                name: "RealEstateAgents");

            migrationBuilder.AlterColumn<string>(
                name: "RealEstateAgentId",
                table: "MarketProperties",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "RealEstateAgency",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "RealEstateAgentEmail",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "RealEstateAgentFirstName",
                table: "AspNetUsers",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "RealEstateAgentId",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "RealEstateAgentImageUrl",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "RealEstateAgentLastName",
                table: "AspNetUsers",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "RealEstateAgentPhoneNumber",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_RealEstateAgency",
                table: "AspNetUsers",
                column: "RealEstateAgency");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_RealEstateAgencies_RealEstateAgency",
                table: "AspNetUsers",
                column: "RealEstateAgency",
                principalTable: "RealEstateAgencies",
                principalColumn: "RealEstateAgencyId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MarketProperties_AspNetUsers_RealEstateAgentId",
                table: "MarketProperties",
                column: "RealEstateAgentId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_RealEstateAgencies_RealEstateAgency",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_MarketProperties_AspNetUsers_RealEstateAgentId",
                table: "MarketProperties");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_RealEstateAgency",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "RealEstateAgency",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "RealEstateAgentEmail",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "RealEstateAgentFirstName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "RealEstateAgentId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "RealEstateAgentImageUrl",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "RealEstateAgentLastName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "RealEstateAgentPhoneNumber",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<int>(
                name: "RealEstateAgentId",
                table: "MarketProperties",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "RealEstateAgents",
                columns: table => new
                {
                    RealEstateAgentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RealEstateAgency = table.Column<int>(type: "int", nullable: false),
                    RealEstateAgentEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RealEstateAgentFirstName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    RealEstateAgentImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RealEstateAgentLastName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    RealEstateAgentPhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RealEstateAgents", x => x.RealEstateAgentId);
                    table.ForeignKey(
                        name: "FK_RealEstateAgents_RealEstateAgencies_RealEstateAgency",
                        column: x => x.RealEstateAgency,
                        principalTable: "RealEstateAgencies",
                        principalColumn: "RealEstateAgencyId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RealEstateAgents_RealEstateAgency",
                table: "RealEstateAgents",
                column: "RealEstateAgency");

            migrationBuilder.AddForeignKey(
                name: "FK_MarketProperties_RealEstateAgents_RealEstateAgentId",
                table: "MarketProperties",
                column: "RealEstateAgentId",
                principalTable: "RealEstateAgents",
                principalColumn: "RealEstateAgentId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
