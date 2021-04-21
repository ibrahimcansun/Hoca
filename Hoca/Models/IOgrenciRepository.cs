using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hoca.Models
{
    public interface IOgrenciRepository
    {
        Ogrenci GetOgrenci(int Id);
        IEnumerable<Ogrenci> GetAllOgrenci();
        Ogrenci Olustur(Ogrenci ogrenci);
        Ogrenci Guncelle(Ogrenci ogrenci);
        Ogrenci Sil(int id);
    }
}
