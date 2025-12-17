using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo_LuisCondori.Entidad.Entidad
{
    public class Calificacion
    {
        public int Id { get; set; }
        public string CodigoAlumno { get; set; }
        public string NombreEvaluacion { get; set; }
        public decimal Nota { get; set; }
    }
}
