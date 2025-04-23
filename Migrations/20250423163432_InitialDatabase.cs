using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BookingApp.Migrations
{
    /// <inheritdoc />
    public partial class InitialDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "events",
                columns: table => new
                {
                    EventId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Duration = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Time = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Date = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_events", x => x.EventId);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AvatarUrl = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "eventUsers",
                columns: table => new
                {
                    EventUserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EventId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_eventUsers", x => x.EventUserId);
                    table.ForeignKey(
                        name: "FK_eventUsers_events_EventId",
                        column: x => x.EventId,
                        principalTable: "events",
                        principalColumn: "EventId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_eventUsers_users_UserId",
                        column: x => x.UserId,
                        principalTable: "users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "events",
                columns: new[] { "EventId", "Date", "Duration", "Time" },
                values: new object[,]
                {
                    { 1, "2025-03-13", "30 MIN", "10:00 AM" },
                    { 2, "2025-03-13", "45 MIN", "02:00 PM" },
                    { 3, "2025-03-14", "1 HOUR", "06:00 PM" }
                });

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "UserId", "AvatarUrl", "Email", "Name", "Phone" },
                values: new object[,]
                {
                    { 1, "https://example.com/avatars/johndoe.png", "john.doe@example.com", "John Doe", "1234567890" },
                    { 2, "https://example.com/avatars/janesmith.png", "jane.smith@example.com", "Jane Smith", "0987654321" },
                    { 3, "https://example.com/avatars/alicejohnson.png", "alice.johnson@example.com", "Alice Johnson", "1122334455" }
                });

            migrationBuilder.InsertData(
                table: "eventUsers",
                columns: new[] { "EventUserId", "EventId", "Role", "UserId" },
                values: new object[,]
                {
                    { 1, 1, "Organizer", 1 },
                    { 2, 1, "Attendee", 2 },
                    { 3, 2, "Attendee", 3 },
                    { 4, 3, "Attendee", 1 },
                    { 5, 3, "Organizer", 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_eventUsers_EventId",
                table: "eventUsers",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_eventUsers_UserId",
                table: "eventUsers",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "eventUsers");

            migrationBuilder.DropTable(
                name: "events");

            migrationBuilder.DropTable(
                name: "users");
        }
    }
}
