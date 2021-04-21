using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hoca.Models
{
    public class MOgrenciRepository : IOgrenciRepository
    {
        private List<Ogrenci> _ogrenciList;

        public MOgrenciRepository()//Initilaize Ogrenci data
        {
            _ogrenciList = new List<Ogrenci>
            {
                new Ogrenci(){ Id=1, Ad="Engin", Soyad="Al", Email="engin58a@hotmail.com", Sifre="123", Sinif=Siniflar.Üç },
                new Ogrenci(){ Id=2, Ad="Lale", Soyad="Kale", Email="lale@hotmail.com", Sifre="123", Sinif=Siniflar.Yükseköğrenim }
            };
        }

        public IEnumerable<Ogrenci> GetAllOgrenci()
        {
            return _ogrenciList;
        }

        public Ogrenci GetOgrenci(int Id)
        {
            return _ogrenciList.FirstOrDefault(ogr => ogr.Id == Id);
        }

        public Ogrenci Guncelle(Ogrenci ogrenci)
        {
            Ogrenci ogren = _ogrenciList.FirstOrDefault(ogr => ogr.Id == ogrenci.Id);
            if (ogren != null)
            {
                ogren.Ad = ogrenci.Ad;
                ogren.Soyad = ogrenci.Soyad;
                ogren.Email = ogrenci.Email;
                ogren.Sifre = ogrenci.Sifre;
                ogren.Sinif = ogrenci.Sinif;
            }
            return ogren;
        }

        public Ogrenci Olustur(Ogrenci ogrenci)
        {
            ogrenci.Id = _ogrenciList.Max(ogr => ogr.Id) + 1;
            _ogrenciList.Add(ogrenci);
            return ogrenci;
        }

        public Ogrenci Sil(int id)
        {
            Ogrenci ogren = _ogrenciList.FirstOrDefault(ogr => ogr.Id == id);
            if(ogren != null)
            {
                _ogrenciList.Remove(ogren);
            }
            return ogren;
        }
    }
}
