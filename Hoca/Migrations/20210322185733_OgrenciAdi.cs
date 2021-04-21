using Microsoft.EntityFrameworkCore.Migrations;

namespace Hoca.Migrations
{
    public partial class OgrenciAdi : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cevaplar_Ogretmenler_OgretmenID",
                table: "Cevaplar");

            migrationBuilder.DropForeignKey(
                name: "FK_Sorular_AspNetUsers_OgrenciId1",
                table: "Sorular");

            migrationBuilder.DropForeignKey(
                name: "FK_Sorular_Cevaplar_CevaplarCevapID",
                table: "Sorular");

            migrationBuilder.DropForeignKey(
                name: "FK_Sorular_Ogretmenler_OgretmenID",
                table: "Sorular");

            migrationBuilder.DropTable(
                name: "Adminler");

            migrationBuilder.DropTable(
                name: "Ogretmenler");

            migrationBuilder.DropIndex(
                name: "IX_Sorular_CevaplarCevapID",
                table: "Sorular");

            migrationBuilder.DropIndex(
                name: "IX_Sorular_OgrenciId1",
                table: "Sorular");

            migrationBuilder.DropIndex(
                name: "IX_Sorular_OgretmenID",
                table: "Sorular");

            migrationBuilder.DropColumn(
                name: "CevaplarCevapID",
                table: "Sorular");

            migrationBuilder.DropColumn(
                name: "OgretmenID",
                table: "Sorular");

            migrationBuilder.DropColumn(
                name: "sorulanSoruSayisi",
                table: "Ogrenciler");

            migrationBuilder.RenameColumn(
                name: "OgretmenID",
                table: "Cevaplar",
                newName: "OgretmenId");

            migrationBuilder.RenameIndex(
                name: "IX_Cevaplar_OgretmenID",
                table: "Cevaplar",
                newName: "IX_Cevaplar_OgretmenId");

            migrationBuilder.AlterColumn<string>(
                name: "OgrenciId1",
                table: "Sorular",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "OgrenciAd",
                table: "Sorular",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "OgretmenId",
                table: "Cevaplar",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Cevaplar_AspNetUsers_OgretmenId",
                table: "Cevaplar",
                column: "OgretmenId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cevaplar_AspNetUsers_OgretmenId",
                table: "Cevaplar");

            migrationBuilder.DropColumn(
                name: "OgrenciAd",
                table: "Sorular");

            migrationBuilder.RenameColumn(
                name: "OgretmenId",
                table: "Cevaplar",
                newName: "OgretmenID");

            migrationBuilder.RenameIndex(
                name: "IX_Cevaplar_OgretmenId",
                table: "Cevaplar",
                newName: "IX_Cevaplar_OgretmenID");

            migrationBuilder.AlterColumn<string>(
                name: "OgrenciId1",
                table: "Sorular",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "CevaplarCevapID",
                table: "Sorular",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OgretmenID",
                table: "Sorular",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "sorulanSoruSayisi",
                table: "Ogrenciler",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "OgretmenID",
                table: "Cevaplar",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "Adminler",
                columns: table => new
                {
                    AdminID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Sifre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Soyad = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Adminler", x => x.AdminID);
                });

            migrationBuilder.CreateTable(
                name: "Ogretmenler",
                columns: table => new
                {
                    OgretmenID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ad = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Bolum = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CevaplananSoruSayisi = table.Column<int>(type: "int", nullable: false),
                    Mail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Sifre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Soyad = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ogretmenler", x => x.OgretmenID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Sorular_CevaplarCevapID",
                table: "Sorular",
                column: "CevaplarCevapID");

            migrationBuilder.CreateIndex(
                name: "IX_Sorular_OgrenciId1",
                table: "Sorular",
                column: "OgrenciId1");

            migrationBuilder.CreateIndex(
                name: "IX_Sorular_OgretmenID",
                table: "Sorular",
                column: "OgretmenID");

            migrationBuilder.AddForeignKey(
                name: "FK_Cevaplar_Ogretmenler_OgretmenID",
                table: "Cevaplar",
                column: "OgretmenID",
                principalTable: "Ogretmenler",
                principalColumn: "OgretmenID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Sorular_AspNetUsers_OgrenciId1",
                table: "Sorular",
                column: "OgrenciId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Sorular_Cevaplar_CevaplarCevapID",
                table: "Sorular",
                column: "CevaplarCevapID",
                principalTable: "Cevaplar",
                principalColumn: "CevapID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Sorular_Ogretmenler_OgretmenID",
                table: "Sorular",
                column: "OgretmenID",
                principalTable: "Ogretmenler",
                principalColumn: "OgretmenID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
