using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NUEVO.EmlakOfisi.Case.Entity.DTO.Ilan
{
    public class ListIlanProfileDto
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
        /// İlanın türü. Satılık, kiralık. True satılık, false kiralık
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

        /// <summary>
        /// İlandaki emlağın türü: ev, ofis vb.
        /// </summary>
        public int EmlakTuruId { get; set; }


        public string CountryName { get; set; }

        public string CityName { get; set; }

        public string EmlakTuru { get; set; }

        public int UserId { get; set; }

        public string GorselLinki { get; set; }
    }
}
