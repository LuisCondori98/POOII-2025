using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Simulacro.Models
{
    public class Producto
    {
        [Display(Name = "Id")] public int Id { get; set; }
        [Display(Name = "Nombre")] public string NombreProducto { get; set; }
        [Display(Name = "Precio")] public decimal Precio { get; set; }
        [Display(Name = "Stock")] public int Stock { get; set; }
        [Display(Name = "Desripcion")] public string Descripcion { get; set; }
    }
}