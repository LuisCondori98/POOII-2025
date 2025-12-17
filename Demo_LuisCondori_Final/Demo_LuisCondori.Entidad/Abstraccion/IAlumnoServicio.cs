using Demo_LuisCondori.Entidad.Entidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo_LuisCondori.Entidad.Abstraccion
{
    public interface IAlumnoServicio
    {
        IEnumerable<Alumno> filtrarAlumnos(string filtro = "");
        Alumno obtenerAlumno(string codigo = "");
        string insertar_alumno(Alumno alumno);
        string eliminar_alumno(string codigoAlumno);
    }
}
