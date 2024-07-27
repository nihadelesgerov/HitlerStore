using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HitlerStore.Migrations
{
    /// <inheritdoc />
    public partial class HitlerDbMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProductsTable",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SpeacialId = table.Column<long>(type: "bigint", nullable: false),
                    ProductName = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    ProductCategory = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductBio = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    ProductPrice = table.Column<double>(type: "float", nullable: false),
                    ProductImageName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductUploadedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductsTable", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductsTable");
        }
    }
}
