using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NUEVO.EmlakOfisi.Case.Entity.DTO.User
{
    public class PasswordChangeDto
    {
        [Display(Name = "Eski Şifreniz")]
        [Required(ErrorMessage = "Eski şifreniz zorunlu"),DataType(DataType.Password)]
        public string OldPassword { get; set; }

        [Display(Name = "Yeni Şifreniz")]
        [Required(ErrorMessage = "Yeni şifreniz zorunludur"), DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [Display(Name = "Yeni Şifrenizin Tekrarı")]
        [Required(ErrorMessage = "Yeni şifrenizin tekrarı zorunludur"), DataType(DataType.Password)]
        [Compare("NewPassword",ErrorMessage = "Yeni şifreniz ve onay şifreniz birbirinden farklıdır!")]
        public string ConfirmPassword { get; set; }
    }
}
