using Hoca.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hoca.ViewModels
{
    public class SoruOlusturViewModel
    {
        public string SoruBaslik { get; set; }

        public string SoruIcerik { get; set; }

        public SoruKategori SoruKategori { get; set; }

        public string OgrenciId { get; set; }
    }
}
