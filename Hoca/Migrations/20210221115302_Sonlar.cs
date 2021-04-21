using Microsoft.EntityFrameworkCore.Migrations;

namespace Hoca.Migrations
{
    public partial class Sonlar : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sorular_AspNetUsers_OgrenciUserId",
                table: "Sorular");

            migrationBuilder.RenameColumn(
                name: "OgrenciUserId",
                table: "Sorular",
                newName: "OgrenciId1");

            migrationBuilder.RenameIndex(
                name: "IX_Sorular_OgrenciUserId",
                table: "Sorular",
                newName: "IX_Sorular_OgrenciId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Sorular_AspNetUsers_OgrenciId1",
                table: "Sorular",
                column: "OgrenciId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sorular_AspNetUsers_OgrenciId1",
                table: "Sorular");

            migrationBuilder.RenameColumn(
                name: "OgrenciId1",
                table: "Sorular",
                newName: "OgrenciUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Sorular_OgrenciId1",
                table: "Sorular",
                newName: "IX_Sorular_OgrenciUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Sorular_AspNetUsers_OgrenciUserId",
                table: "Sorular",
                column: "OgrenciUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
