using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Hoca.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Adminler",
                columns: table => new
                {
                    AdminID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Soyad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Sifre = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Adminler", x => x.AdminID);
                });

            migrationBuilder.CreateTable(
                name: "Ogrenciler",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ad = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Soyad = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(345)", maxLength: 345, nullable: false),
                    Sifre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Sinif = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ogrenciler", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Ogretmenler",
                columns: table => new
                {
                    OgretmenID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Soyad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Mail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Sifre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Bolum = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CevaplananSoruSayisi = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ogretmenler", x => x.OgretmenID);
                });

            migrationBuilder.CreateTable(
                name: "Cevaplar",
                columns: table => new
                {
                    CevapID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Cevap = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OgretmenID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cevaplar", x => x.CevapID);
                    table.ForeignKey(
                        name: "FK_Cevaplar_Ogretmenler_OgretmenID",
                        column: x => x.OgretmenID,
                        principalTable: "Ogretmenler",
                        principalColumn: "OgretmenID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Sorular",
                columns: table => new
                {
                    SoruID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SoruBaslik = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SoruIcerik = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SoruKategori = table.Column<int>(type: "int", nullable: false),
                    Tarih = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OgrenciId = table.Column<int>(type: "int", nullable: true),
                    CevaplarCevapID = table.Column<int>(type: "int", nullable: true),
                    OgretmenID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sorular", x => x.SoruID);
                    table.ForeignKey(
                        name: "FK_Sorular_Cevaplar_CevaplarCevapID",
                        column: x => x.CevaplarCevapID,
                        principalTable: "Cevaplar",
                        principalColumn: "CevapID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Sorular_Ogrenciler_OgrenciId",
                        column: x => x.OgrenciId,
                        principalTable: "Ogrenciler",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Sorular_Ogretmenler_OgretmenID",
                        column: x => x.OgretmenID,
                        principalTable: "Ogretmenler",
                        principalColumn: "OgretmenID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cevaplar_OgretmenID",
                table: "Cevaplar",
                column: "OgretmenID");

            migrationBuilder.CreateIndex(
                name: "IX_Sorular_CevaplarCevapID",
                table: "Sorular",
                column: "CevaplarCevapID");

            migrationBuilder.CreateIndex(
                name: "IX_Sorular_OgrenciId",
                table: "Sorular",
                column: "OgrenciId");

            migrationBuilder.CreateIndex(
                name: "IX_Sorular_OgretmenID",
                table: "Sorular",
                column: "OgretmenID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Adminler");

            migrationBuilder.DropTable(
                name: "Sorular");

            migrationBuilder.DropTable(
                name: "Cevaplar");

            migrationBuilder.DropTable(
                name: "Ogrenciler");

            migrationBuilder.DropTable(
                name: "Ogretmenler");
        }
    }
}
