using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ecom.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddProductRichFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<List<string>>(
                name: "Images",
                table: "ProductVariants",
                type: "text[]",
                nullable: false);

            migrationBuilder.AddColumn<string>(
                name: "OptionName",
                table: "ProductVariants",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OptionValue",
                table: "ProductVariants",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<List<string>>(
                name: "Highlights",
                table: "Products",
                type: "text[]",
                nullable: false);

            migrationBuilder.AddColumn<List<string>>(
                name: "Images",
                table: "Products",
                type: "text[]",
                nullable: false);

            migrationBuilder.AddColumn<string>(
                name: "Specifications",
                table: "Products",
                type: "jsonb",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Images",
                table: "ProductVariants");

            migrationBuilder.DropColumn(
                name: "OptionName",
                table: "ProductVariants");

            migrationBuilder.DropColumn(
                name: "OptionValue",
                table: "ProductVariants");

            migrationBuilder.DropColumn(
                name: "Highlights",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Images",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Specifications",
                table: "Products");
        }
    }
}
