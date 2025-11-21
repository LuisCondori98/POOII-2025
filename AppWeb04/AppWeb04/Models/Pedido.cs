using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AppWeb04.Models
{
    public class Pedido
    {
        [Display(Name = "Id Pedido")]public int IdPedido { get; set; }
        [Display(Name = "Fecha pedido")] public DateTime Fecha { get; set; }
        [Display(Name = "Cliente")] public string Cliente { get; set; }
        [Display(Name = "Direccion destino")] public string Direccion { get; set; }
        [Display(Name = "Ciudad destino")] public string Ciudad { get; set; }
    }
}