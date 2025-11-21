using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AppWeb04.Models
{
    public class Producto
    {
        [Display(Name = "Id producto")] public int IdProducto { get; set; }
        [Display(Name = "Descripcion")] public string Descripcion { get; set; }
        [Display(Name = "Precio unitario")] public decimal PreUni { get; set; }
        [Display(Name = "Unidades disponibles")] public Int16 Stock { get; set; }
        [Display(Name = "Sub total")] public decimal SubTotal { get { return PreUni * Stock; } }
    }
}