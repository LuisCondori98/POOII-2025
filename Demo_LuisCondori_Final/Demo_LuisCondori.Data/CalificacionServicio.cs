using Demo_LuisCondori.Entidad.Abstraccion;
using Demo_LuisCondori.Entidad.Entidad;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo_LuisCondori.Data
{
    public class CalificacionServicio : ICalificacionServicio
    {
        readonly string connectionString = ConfigurationManager.ConnectionStrings["cadena"].ConnectionString;

        public IEnumerable<Calificacion> listarCalificaciones(string codigoAlumno = "")
        {
            List<Calificacion> temporal = new List<Calificacion>();

            SqlConnection cn = new SqlConnection(connectionString);

            cn.Open();

            SqlCommand cmd = new SqlCommand("sp_listar_notas_por_codigoalumno", cn);

            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@codigo", codigoAlumno);

            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                temporal.Add(new Calificacion()
                {
                    Id = dr.GetInt32(0),
                    CodigoAlumno = dr.GetString(1),
                    NombreEvaluacion = dr.GetString(2),
                    Nota = dr.GetDecimal(3)
                });

            }
            cn.Close();

            return temporal;
        }
    }
}
