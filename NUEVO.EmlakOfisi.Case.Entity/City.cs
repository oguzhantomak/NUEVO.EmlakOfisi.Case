using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NUEVO.EmlakOfisi.Case.Entity
{
    public class City : BaseClass
    {
        /// <summary>
        /// Şehir adı
        /// </summary>
        public string CityName { get; set; }

        /// <summary>
        /// Şehir kodu-plakası
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Telefon alan kodu
        /// </summary>
        public string PhoneCode { get; set; }

        //Diğer tablolar ile kurulacak ilişkiler
        #region [ Table Integration ]

        #region [ Country ]

        //Şehirin ait olduğu ülkenin ID'si
        public int CountryId { get; set; }

        //City Sınıfının içerisinde o şehire ait ülkenin de yer alması için Country sınıfından bir property ekliyoruz ve şehri çağırdığımızda o şehre ait ülkenin de dolu gelebilmesi için virtual olarak işaretliyoruz
        public virtual Country Country { get; set; }

        #endregion

        #endregion
    }
}
