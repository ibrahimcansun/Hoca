using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Hoca.Models
{
    public static class ClaimsStore
    {
        public static List<Claim> AllClaims = new List<Claim>()
        {
            new Claim("Rol Oluştur","Rol Oluştur"),
            new Claim("Rol Düzenle","Rol Düzenle"),
            new Claim("Rol Sil","Rol Sil")
        };


    }
}
