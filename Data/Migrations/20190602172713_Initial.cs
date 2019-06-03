using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false, defaultValueSql: "getdate()"),
                    Type = table.Column<int>(nullable: false),
                    Info = table.Column<string>(maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FarmBots",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false, defaultValueSql: "getdate()"),
                    Updated = table.Column<DateTime>(nullable: false, defaultValueSql: "getdate()"),
                    Name = table.Column<string>(maxLength: 75, nullable: true),
                    IpAddress = table.Column<string>(maxLength: 15, nullable: true),
                    IpCameraAddress = table.Column<string>(maxLength: 50, nullable: true),
                    LastX = table.Column<int>(nullable: false),
                    LastY = table.Column<int>(nullable: false),
                    Width = table.Column<int>(nullable: false),
                    Length = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FarmBots", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Plants",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false, defaultValueSql: "getdate()"),
                    Updated = table.Column<DateTime>(nullable: false, defaultValueSql: "getdate()"),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    Info = table.Column<string>(maxLength: 250, nullable: true),
                    RowDistance = table.Column<int>(nullable: false),
                    PlantDistance = table.Column<int>(nullable: false),
                    SeedDepth = table.Column<short>(nullable: false),
                    SoilHumidity = table.Column<byte>(nullable: false),
                    Duration = table.Column<short>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Plants", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Parameters",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false, defaultValueSql: "getdate()"),
                    FarmBotId = table.Column<Guid>(nullable: false),
                    Luminosity = table.Column<byte>(nullable: false),
                    AirTemperature = table.Column<byte>(nullable: false),
                    SoilHumidity = table.Column<byte>(nullable: false),
                    SeededPlants = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Parameters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Parameters_FarmBots_FarmBotId",
                        column: x => x.FarmBotId,
                        principalTable: "FarmBots",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FarmBotPlants",
                columns: table => new
                {
                    FarmBotId = table.Column<Guid>(nullable: false),
                    PlantId = table.Column<Guid>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    Updated = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FarmBotPlants", x => new { x.FarmBotId, x.PlantId });
                    table.ForeignKey(
                        name: "FK_FarmBotPlants_FarmBots_FarmBotId",
                        column: x => x.FarmBotId,
                        principalTable: "FarmBots",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FarmBotPlants_Plants_PlantId",
                        column: x => x.PlantId,
                        principalTable: "Plants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "FarmBots",
                columns: new[] { "Id", "IpAddress", "IpCameraAddress", "LastX", "LastY", "Length", "Name", "Width" },
                values: new object[] { new Guid("99d9742b-1ee2-45c9-a9fb-8742baa3bb86"), "192.168.1.112", "http://192.168.1.1:8080/video", 0, 0, 0, "Utm FarmBot", 0 });

            migrationBuilder.CreateIndex(
                name: "IX_FarmBotPlants_PlantId",
                table: "FarmBotPlants",
                column: "PlantId");

            migrationBuilder.CreateIndex(
                name: "IX_FarmBots_Name",
                table: "FarmBots",
                column: "Name",
                unique: true,
                filter: "[Name] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Parameters_FarmBotId",
                table: "Parameters",
                column: "FarmBotId");

            migrationBuilder.CreateIndex(
                name: "IX_Plants_Name",
                table: "Plants",
                column: "Name",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Events");

            migrationBuilder.DropTable(
                name: "FarmBotPlants");

            migrationBuilder.DropTable(
                name: "Parameters");

            migrationBuilder.DropTable(
                name: "Plants");

            migrationBuilder.DropTable(
                name: "FarmBots");
        }
    }
}
