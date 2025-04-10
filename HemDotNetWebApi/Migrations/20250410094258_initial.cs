using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HemDotNetWebApi.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Municipalities",
                columns: table => new
                {
                    MunicipalityId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MunicipalityName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Municipalities", x => x.MunicipalityId);
                });

            migrationBuilder.CreateTable(
                name: "RealEstateAgencies",
                columns: table => new
                {
                    RealEstateAgencyId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RealEstateAgencyName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    RealEstateAgencyPresentation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RealEstateAgencyLogoUrl = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RealEstateAgencies", x => x.RealEstateAgencyId);
                });

            migrationBuilder.CreateTable(
                name: "RealEstateAgents",
                columns: table => new
                {
                    RealEstateAgentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RealEstateAgentFirstName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    RealEstateAgentLastName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    RealEstateAgentEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RealEstateAgentPhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RealEstateAgentImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RealEstateAgency = table.Column<int>(type: "int", nullable: false)
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

            migrationBuilder.CreateTable(
                name: "MarketProperties",
                columns: table => new
                {
                    MarketPropertyId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MunicipalityId = table.Column<int>(type: "int", nullable: false),
                    Category = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    LivingArea = table.Column<double>(type: "float", nullable: false),
                    AncillaryArea = table.Column<double>(type: "float", nullable: false),
                    LotArea = table.Column<double>(type: "float", nullable: false),
                    PropertyAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    AmountOfRooms = table.Column<int>(type: "int", nullable: false),
                    MonthlyFee = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    YearlyMaintenanceCost = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ContructionYear = table.Column<int>(type: "int", nullable: false),
                    RealEstateAgentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MarketProperties", x => x.MarketPropertyId);
                    table.ForeignKey(
                        name: "FK_MarketProperties_Municipalities_MunicipalityId",
                        column: x => x.MunicipalityId,
                        principalTable: "Municipalities",
                        principalColumn: "MunicipalityId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MarketProperties_RealEstateAgents_RealEstateAgentId",
                        column: x => x.RealEstateAgentId,
                        principalTable: "RealEstateAgents",
                        principalColumn: "RealEstateAgentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PropertyImages",
                columns: table => new
                {
                    PropertyImageId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MarketProperty = table.Column<int>(type: "int", nullable: false),
                    PropertyImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PropertyImages", x => x.PropertyImageId);
                    table.ForeignKey(
                        name: "FK_PropertyImages_MarketProperties_MarketProperty",
                        column: x => x.MarketProperty,
                        principalTable: "MarketProperties",
                        principalColumn: "MarketPropertyId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MarketProperties_MunicipalityId",
                table: "MarketProperties",
                column: "MunicipalityId");

            migrationBuilder.CreateIndex(
                name: "IX_MarketProperties_RealEstateAgentId",
                table: "MarketProperties",
                column: "RealEstateAgentId");

            migrationBuilder.CreateIndex(
                name: "IX_PropertyImages_MarketProperty",
                table: "PropertyImages",
                column: "MarketProperty");

            migrationBuilder.CreateIndex(
                name: "IX_RealEstateAgents_RealEstateAgency",
                table: "RealEstateAgents",
                column: "RealEstateAgency");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PropertyImages");

            migrationBuilder.DropTable(
                name: "MarketProperties");

            migrationBuilder.DropTable(
                name: "Municipalities");

            migrationBuilder.DropTable(
                name: "RealEstateAgents");

            migrationBuilder.DropTable(
                name: "RealEstateAgencies");
        }
    }
}
