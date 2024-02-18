using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskBoardApp.Data.Migrations
{
    public partial class seedingData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Boards",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Boards", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tasks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "Task Identifier")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: false, comment: "Task Title"),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false, comment: "Task Description"),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "Task Date of Creation"),
                    BoardId = table.Column<int>(type: "int", nullable: false, comment: "Task Board Identifier"),
                    OwnerId = table.Column<string>(type: "nvarchar(450)", nullable: false, comment: "Task Owner Identifier")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tasks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tasks_AspNetUsers_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Tasks_Boards_BoardId",
                        column: x => x.BoardId,
                        principalTable: "Boards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "0d66f1a9-7bc2-461a-a9e7-5dc7572b8709", 0, "e12bc067-1bc9-4b04-ac12-9f8b5f35bda6", null, false, false, null, null, "TEST@SOFTUNI.BG", "AQAAAAEAACcQAAAAEA+iXmpIwH9hycu3r0/pbj8wNnAKJiM+B/gAjtvNllULtuup7P/Ao47dgXE1RC4W2w==", null, false, "f3350136-22e1-44a1-be48-9f759b282f49", false, "test@softuni.bg" });

            migrationBuilder.InsertData(
                table: "Boards",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Open" },
                    { 2, "In Progress" },
                    { 3, "Done" }
                });

            migrationBuilder.InsertData(
                table: "Tasks",
                columns: new[] { "Id", "BoardId", "CreatedOn", "Description", "OwnerId", "Title" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2023, 7, 24, 16, 11, 42, 991, DateTimeKind.Local).AddTicks(233), "Implement better styling for all public pages", "0d66f1a9-7bc2-461a-a9e7-5dc7572b8709", "Improve CSS styles" },
                    { 2, 1, new DateTime(2023, 9, 9, 16, 11, 42, 991, DateTimeKind.Local).AddTicks(266), "Create Android client app for the TaskBoard RESTful API", "0d66f1a9-7bc2-461a-a9e7-5dc7572b8709", "Android Client App" },
                    { 3, 2, new DateTime(2024, 1, 9, 16, 11, 42, 991, DateTimeKind.Local).AddTicks(268), "Create Windows Forms app client for the TaskBoard RESTful API", "0d66f1a9-7bc2-461a-a9e7-5dc7572b8709", "Desktop Client App" },
                    { 4, 3, new DateTime(2023, 2, 9, 16, 11, 42, 991, DateTimeKind.Local).AddTicks(271), "Implement [Create Task] page for adding new tasks", "0d66f1a9-7bc2-461a-a9e7-5dc7572b8709", "Create Tasks" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_BoardId",
                table: "Tasks",
                column: "BoardId");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_OwnerId",
                table: "Tasks",
                column: "OwnerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tasks");

            migrationBuilder.DropTable(
                name: "Boards");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "0d66f1a9-7bc2-461a-a9e7-5dc7572b8709");
        }
    }
}
