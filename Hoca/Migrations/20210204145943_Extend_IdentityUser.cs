using Microsoft.EntityFrameworkCore.Migrations;

namespace Hoca.Migrations
{
    public partial class Extend_IdentityUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OgrenciUserId",
                table: "Sorular",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FotoPath",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Sinif",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "sorulanSoruSayisi",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Sorular_OgrenciUserId",
                table: "Sorular",
                column: "OgrenciUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Sorular_AspNetUsers_OgrenciUserId",
                table: "Sorular",
                column: "OgrenciUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sorular_AspNetUsers_OgrenciUserId",
                table: "Sorular");

            migrationBuilder.DropIndex(
                name: "IX_Sorular_OgrenciUserId",
                table: "Sorular");

            migrationBuilder.DropColumn(
                name: "OgrenciUserId",
                table: "Sorular");

            migrationBuilder.DropColumn(
                name: "FotoPath",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Sinif",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "sorulanSoruSayisi",
                table: "AspNetUsers");
        }
    }
}
