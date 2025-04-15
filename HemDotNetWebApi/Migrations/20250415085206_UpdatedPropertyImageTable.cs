using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HemDotNetWebApi.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedPropertyImageTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //Author: Johan Ek
            migrationBuilder.AddColumn<int>(
                name: "MarketPropertyId",
                table: "PropertyImages",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_PropertyImages_MarketPropertyId",
                table: "PropertyImages",
                column: "MarketPropertyId");

            migrationBuilder.AddForeignKey(
                name: "FK_PropertyImages_MarketProperties_MarketPropertyId",
                table: "PropertyImages",
                column: "MarketPropertyId",
                principalTable: "MarketProperties",
                principalColumn: "MarketPropertyId",
                //Cannot use Cascading Delete here, because EF hates multiple cascade paths.
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.DropForeignKey(
                name: "FK_PropertyImages_MarketProperties_MarketProperty",
                table: "PropertyImages");

            migrationBuilder.DropIndex(
                name: "IX_PropertyImages_MarketProperty",
                table: "PropertyImages");

            migrationBuilder.DropColumn(
                name: "MarketProperty",
                table: "PropertyImages");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
