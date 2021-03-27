using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace NUEVO.EmlakOfisi.Case.Entity
{
    public class Role : IdentityRole<int>
    {
        public Role()
        {
            CreatedDate = DateTime.UtcNow;
        }
        /// <summary>
        /// Kaydın oluşturulma tarihi
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Kaydın güncellenme tarihi
        /// </summary>
        public DateTime UpdatedDate { get; set; }
    }
}
