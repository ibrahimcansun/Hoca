using Hoca.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hoca.Models
{
    public class SQLSoruRepository : ISorularRepository
    {
        private readonly AppDbContext context;

        public SQLSoruRepository(AppDbContext context)
        {
            this.context = context;
        }

        public IEnumerable<Sorular> GetAllSorular()
        {
            return context.Sorular;
        }
        /*
        public Sorular GetOgrenciSorular(string id)
        {
            var ogr = context.Ogrenciler.Find(id);
            var query = from b in context.Sorular
                        select context.Sorular.OrderBy(ogr, ogr.Id,);
            
            return context.Sorular.FirstOrDefault(ogr => ogr.OgrenciId1 == id);

        }*/

        public Sorular GetSoru(int Id)
        {
            return context.Sorular.Find(Id);
        }

        public Sorular Olustur(Sorular soru)
        {
            context.Sorular.Add(soru);
            context.SaveChanges();
            return soru;
        }

        public Sorular Guncelle(Sorular soru)
        {
            var question = context.Sorular.Attach(soru);
            question.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
            return soru;
        }

        public Sorular Sil(int id)
        {
            Sorular soru = context.Sorular.Find(id);
            if (soru != null)
            {
                context.Sorular.Remove(soru);
                context.SaveChanges();
            }
            return soru;
        }
    }
}
