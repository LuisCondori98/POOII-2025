using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo_LuisCondori.Entidad.Entidad
{
    public class Alumno
    {
        public int Id { get; set; }
        public string Codigo { get; set; }
        public string Nombres { get; set; }
        public string ApePaterno { get; set; }
        public string ApeMaterno { get; set; }

        public int PromedioFinal { get; set; }
        public IList<Calificacion> Calificaciones { get; set; }

        public Alumno()
        {
            Calificaciones = new List<Calificacion>();
        }

        public string Apellidos
        {
            get
            {
                return ApePaterno + " " + ApeMaterno;
            }
        }

        public string Estado
        {
            get
            {
                return (PromedioFinal >= 13) ? "Aprobado" : "Desaprobado";
            }
        }
    }
}
