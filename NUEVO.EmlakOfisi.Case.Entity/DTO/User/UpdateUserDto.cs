using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NUEVO.EmlakOfisi.Case.Entity.DTO.User
{
    public class UpdateUserDto
    {
        public string Ad { get; set; }

        public string Soyad { get; set; }

        public string FirmaAdi { get; set; }

        public string Email { get; set; }

        public string UserName { get; set; }

        public int Id { get; set; }
    }
}
