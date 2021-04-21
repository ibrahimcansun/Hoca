using Microsoft.EntityFrameworkCore.Migrations;

namespace Hoca.Migrations
{
    public partial class SeedOgrencis : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Ogrenciler",
                keyColumn: "Id",
                keyValue: 100);

            migrationBuilder.InsertData(
                table: "Ogrenciler",
                columns: new[] { "Id", "Ad", "Email", "Sifre", "Sinif", "Soyad", "sorulanSoruSayisi" },
                values: new object[] { 108, "ayse", "ayse24@gmail.com", "123456", 8, "kalin", 0 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Ogrenciler",
                keyColumn: "Id",
                keyValue: 108);

            migrationBuilder.InsertData(
                table: "Ogrenciler",
                columns: new[] { "Id", "Ad", "Email", "Sifre", "Sinif", "Soyad", "sorulanSoruSayisi" },
                values: new object[] { 100, "ayşe", "ayse@gmail.com", "123456", 12, "dal", 0 });
        }
    }
}
