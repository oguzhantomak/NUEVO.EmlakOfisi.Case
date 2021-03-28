using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NUEVO.EmlakOfisi.Case.Entity.DTO.Role
{
    public class CreateRoleDto
    {
        [Display(Name = "Rol ismi")]
        [Required(ErrorMessage = "Rol adı gereklidir")]
        public string Name { get; set; }

        public string Id { get; set; }
    }
}
