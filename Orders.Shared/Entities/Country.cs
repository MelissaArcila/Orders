using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orders.Shared.Entities
{
    public class Country
    {
        public int Id { get; set; }//va a ser la clave primaria, alfanumerico autoincremental

        //data notations de entidad Name
        [Display(Name ="País")]
        [MaxLength(100, ErrorMessage ="El campo {0} no puede tener más de {1} caracteres")]
        [Required(ErrorMessage ="El campo {0} es requerido")] //
        public string Name { get; set; } = null!; //le estamos diciendo que no es nullable
    }
}
