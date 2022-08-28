using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Protein.Tech.TCMB.Infrastructure.Persistence.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Currencies",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Currencies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Rates",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Rate = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CurrencyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Changes = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    LastUpdated = table.Column<DateTime>(type: "datetime", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Rates_Currencies_CurrencyId",
                        column: x => x.CurrencyId,
                        principalTable: "Currencies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Currencies",
                columns: new[] { "Id", "Code", "CreateDate" },
                values: new object[,]
                {
                    { new Guid("d9eefed9-ead1-47e9-b045-a3357be50128"), "USD", new DateTime(2022, 8, 27, 19, 52, 56, 163, DateTimeKind.Local).AddTicks(6348) },
                    { new Guid("44223ea3-1955-4e38-bbe5-4c918f643255"), "EUR", new DateTime(2022, 8, 27, 19, 52, 56, 165, DateTimeKind.Local).AddTicks(316) },
                    { new Guid("75d1a8c3-a054-4a1a-857e-aef623ba8644"), "GBP", new DateTime(2022, 8, 27, 19, 52, 56, 165, DateTimeKind.Local).AddTicks(349) },
                    { new Guid("2a16f857-5b8a-445c-9013-705553367267"), "CHF", new DateTime(2022, 8, 27, 19, 52, 56, 165, DateTimeKind.Local).AddTicks(355) },
                    { new Guid("e55ff64e-558c-4b79-85c8-1ebdded0806f"), "KWD", new DateTime(2022, 8, 27, 19, 52, 56, 165, DateTimeKind.Local).AddTicks(360) },
                    { new Guid("632ee9f7-3633-4333-95a7-36fd395758d7"), "SAR", new DateTime(2022, 8, 27, 19, 52, 56, 165, DateTimeKind.Local).AddTicks(365) },
                    { new Guid("116251bf-8993-45cc-9ab8-91e6a28340b6"), "RUB", new DateTime(2022, 8, 27, 19, 52, 56, 165, DateTimeKind.Local).AddTicks(392) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Rates_CurrencyId",
                table: "Rates",
                column: "CurrencyId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Rates");

            migrationBuilder.DropTable(
                name: "Currencies");
        }
    }
}
