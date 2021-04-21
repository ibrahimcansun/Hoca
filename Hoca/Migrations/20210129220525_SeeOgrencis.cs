using Microsoft.EntityFrameworkCore.Migrations;

namespace Hoca.Migrations
{
    public partial class SeeOgrencis : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Ogrenciler",
                columns: new[] { "Id", "Ad", "Email", "Sifre", "Sinif", "Soyad", "sorulanSoruSayisi" },
                values: new object[] { 110, "ay", "aytepe@gmail.com", "123456", 7, "tepe", 0 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Ogrenciler",
                keyColumn: "Id",
                keyValue: 110);
        }
    }
}
