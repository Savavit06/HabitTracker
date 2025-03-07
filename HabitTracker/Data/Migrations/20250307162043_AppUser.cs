using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HabitTracker.Data.Migrations
{
    /// <inheritdoc />
    public partial class AppUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AppUserId",
                table: "Habit",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Habit_AppUserId",
                table: "Habit",
                column: "AppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Habit_AspNetUsers_AppUserId",
                table: "Habit",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Habit_AspNetUsers_AppUserId",
                table: "Habit");

            migrationBuilder.DropIndex(
                name: "IX_Habit_AppUserId",
                table: "Habit");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "Habit");
        }
    }
}
