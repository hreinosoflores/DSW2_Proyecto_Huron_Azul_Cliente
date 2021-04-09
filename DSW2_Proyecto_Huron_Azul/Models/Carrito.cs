using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DSW2_Proyecto_Huron_Azul.Models
{
    public class Carrito
    {
        [Display(Name = "Nro. producto")]
        public long codigo { get; set; }
        [Display(Name = "Nombre")]
        public string descripcion { get; set; }
        [Display(Name = "Precio Unitario")]

        public decimal precio { get; set; }
        [Display(Name = "Cantidad solicitada")]

        public int cantidad { get; set; }
        [Display(Name = "Monto")]

        public decimal monto { get { return precio * cantidad;  }  } 


    }
}
