using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NUEVO.EmlakOfisi.Case.Entity
{
    public class BaseClass
    {
        public BaseClass()
        {
            CreatedDate = DateTime.UtcNow;
        }

        /// <summary>
        /// Kayda ait ID
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Kaydın oluşturulma tarihi
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Kaydın değiştirilme tarihi
        /// </summary>
        public DateTime? UpdatedDate { get; set; }

        /// <summary>
        /// Kaydın durumu: ÖRN: 1 = Active, 99 = Passive, 0 = Deleted
        /// </summary>
        public string Status { get; set; }
    }
}
