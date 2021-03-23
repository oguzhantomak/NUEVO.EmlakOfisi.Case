using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace NUEVO.EmlakOfisi.Case.Entity
{
    public class User : IdentityUser<int>
    {

        /// <summary>
        /// Kaydın oluşturulma tarihi
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Kaydın güncellenme tarihi
        /// </summary>
        public DateTime UpdatedDate { get; set; }

        /// <summary>
        /// Eğer kullanıcı Emlakçı ise firma adı girebilir
        /// </summary>
        public string FirmaAdi { get; set; }
    }
}
