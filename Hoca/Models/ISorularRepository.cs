using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hoca.Models
{
    public interface ISorularRepository
    {
        Sorular GetSoru(int Id);
        IEnumerable<Sorular> GetAllSorular();
        //Sorular GetOgrenciSorular(string id);
        Sorular Olustur(Sorular sorular);
        Sorular Guncelle(Sorular sorular);
        Sorular Sil(int Id);
    }
}
