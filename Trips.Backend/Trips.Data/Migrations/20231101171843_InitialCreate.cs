using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Trips.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:PostgresExtension:uuid-ossp", ",,");

            migrationBuilder.CreateTable(
                name: "Drivers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_generate_v4()"),
                    PayableTime = table.Column<double>(type: "double precision", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Drivers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Trips",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_generate_v4()"),
                    TripStartTimestamp = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    TripEndTimestamp = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    StartPlaceName = table.Column<string>(type: "text", nullable: false),
                    DestinationPlaceName = table.Column<string>(type: "text", nullable: false),
                    DriverId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trips", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Trips_Drivers_DriverId",
                        column: x => x.DriverId,
                        principalTable: "Drivers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Drivers",
                columns: new[] { "Id", "Name", "PayableTime" },
                values: new object[,]
                {
                    { new Guid("37bd6a0b-d6d6-4525-ac7b-e673717ec8ad"), "Joanne", null },
                    { new Guid("ee159f08-4a14-4d69-8f9f-92cebbfbd19c"), "Jack", null }
                });

            migrationBuilder.InsertData(
                table: "Trips",
                columns: new[] { "Id", "DestinationPlaceName", "DriverId", "StartPlaceName", "TripEndTimestamp", "TripStartTimestamp" },
                values: new object[,]
                {
                    { new Guid("07e8a2dc-e96b-4f7d-831f-c6cae84edd83"), "Place B", new Guid("37bd6a0b-d6d6-4525-ac7b-e673717ec8ad"), "Place C", new DateTime(2012, 4, 23, 2, 43, 23, 0, DateTimeKind.Utc), new DateTime(2012, 4, 23, 2, 6, 15, 0, DateTimeKind.Utc) },
                    { new Guid("1cc80906-88e3-4baf-9855-a4ec131b7307"), "Place B", new Guid("ee159f08-4a14-4d69-8f9f-92cebbfbd19c"), "Place A", new DateTime(2012, 4, 23, 1, 15, 0, 0, DateTimeKind.Utc), new DateTime(2012, 4, 23, 1, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("68e92246-76c9-410b-8f73-dfa62b32edd4"), "Place B", new Guid("ee159f08-4a14-4d69-8f9f-92cebbfbd19c"), "Place C", new DateTime(2012, 4, 23, 3, 47, 0, 0, DateTimeKind.Utc), new DateTime(2012, 4, 23, 3, 33, 0, 0, DateTimeKind.Utc) },
                    { new Guid("84e53efd-a138-4f61-acf4-11de6888475d"), "Place Bb", new Guid("ee159f08-4a14-4d69-8f9f-92cebbfbd19c"), "Place Aa", new DateTime(2012, 4, 23, 2, 2, 0, 0, DateTimeKind.Utc), new DateTime(2012, 4, 23, 2, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("94130245-d7d9-4788-a758-77065f1d22d1"), "Place Bb", new Guid("37bd6a0b-d6d6-4525-ac7b-e673717ec8ad"), "Place Aa", new DateTime(2012, 4, 23, 3, 59, 0, 0, DateTimeKind.Utc), new DateTime(2012, 4, 23, 3, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("b6bc9b3b-c45e-45ce-94d7-746acdb64f63"), "Place C", new Guid("37bd6a0b-d6d6-4525-ac7b-e673717ec8ad"), "Place B", new DateTime(2012, 4, 23, 1, 22, 0, 0, DateTimeKind.Utc), new DateTime(2012, 4, 23, 1, 6, 0, 0, DateTimeKind.Utc) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Trips_DriverId",
                table: "Trips",
                column: "DriverId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Trips");

            migrationBuilder.DropTable(
                name: "Drivers");
        }
    }
}
