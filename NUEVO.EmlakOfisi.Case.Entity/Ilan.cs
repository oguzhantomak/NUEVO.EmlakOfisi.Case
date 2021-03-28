using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NUEVO.EmlakOfisi.Case.Entity
{
    public class Ilan : BaseClass
    {
        /// <summary>
        /// İlana ait başlık
        /// </summary>
        public string IlanBasligi { get; set; }

        /// <summary>
        /// İlanın açıklaması
        /// </summary>
        public string IlanIcerigi { get; set; }

        /// <summary>
        /// İlanın fiyatı
        /// </summary>
        public double Fiyat { get; set; }

        /// <summary>
        /// İlanın türü. Satılık, kiralık True satılık, false kiralık
        /// </summary>
        public bool Tur { get; set; }

        /// <summary>
        /// İlana ait oda sayısı
        /// </summary>
        public byte OdaSayisi { get; set; }

        /// <summary>
        /// İlanın banyo sayısı
        /// </summary>
        public byte BanyoSayisi { get; set; }

        /// <summary>
        /// İlanın bulunduğu kat
        /// </summary>
        public byte? BulunduguKat { get; set; }

        /// <summary>
        /// İlandaki emlağın bulunduğu binanın kat sayısı
        /// </summary>
        public byte? BinaToplamKatSayisi { get; set; }

        public string GorselLinki { get; set; }

        public int EmlakYasi { get; set; }

        public double Metrekare { get; set; }

        #region [ Integrations ]

        #region [ Country ]

        //İlanın ait olduğu ülkenin ID'si
        [ForeignKey("Country")]
        public int CountryId { get; set; }

        public virtual Country Country { get; set; }

        #endregion

        #region [ City ]

        //İlanın ait olduğu şehrin ID'si
        [ForeignKey("City")]
        public int CityId { get; set; }

        public virtual City City { get; set; }

        #endregion

        #region [ User ]

        //İlanın sahibinin ID'si
        [ForeignKey("User")]
        public int UserId { get; set; }

        public virtual User User { get; set; }

        #endregion

        #region [ EmlakTuru ]

        
        [ForeignKey("EmlakTuru")]
        public int EmlakTuruId { get; set; }

        public virtual EmlakTuru EmlakTuru { get; set; }


        #endregion

        #endregion
    }
}
