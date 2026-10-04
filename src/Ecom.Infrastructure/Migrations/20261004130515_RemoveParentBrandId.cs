using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ecom.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RemoveParentBrandId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Brands_Brands_ParentBrandId",
                table: "Brands");

            migrationBuilder.DropIndex(
                name: "IX_Brands_ParentBrandId",
                table: "Brands");

            migrationBuilder.DropColumn(
                name: "ParentBrandId",
                table: "Brands");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ParentBrandId",
                table: "Brands",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Brands_ParentBrandId",
                table: "Brands",
                column: "ParentBrandId");

            migrationBuilder.AddForeignKey(
                name: "FK_Brands_Brands_ParentBrandId",
                table: "Brands",
                column: "ParentBrandId",
                principalTable: "Brands",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
