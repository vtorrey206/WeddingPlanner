using Microsoft.EntityFrameworkCore.Migrations;

namespace WeddingPlanner.Migrations
{
    public partial class THEREALONE : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Creator_User_UserId",
                table: "Creator");

            migrationBuilder.DropForeignKey(
                name: "FK_Guest_Creator_WeddingId",
                table: "Guest");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Creator",
                table: "Creator");

            migrationBuilder.RenameTable(
                name: "Creator",
                newName: "Wedding");

            migrationBuilder.RenameIndex(
                name: "IX_Creator_UserId",
                table: "Wedding",
                newName: "IX_Wedding_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Wedding",
                table: "Wedding",
                column: "WeddingId");

            migrationBuilder.AddForeignKey(
                name: "FK_Guest_Wedding_WeddingId",
                table: "Guest",
                column: "WeddingId",
                principalTable: "Wedding",
                principalColumn: "WeddingId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Wedding_User_UserId",
                table: "Wedding",
                column: "UserId",
                principalTable: "User",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Guest_Wedding_WeddingId",
                table: "Guest");

            migrationBuilder.DropForeignKey(
                name: "FK_Wedding_User_UserId",
                table: "Wedding");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Wedding",
                table: "Wedding");

            migrationBuilder.RenameTable(
                name: "Wedding",
                newName: "Creator");

            migrationBuilder.RenameIndex(
                name: "IX_Wedding_UserId",
                table: "Creator",
                newName: "IX_Creator_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Creator",
                table: "Creator",
                column: "WeddingId");

            migrationBuilder.AddForeignKey(
                name: "FK_Creator_User_UserId",
                table: "Creator",
                column: "UserId",
                principalTable: "User",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Guest_Creator_WeddingId",
                table: "Guest",
                column: "WeddingId",
                principalTable: "Creator",
                principalColumn: "WeddingId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
