using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SimulacroT2.Models
{
    public class Alumno : DatosPromedio
    {
        [Display(Name = "Id")] public int Id { get; set; }
        [Display(Name = "Codigo")] public string Codigo { get; set; }
        [Display(Name = "Nombres")] public string Nombres { get; set; }
        [Display(Name = "Apellido Paterno")] public string ApePaterno { get; set; }
        [Display(Name = "Apellido Materno")] public string ApeMaterno { get; set; }

        [Display(Name = "Apellidos")]
        public string Apellidos { 
            get
            {
                return ApePaterno + " " + ApeMaterno;
            }
        }

        public decimal Promedio
        {
            get
            {
                return (T1 + T2 + T3) / 3;
            }
        }

        public string Estado
        {
            get
            {
                return Promedio >= 13 ? "Aprobado" : "Desaprobado";
            }
        }
    }
}