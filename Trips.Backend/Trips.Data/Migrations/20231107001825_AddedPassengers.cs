using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Trips.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedPassengers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Passengers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_generate_v4()"),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Passengers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TripPassengers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_generate_v4()"),
                    PassengerId = table.Column<Guid>(type: "uuid", nullable: false),
                    TripId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TripPassengers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TripPassengers_Passengers_PassengerId",
                        column: x => x.PassengerId,
                        principalTable: "Passengers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TripPassengers_Trips_TripId",
                        column: x => x.TripId,
                        principalTable: "Trips",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Passengers",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("39a4b601-45cc-49d0-bf27-ddf0ccd22f6b"), "John" },
                    { new Guid("c1a9ec7c-87de-4884-ae03-b87afa0228df"), "Bill" },
                    { new Guid("cb34f6ff-6a81-4f8a-a98c-7b3fed4903ae"), "Sam" },
                    { new Guid("d277cff1-88ab-4b5a-a89f-6fc3535e3a8f"), "Jin" }
                });

            migrationBuilder.InsertData(
                table: "TripPassengers",
                columns: new[] { "Id", "PassengerId", "TripId" },
                values: new object[,]
                {
                    { new Guid("2bb56d1f-fabe-4044-b519-f8050a0a5afb"), new Guid("d277cff1-88ab-4b5a-a89f-6fc3535e3a8f"), new Guid("1cc80906-88e3-4baf-9855-a4ec131b7307") },
                    { new Guid("3f9a4bce-1c59-43ca-8e4e-23a39418a46f"), new Guid("39a4b601-45cc-49d0-bf27-ddf0ccd22f6b"), new Guid("68e92246-76c9-410b-8f73-dfa62b32edd4") },
                    { new Guid("75d70806-47e9-410f-82d5-77ecb5c74bb5"), new Guid("c1a9ec7c-87de-4884-ae03-b87afa0228df"), new Guid("1cc80906-88e3-4baf-9855-a4ec131b7307") },
                    { new Guid("c19c99f6-7e93-43a8-a98f-94cb44298d35"), new Guid("39a4b601-45cc-49d0-bf27-ddf0ccd22f6b"), new Guid("07e8a2dc-e96b-4f7d-831f-c6cae84edd83") },
                    { new Guid("c445fb2b-e8ef-471f-8193-d25a2689b8cc"), new Guid("cb34f6ff-6a81-4f8a-a98c-7b3fed4903ae"), new Guid("94130245-d7d9-4788-a758-77065f1d22d1") },
                    { new Guid("fb3c688a-8589-4bf7-8bc8-8577850ce545"), new Guid("cb34f6ff-6a81-4f8a-a98c-7b3fed4903ae"), new Guid("1cc80906-88e3-4baf-9855-a4ec131b7307") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_TripPassengers_PassengerId",
                table: "TripPassengers",
                column: "PassengerId");

            migrationBuilder.CreateIndex(
                name: "IX_TripPassengers_TripId",
                table: "TripPassengers",
                column: "TripId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TripPassengers");

            migrationBuilder.DropTable(
                name: "Passengers");
        }
    }
}
