using Demo_LuisCondori.Entidad.Entidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo_LuisCondori.Entidad.Abstraccion
{
    public interface ICalificacionServicio
    {
        IEnumerable<Calificacion> listarCalificaciones(string codigoAumno = "");
    }
}
