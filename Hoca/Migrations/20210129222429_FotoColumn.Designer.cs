﻿// <auto-generated />
using System;
using Hoca.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Hoca.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20210129222429_FotoColumn")]
    partial class FotoColumn
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.2");

            modelBuilder.Entity("Hoca.Models.Admin", b =>
                {
                    b.Property<int>("AdminID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Ad")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Sifre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Soyad")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("AdminID");

                    b.ToTable("Adminler");
                });

            modelBuilder.Entity("Hoca.Models.Cevaplar", b =>
                {
                    b.Property<int>("CevapID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Cevap")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("OgretmenID")
                        .HasColumnType("int");

                    b.HasKey("CevapID");

                    b.HasIndex("OgretmenID");

                    b.ToTable("Cevaplar");
                });

            modelBuilder.Entity("Hoca.Models.Ogrenci", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Ad")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(345)
                        .HasColumnType("nvarchar(345)");

                    b.Property<string>("FotoPath")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Sifre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Sinif")
                        .HasColumnType("int");

                    b.Property<string>("Soyad")
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<int>("sorulanSoruSayisi")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Ogrenciler");

                    b.HasData(
                        new
                        {
                            Id = 108,
                            Ad = "ayse",
                            Email = "ayse24@gmail.com",
                            Sifre = "123456",
                            Sinif = 8,
                            Soyad = "kalin",
                            sorulanSoruSayisi = 0
                        },
                        new
                        {
                            Id = 110,
                            Ad = "ay",
                            Email = "aytepe@gmail.com",
                            Sifre = "123456",
                            Sinif = 7,
                            Soyad = "tepe",
                            sorulanSoruSayisi = 0
                        });
                });

            modelBuilder.Entity("Hoca.Models.Ogretmen", b =>
                {
                    b.Property<int>("OgretmenID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Ad")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Bolum")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("CevaplananSoruSayisi")
                        .HasColumnType("int");

                    b.Property<string>("Mail")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Sifre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Soyad")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("OgretmenID");

                    b.ToTable("Ogretmenler");
                });

            modelBuilder.Entity("Hoca.Models.Sorular", b =>
                {
                    b.Property<int>("SoruID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int?>("CevaplarCevapID")
                        .HasColumnType("int");

                    b.Property<int?>("OgrenciId")
                        .HasColumnType("int");

                    b.Property<int?>("OgretmenID")
                        .HasColumnType("int");

                    b.Property<string>("SoruBaslik")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SoruIcerik")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SoruKategori")
                        .HasColumnType("int");

                    b.Property<DateTime>("Tarih")
                        .HasColumnType("datetime2");

                    b.HasKey("SoruID");

                    b.HasIndex("CevaplarCevapID");

                    b.HasIndex("OgrenciId");

                    b.HasIndex("OgretmenID");

                    b.ToTable("Sorular");
                });

            modelBuilder.Entity("Hoca.Models.Cevaplar", b =>
                {
                    b.HasOne("Hoca.Models.Ogretmen", "Ogretmen")
                        .WithMany()
                        .HasForeignKey("OgretmenID");

                    b.Navigation("Ogretmen");
                });

            modelBuilder.Entity("Hoca.Models.Sorular", b =>
                {
                    b.HasOne("Hoca.Models.Cevaplar", "Cevaplar")
                        .WithMany()
                        .HasForeignKey("CevaplarCevapID");

                    b.HasOne("Hoca.Models.Ogrenci", "Ogrenci")
                        .WithMany("SorduguSorular")
                        .HasForeignKey("OgrenciId");

                    b.HasOne("Hoca.Models.Ogretmen", null)
                        .WithMany("Sorular")
                        .HasForeignKey("OgretmenID");

                    b.Navigation("Cevaplar");

                    b.Navigation("Ogrenci");
                });

            modelBuilder.Entity("Hoca.Models.Ogrenci", b =>
                {
                    b.Navigation("SorduguSorular");
                });

            modelBuilder.Entity("Hoca.Models.Ogretmen", b =>
                {
                    b.Navigation("Sorular");
                });
#pragma warning restore 612, 618
        }
    }
}
