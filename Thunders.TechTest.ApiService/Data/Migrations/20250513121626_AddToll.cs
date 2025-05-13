using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Thunders.TechTest.ApiService.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddToll : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TollConcessionaire",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CompanyName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    LegalDocument = table.Column<string>(type: "nchar(14)", fixedLength: true, maxLength: 14, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TollConcessionaire", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TollPlaza",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Highway = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Kms = table.Column<decimal>(type: "decimal(8,2)", precision: 8, scale: 2, nullable: false),
                    State = table.Column<string>(type: "nchar(2)", fixedLength: true, maxLength: 2, nullable: false),
                    City = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Latitude = table.Column<decimal>(type: "decimal(7,5)", precision: 7, scale: 5, nullable: false),
                    Longitude = table.Column<decimal>(type: "decimal(7,5)", precision: 7, scale: 5, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ConcessionaireId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TollPlaza", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TollPlaza_TollConcessionaire_ConcessionaireId",
                        column: x => x.ConcessionaireId,
                        principalTable: "TollConcessionaire",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TollPayment",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PaidAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(6,2)", precision: 6, scale: 2, nullable: false),
                    Vehicle = table.Column<int>(type: "int", nullable: false),
                    PlazaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TollPayment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TollPayment_TollPlaza_PlazaId",
                        column: x => x.PlazaId,
                        principalTable: "TollPlaza",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_TollPayment_PlazaId",
                table: "TollPayment",
                column: "PlazaId");

            migrationBuilder.CreateIndex(
                name: "IX_TollPlaza_ConcessionaireId",
                table: "TollPlaza",
                column: "ConcessionaireId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TollPayment");

            migrationBuilder.DropTable(
                name: "TollPlaza");

            migrationBuilder.DropTable(
                name: "TollConcessionaire");
        }
    }
}
