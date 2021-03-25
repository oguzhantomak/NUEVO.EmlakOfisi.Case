using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NUEVO.EmlakOfisi.Case.Entity.DTO.User
{
    public class UserLoginDto
    {
        [Required(ErrorMessage = "Email alanı zorunludur"), DataType(DataType.EmailAddress)]
        [Display(Name = "Kullanıcı Adı")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Şifre zorunludur"), DataType(DataType.Password)]
        [Display(Name = "Şifre")]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}
