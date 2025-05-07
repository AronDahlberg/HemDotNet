using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HemDotNetWebApi.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedSpellingErrorMarketProperty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ContructionYear",
                table: "MarketProperties",
                newName: "ConstructionYear");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ConstructionYear",
                table: "MarketProperties",
                newName: "ContructionYear");
        }
    }
}
