using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IRFestival.Api.Migrations
{
    public partial class InitialCommit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Festivals",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Festivals", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Artists",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FestivalId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImagePath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Website = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Artists", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Artists_Festivals_FestivalId",
                        column: x => x.FestivalId,
                        principalTable: "Festivals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Schedules",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FestivalId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Schedules", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Schedules_Festivals_FestivalId",
                        column: x => x.FestivalId,
                        principalTable: "Festivals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Stages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FestivalId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Stages_Festivals_FestivalId",
                        column: x => x.FestivalId,
                        principalTable: "Festivals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ScheduleItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ScheduleId = table.Column<int>(type: "int", nullable: true),
                    ArtistId = table.Column<int>(type: "int", nullable: true),
                    StageId = table.Column<int>(type: "int", nullable: true),
                    Time = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsFavorite = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScheduleItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ScheduleItems_Artists_ArtistId",
                        column: x => x.ArtistId,
                        principalTable: "Artists",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ScheduleItems_Schedules_ScheduleId",
                        column: x => x.ScheduleId,
                        principalTable: "Schedules",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ScheduleItems_Stages_StageId",
                        column: x => x.StageId,
                        principalTable: "Stages",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Festivals",
                column: "Id",
                value: 1);

            migrationBuilder.InsertData(
                table: "Artists",
                columns: new[] { "Id", "FestivalId", "ImagePath", "Name", "Website" },
                values: new object[,]
                {
                    { 1, 1, "dianaross.jpg", "Diana Ross", "http://www.dianaross.co.uk/indexb.html" },
                    { 2, 1, "thecommodores.jpg", "The Commodores", "http://en.wikipedia.org/wiki/Commodores" },
                    { 3, 1, "steviewonder.jpg", "Stevie Wonder", "http://www.steviewonder.net/" },
                    { 4, 1, "lionelrichie.jpg", "Lionel Richie", "http://lionelrichie.com/" },
                    { 5, 1, "marvingaye.jpg", "Marvin Gaye", "http://www.marvingayepage.net/" }
                });

            migrationBuilder.InsertData(
                table: "Schedules",
                columns: new[] { "Id", "FestivalId" },
                values: new object[] { 1, 1 });

            migrationBuilder.InsertData(
                table: "Stages",
                columns: new[] { "Id", "Description", "FestivalId", "Name" },
                values: new object[,]
                {
                    { 1, "A music festival is oriented towards music that is sometimes presented with a theme such as musical genre, nationality or locality of musicians, or holiday. They are commonly held outdoors, and are often inclusive of other attractions such as food and merchandise vending machines, performance art, and social activities. Large music festivals such as Lollapalooza are constructed around well known main stage acts and lesser known musicians and bands on side stages. Many festivals are annual, or repeat at some other interval, and have modular staging of many types. Each year Lollapalooza often features multiple acts on its main and side stages.", 1, "Main Stage" },
                    { 2, "A music festival is oriented towards music that is sometimes presented with a theme such as musical genre, nationality or locality of musicians, or holiday. They are commonly held outdoors, and are often inclusive of other attractions such as food and merchandise vending machines, performance art, and social activities. Large music festivals such as Lollapalooza are constructed around well known main stage acts and lesser known musicians and bands on side stages. Many festivals are annual, or repeat at some other interval, and have modular staging of many types. Each year Lollapalooza often features multiple acts on its main and side stages.", 1, "Orange Room" },
                    { 3, "A music festival is oriented towards music that is sometimes presented with a theme such as musical genre, nationality or locality of musicians, or holiday. They are commonly held outdoors, and are often inclusive of other attractions such as food and merchandise vending machines, performance art, and social activities. Large music festivals such as Lollapalooza are constructed around well known main stage acts and lesser known musicians and bands on side stages. Many festivals are annual, or repeat at some other interval, and have modular staging of many types. Each year Lollapalooza often features multiple acts on its main and side stages.", 1, "StarDust" },
                    { 4, "A music festival is oriented towards music that is sometimes presented with a theme such as musical genre, nationality or locality of musicians, or holiday. They are commonly held outdoors, and are often inclusive of other attractions such as food and merchandise vending machines, performance art, and social activities. Large music festivals such as Lollapalooza are constructed around well known main stage acts and lesser known musicians and bands on side stages. Many festivals are annual, or repeat at some other interval, and have modular staging of many types. Each year Lollapalooza often features multiple acts on its main and side stages.", 1, "Blue Room" },
                    { 5, "A music festival is oriented towards music that is sometimes presented with a theme such as musical genre, nationality or locality of musicians, or holiday. They are commonly held outdoors, and are often inclusive of other attractions such as food and merchandise vending machines, performance art, and social activities. Large music festivals such as Lollapalooza are constructed around well known main stage acts and lesser known musicians and bands on side stages. Many festivals are annual, or repeat at some other interval, and have modular staging of many types. Each year Lollapalooza often features multiple acts on its main and side stages.", 1, "VIP Room" }
                });

            migrationBuilder.InsertData(
                table: "ScheduleItems",
                columns: new[] { "Id", "ArtistId", "IsFavorite", "ScheduleId", "StageId", "Time" },
                values: new object[,]
                {
                    { 1, 1, false, null, 1, new DateTime(1972, 7, 1, 20, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, 5, false, null, 4, new DateTime(1972, 7, 1, 20, 30, 0, 0, DateTimeKind.Unspecified) },
                    { 3, 3, false, null, 1, new DateTime(1972, 7, 1, 22, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 4, 2, false, null, 2, new DateTime(1972, 7, 1, 22, 15, 0, 0, DateTimeKind.Unspecified) },
                    { 5, 1, false, null, 5, new DateTime(1972, 7, 2, 20, 15, 0, 0, DateTimeKind.Unspecified) },
                    { 6, 5, false, null, 4, new DateTime(1972, 7, 2, 20, 45, 0, 0, DateTimeKind.Unspecified) },
                    { 7, 4, false, null, 5, new DateTime(1972, 7, 2, 22, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 8, 2, false, null, 2, new DateTime(1972, 7, 2, 22, 30, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Artists_FestivalId",
                table: "Artists",
                column: "FestivalId");

            migrationBuilder.CreateIndex(
                name: "IX_ScheduleItems_ArtistId",
                table: "ScheduleItems",
                column: "ArtistId");

            migrationBuilder.CreateIndex(
                name: "IX_ScheduleItems_ScheduleId",
                table: "ScheduleItems",
                column: "ScheduleId");

            migrationBuilder.CreateIndex(
                name: "IX_ScheduleItems_StageId",
                table: "ScheduleItems",
                column: "StageId");

            migrationBuilder.CreateIndex(
                name: "IX_Schedules_FestivalId",
                table: "Schedules",
                column: "FestivalId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Stages_FestivalId",
                table: "Stages",
                column: "FestivalId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ScheduleItems");

            migrationBuilder.DropTable(
                name: "Artists");

            migrationBuilder.DropTable(
                name: "Schedules");

            migrationBuilder.DropTable(
                name: "Stages");

            migrationBuilder.DropTable(
                name: "Festivals");
        }
    }
}
