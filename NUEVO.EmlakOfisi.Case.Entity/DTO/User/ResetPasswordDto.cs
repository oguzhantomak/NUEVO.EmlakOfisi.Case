using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NUEVO.EmlakOfisi.Case.Entity.DTO.User
{
    public class ResetPasswordDto
    {
        [Required(ErrorMessage = "Email alanı zorunludur"), DataType(DataType.EmailAddress)]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Şifre zorunludur"), DataType(DataType.Password)]
        [Display(Name = "Yeni Şifre")]
        public string Password { get; set; }
    }
}
