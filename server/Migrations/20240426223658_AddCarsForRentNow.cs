using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace server.Migrations
{
    /// <inheritdoc />
    public partial class AddCarsForRentNow : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CarsForRent",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BrandId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ModelId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CarYearId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    PriceId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Test = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarsForRent", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CarsForRent_Brand_BrandId",
                        column: x => x.BrandId,
                        principalTable: "Brand",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CarsForRent_CarYear_CarYearId",
                        column: x => x.CarYearId,
                        principalTable: "CarYear",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CarsForRent_Models_ModelId",
                        column: x => x.ModelId,
                        principalTable: "Models",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CarsForRent_Price_PriceId",
                        column: x => x.PriceId,
                        principalTable: "Price",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_CarsForRent_BrandId",
                table: "CarsForRent",
                column: "BrandId");

            migrationBuilder.CreateIndex(
                name: "IX_CarsForRent_CarYearId",
                table: "CarsForRent",
                column: "CarYearId");

            migrationBuilder.CreateIndex(
                name: "IX_CarsForRent_ModelId",
                table: "CarsForRent",
                column: "ModelId");

            migrationBuilder.CreateIndex(
                name: "IX_CarsForRent_PriceId",
                table: "CarsForRent",
                column: "PriceId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CarsForRent");
        }
    }
}
