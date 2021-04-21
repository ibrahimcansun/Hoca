using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hoca.Models
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Ogrenci>().HasData(
                new Ogrenci
                {
                    Id = 108,
                    Ad = "ayse",
                    Soyad = "kalin",
                    Email = "ayse24@gmail.com",
                    Sifre = "123456",
                    Sinif = (Siniflar?)8
                },
                new Ogrenci
                {
                    Id = 110,
                    Ad = "ay",
                    Soyad = "tepe",
                    Email = "aytepe@gmail.com",
                    Sifre = "123456",
                    Sinif = (Siniflar?)7
                }
            );
        }
    }
}
