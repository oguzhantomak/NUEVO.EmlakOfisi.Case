using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NUEVO.EmlakOfisi.Case.Entity.DTO.User
{
    public class CreateUserDto
    {
        [Required(ErrorMessage = "Kullanıcı adı gereklidir!"), StringLength(12, ErrorMessage = "3 karakterden uzun kullanıcı adı giriniz",MinimumLength = 3)]
        [Display(Name = "Kullanıcı Adı")]
        public string Username { get; set; }

        [Display(Name = "Şifre"), DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Firma adı gereklidir!")]
        [Display(Name = "Firma Adı")]
        public string FirmaAdi { get; set; }

        [Display(Name = "Ad")]
        public string Ad { get; set; }

        [Display(Name = "Soyad")]
        public string Soyad { get; set; }

        [Display(Name = "E-mail"), DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }
}
