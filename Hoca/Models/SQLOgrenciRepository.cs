using Hoca.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hoca.Models
{
    public class SQLOgrenciRepository : IOgrenciRepository
    {
        private readonly AppDbContext context;

        public SQLOgrenciRepository(AppDbContext context)
        {
            this.context = context;
        }

        public IEnumerable<Ogrenci> GetAllOgrenci()
        {
            return context.Ogrenciler;
        }

        public Ogrenci GetOgrenci(int Id)
        {
            return context.Ogrenciler.Find(Id);
        }

        public Ogrenci Guncelle(Ogrenci ogrenci)
        {
            var ogr = context.Ogrenciler.Attach(ogrenci);
            ogr.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
            return ogrenci;
        }

        public Ogrenci Olustur(Ogrenci ogrenci)
        {
            context.Ogrenciler.Add(ogrenci);
            context.SaveChanges();
            return ogrenci;
        }

        public Ogrenci Sil(int id)
        {
            Ogrenci ogr = context.Ogrenciler.Find(id);
            if(ogr != null)
            {
                context.Ogrenciler.Remove(ogr);
                context.SaveChanges();
            }
            return ogr;
        }
    }
}
