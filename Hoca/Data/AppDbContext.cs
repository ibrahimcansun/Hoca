using Hoca.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hoca.Data
{
    public class AppDbContext : IdentityDbContext<OgrenciUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) 
            : base(options)
        {

        }
        public DbSet<Ogrenci> Ogrenciler { get; set; }
        //public DbSet<Ogretmen> Ogretmenler { get; set; }
        //public DbSet<Admin> Adminler { get; set; }
        public DbSet<Sorular> Sorular { get; set; }
        public DbSet<Cevaplar> Cevaplar { get; set; }
        //public DbSet<Soru_Cevap> Soru_Cevap { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Seed();

            foreach (var foreignKey in modelBuilder.Model.GetEntityTypes()
                .SelectMany(e => e.GetForeignKeys()))
            {
                foreignKey.DeleteBehavior = DeleteBehavior.Restrict;
            }
        }
    }
}
