using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NUEVO.EmlakOfisi.Case.Entity
{
    public class Country : BaseClass
    {
        /// <summary>
        /// Ülke Adı
        /// </summary>
        public string CountryName { get; set; }

        //Diğer tablolar ile kurulacak ilişkiler
        #region [ Table Integration ]

        #region [ City ]

        // Bir ülkenin birden fazla şehri vardır.
        public virtual ICollection<City> Cities { get; set; }

        #endregion

        #endregion
    }
}
