using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NUEVO.EmlakOfisi.Case.Entity.DTO.Ilan
{
    public class UpdateIlanDto
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

        public double Metrekare { get; set; }

        public int Id { get; set; }

        public int CityId { get; set; }
        public int CountryId { get; set; }
        public int EmlakTuruId { get; set; }
        public string GorselLinki { get; set; }

        [Display(Name = "Ülke")]
        public List<Country> Countries { get; set; }

        [Display(Name = "Şehir")]
        public List<City> Cities { get; set; }

        [Display(Name = "Emlak Türü")]
        public List<EmlakTuru> EmlakTurus { get; set; }

    }
}
