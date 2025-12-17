using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SimulacroT2.Models
{
    public class Nota
    {
        [Display(Name = "Id")] public int Id { get; set; }
        [Display(Name = "Codigo")] public int CodigoAlumno { get; set; }
        [Display(Name = "Nota 1")] public double T1 { get; set; }
        [Display(Name = "Nota 2")] public double T2 { get; set; }
        [Display(Name = "Nota 3")] public double T3 { get; set; }
    }
}