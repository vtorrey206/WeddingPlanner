using Microsoft.EntityFrameworkCore.Migrations;

namespace WeddingPlanner.Migrations
{
    public partial class SecondMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Creator_Creator_WeddingId1",
                table: "Creator");

            migrationBuilder.RenameColumn(
                name: "WeddingId1",
                table: "Creator",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Creator_WeddingId1",
                table: "Creator",
                newName: "IX_Creator_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Creator_User_UserId",
                table: "Creator",
                column: "UserId",
                principalTable: "User",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Creator_User_UserId",
                table: "Creator");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Creator",
                newName: "WeddingId1");

            migrationBuilder.RenameIndex(
                name: "IX_Creator_UserId",
                table: "Creator",
                newName: "IX_Creator_WeddingId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Creator_Creator_WeddingId1",
                table: "Creator",
                column: "WeddingId1",
                principalTable: "Creator",
                principalColumn: "WeddingId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
