using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PowerDiary.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ChatEvents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    OccurredAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Type = table.Column<int>(type: "INTEGER", nullable: false),
                    UserName = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Message = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true),
                    ToUserName = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatEvents", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "ChatEvents",
                columns: new[] { "Id", "OccurredAt", "Type", "UserName" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 2, 2, 18, 5, 16, 0, DateTimeKind.Unspecified), 0, "Bob" },
                    { 2, new DateTime(2024, 2, 2, 18, 7, 16, 0, DateTimeKind.Unspecified), 0, "Alice" },
                    { 3, new DateTime(2024, 2, 2, 18, 15, 16, 0, DateTimeKind.Unspecified), 0, "George" },
                    { 4, new DateTime(2024, 2, 2, 20, 5, 16, 0, DateTimeKind.Unspecified), 0, "Bob" },
                    { 5, new DateTime(2024, 2, 2, 20, 7, 16, 0, DateTimeKind.Unspecified), 0, "John" },
                    { 6, new DateTime(2024, 2, 2, 21, 15, 16, 0, DateTimeKind.Unspecified), 0, "Maria" },
                    { 7, new DateTime(2024, 2, 3, 19, 10, 16, 0, DateTimeKind.Unspecified), 0, "Bob" },
                    { 8, new DateTime(2024, 2, 3, 19, 15, 16, 0, DateTimeKind.Unspecified), 0, "Maria" },
                    { 9, new DateTime(2024, 2, 3, 20, 20, 16, 0, DateTimeKind.Unspecified), 0, "Alice" },
                    { 10, new DateTime(2024, 2, 3, 20, 25, 16, 0, DateTimeKind.Unspecified), 0, "John" },
                    { 11, new DateTime(2024, 2, 2, 18, 15, 16, 0, DateTimeKind.Unspecified), 1, "Bob" },
                    { 12, new DateTime(2024, 2, 2, 18, 27, 16, 0, DateTimeKind.Unspecified), 1, "Alice" },
                    { 13, new DateTime(2024, 2, 2, 18, 30, 16, 0, DateTimeKind.Unspecified), 1, "John" },
                    { 14, new DateTime(2024, 2, 2, 20, 10, 16, 0, DateTimeKind.Unspecified), 1, "Bob" },
                    { 15, new DateTime(2024, 2, 2, 20, 25, 16, 0, DateTimeKind.Unspecified), 1, "John" },
                    { 16, new DateTime(2024, 2, 2, 21, 25, 16, 0, DateTimeKind.Unspecified), 1, "Maria" },
                    { 17, new DateTime(2024, 2, 3, 19, 20, 16, 0, DateTimeKind.Unspecified), 1, "Bob" },
                    { 18, new DateTime(2024, 2, 3, 19, 25, 16, 0, DateTimeKind.Unspecified), 1, "Maria" }
                });

            migrationBuilder.InsertData(
                table: "ChatEvents",
                columns: new[] { "Id", "Message", "OccurredAt", "Type", "UserName" },
                values: new object[,]
                {
                    { 19, "Hello", new DateTime(2024, 2, 2, 18, 10, 16, 0, DateTimeKind.Unspecified), 2, "Bob" },
                    { 20, "Hi", new DateTime(2024, 2, 2, 18, 12, 16, 0, DateTimeKind.Unspecified), 2, "Alice" },
                    { 21, "How are you?", new DateTime(2024, 2, 2, 18, 17, 16, 0, DateTimeKind.Unspecified), 2, "George" },
                    { 22, "Hey people", new DateTime(2024, 2, 2, 18, 17, 16, 0, DateTimeKind.Unspecified), 2, "George" },
                    { 23, "Hello again", new DateTime(2024, 2, 2, 18, 19, 16, 0, DateTimeKind.Unspecified), 2, "George" },
                    { 24, "Hey Alice", new DateTime(2024, 2, 2, 20, 12, 16, 0, DateTimeKind.Unspecified), 2, "Bob" },
                    { 25, "Hi", new DateTime(2024, 2, 2, 20, 15, 16, 0, DateTimeKind.Unspecified), 2, "John" },
                    { 26, "Hello all", new DateTime(2024, 2, 2, 20, 15, 16, 0, DateTimeKind.Unspecified), 2, "Alice" },
                    { 27, "How are you?", new DateTime(2024, 2, 2, 21, 17, 16, 0, DateTimeKind.Unspecified), 2, "Maria" },
                    { 28, "How are you?", new DateTime(2024, 2, 2, 21, 17, 16, 0, DateTimeKind.Unspecified), 2, "George" },
                    { 29, "Hi George", new DateTime(2024, 2, 2, 21, 25, 16, 0, DateTimeKind.Unspecified), 2, "Maria" },
                    { 30, "Hello Maria", new DateTime(2024, 2, 2, 21, 27, 16, 0, DateTimeKind.Unspecified), 2, "George" },
                    { 31, "Hello", new DateTime(2024, 2, 3, 19, 17, 16, 0, DateTimeKind.Unspecified), 2, "Bob" },
                    { 32, "Hi", new DateTime(2024, 2, 3, 19, 20, 16, 0, DateTimeKind.Unspecified), 2, "Maria" },
                    { 33, "How are you?", new DateTime(2024, 2, 3, 19, 25, 16, 0, DateTimeKind.Unspecified), 2, "Bob" },
                    { 34, "How are you?", new DateTime(2024, 2, 3, 20, 17, 16, 0, DateTimeKind.Unspecified), 2, "Alice" },
                    { 35, "Hi", new DateTime(2024, 2, 3, 20, 20, 16, 0, DateTimeKind.Unspecified), 2, "John" },
                    { 36, "Hello all", new DateTime(2024, 2, 3, 20, 20, 16, 0, DateTimeKind.Unspecified), 2, "Alice" },
                    { 37, "How are you?", new DateTime(2024, 2, 3, 21, 15, 16, 0, DateTimeKind.Unspecified), 2, "Maria" },
                    { 38, "How are you?", new DateTime(2024, 2, 3, 21, 25, 16, 0, DateTimeKind.Unspecified), 2, "George" },
                    { 39, "Hello", new DateTime(2024, 2, 4, 19, 17, 16, 0, DateTimeKind.Unspecified), 2, "Bob" },
                    { 40, "Hi", new DateTime(2024, 2, 4, 19, 20, 16, 0, DateTimeKind.Unspecified), 2, "Maria" },
                    { 41, "How are you?", new DateTime(2024, 2, 4, 19, 25, 16, 0, DateTimeKind.Unspecified), 2, "Bob" },
                    { 42, "How are you?", new DateTime(2024, 2, 4, 20, 5, 16, 0, DateTimeKind.Unspecified), 2, "Alice" },
                    { 43, "Hi", new DateTime(2024, 2, 4, 20, 10, 16, 0, DateTimeKind.Unspecified), 2, "John" },
                    { 44, "Hello all", new DateTime(2024, 2, 4, 20, 10, 16, 0, DateTimeKind.Unspecified), 2, "Alice" }
                });

            migrationBuilder.InsertData(
                table: "ChatEvents",
                columns: new[] { "Id", "OccurredAt", "ToUserName", "Type", "UserName" },
                values: new object[,]
                {
                    { 45, new DateTime(2024, 2, 2, 18, 13, 16, 0, DateTimeKind.Unspecified), "Alice", 3, "Bob" },
                    { 46, new DateTime(2024, 2, 2, 18, 15, 16, 0, DateTimeKind.Unspecified), "Alice", 3, "John" },
                    { 47, new DateTime(2024, 2, 2, 18, 17, 16, 0, DateTimeKind.Unspecified), "Bob", 3, "Alice" },
                    { 48, new DateTime(2024, 2, 2, 18, 19, 16, 0, DateTimeKind.Unspecified), "Alice", 3, "George" },
                    { 49, new DateTime(2024, 2, 2, 20, 13, 16, 0, DateTimeKind.Unspecified), "Alice", 3, "Bob" },
                    { 50, new DateTime(2024, 2, 2, 20, 15, 16, 0, DateTimeKind.Unspecified), "Alice", 3, "John" },
                    { 51, new DateTime(2024, 2, 2, 20, 17, 16, 0, DateTimeKind.Unspecified), "Bob", 3, "Alice" },
                    { 52, new DateTime(2024, 2, 2, 20, 19, 16, 0, DateTimeKind.Unspecified), "Alice", 3, "George" },
                    { 53, new DateTime(2024, 2, 2, 21, 13, 16, 0, DateTimeKind.Unspecified), "Alice", 3, "Bob" },
                    { 54, new DateTime(2024, 2, 2, 21, 15, 16, 0, DateTimeKind.Unspecified), "Alice", 3, "John" },
                    { 55, new DateTime(2024, 2, 2, 21, 17, 16, 0, DateTimeKind.Unspecified), "Bob", 3, "Alice" },
                    { 56, new DateTime(2024, 2, 2, 21, 19, 16, 0, DateTimeKind.Unspecified), "Alice", 3, "George" },
                    { 57, new DateTime(2024, 2, 3, 19, 13, 16, 0, DateTimeKind.Unspecified), "Alice", 3, "Bob" },
                    { 58, new DateTime(2024, 2, 3, 19, 15, 16, 0, DateTimeKind.Unspecified), "Alice", 3, "John" },
                    { 59, new DateTime(2024, 2, 3, 19, 17, 16, 0, DateTimeKind.Unspecified), "Bob", 3, "Alice" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChatEvents");
        }
    }
}
