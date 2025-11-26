using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AppWeb05.Models
{
    public class Producto
    {
        [Display(Name = "Id Producto")] public int IdProducto { get; set; }
        [Display(Name = "Descripcion Producto")] public string Descripcion { get; set; }
        [Display(Name = "Precio Unitario")] public decimal PreUni { get; set; }
        [Display(Name = "Unidades Disponibles")] public Int16 Stock { get; set; }
        [Display(Name = "Sub-Total")] public decimal SubTotal { get { return PreUni * Stock; } }
    }
}