using Demo_LuisCondori.Entidad.Abstraccion;
using Demo_LuisCondori.Entidad.Entidad;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo_LuisCondori.Data
{
    public class AlumnoServicio : IAlumnoServicio
    {
        readonly string cadena = ConfigurationManager.ConnectionStrings["cadena"].ConnectionString;

        public IEnumerable<Alumno> filtrarAlumnos(string filtro = "")
        {
            List<Alumno> listaAlumnos = new List<Alumno>();

            SqlConnection cn = new SqlConnection(cadena);

            cn.Open();

            SqlCommand cmd = new SqlCommand("sp_listar_alumnos", cn);

            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@filtro", filtro);

            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                listaAlumnos.Add(new Alumno()
                {
                    Id = dr.GetInt32(0),
                    Codigo = dr.GetString(1),
                    Nombres = dr.GetString(2),
                    ApePaterno = dr.GetString(3),
                    ApeMaterno = dr.GetString(4),
                    PromedioFinal = dr.GetInt32(5)
                });
            }

            dr.Close();
            cn.Close();

            return listaAlumnos;
        }

        public Alumno obtenerAlumno(string codigo = "")
        {
            Alumno alumno = new Alumno();
            SqlConnection cn = new SqlConnection(cadena);
            cn.Open();
            SqlCommand cmd = new SqlCommand("sp_obtener_alumno", cn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@codigo", codigo);

            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                alumno = new Alumno()
                {
                    Id = dr.GetInt32(0),
                    Codigo = dr.GetString(1),
                    Nombres = dr.GetString(2),
                    ApePaterno = dr.GetString(3),
                    ApeMaterno = dr.GetString(4),
                    PromedioFinal = dr.GetInt32(5)
                };
            }

            dr.Close();
            cn.Close();

            return alumno;
        }

        public string insertar_alumno(Alumno alumno)
        {
            string mensaje = "";

            using (SqlConnection cn = new SqlConnection(cadena))
            {
                cn.Open();

                SqlTransaction transaction = cn.BeginTransaction(IsolationLevel.Serializable);

                try
                {

                    SqlCommand cmd_InsertarAlumno = new SqlCommand("sp_insertar_alumno");
                    cmd_InsertarAlumno.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd_InsertarAlumno.Connection = cn;
                    cmd_InsertarAlumno.Transaction = transaction;

                    cmd_InsertarAlumno.Parameters.AddWithValue("@Codigo", alumno.Codigo);
                    cmd_InsertarAlumno.Parameters.AddWithValue("@Nombres", alumno.Nombres);
                    cmd_InsertarAlumno.Parameters.AddWithValue("@ApePaterno", alumno.ApePaterno);
                    cmd_InsertarAlumno.Parameters.AddWithValue("@ApeMaterno", alumno.ApeMaterno);

                    int rowAffected_Alumno = cmd_InsertarAlumno.ExecuteNonQuery();


                    if (rowAffected_Alumno != 1)
                        throw new Exception();

                    foreach (var calificacion in alumno.Calificaciones)
                    {
                        SqlCommand cmd_InsertarNota = new SqlCommand("sp_insertar_nota", cn);
                        cmd_InsertarNota.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd_InsertarNota.Connection = cn;
                        cmd_InsertarNota.Transaction = transaction;

                        cmd_InsertarNota.Parameters.AddWithValue("@Codigo", alumno.Codigo);
                        cmd_InsertarNota.Parameters.AddWithValue("@NombreEvaluacion", calificacion.NombreEvaluacion);
                        cmd_InsertarNota.Parameters.AddWithValue("@Nota", calificacion.Nota);

                        int rowAffected_Nota = cmd_InsertarNota.ExecuteNonQuery();

                        if (rowAffected_Nota != 1)
                            throw new Exception();

                    }

                    transaction.Commit();
                    mensaje = $"Se ha insertado {rowAffected_Alumno} alumno con Codigo {alumno.Codigo}";
                }
                catch (Exception ex)
                {
                    mensaje = $"Hubo un error al registrar el cliente con Id {alumno.Codigo}" + ex.Message;
                    transaction.Rollback();
                }
                finally //ESTE CODIGO SE EJECUTA SIEMPRE
                {
                    cn.Close();
                }

            }

            return mensaje;
        }

        public string eliminar_alumno(string codigoAlumno)
        {
            string mensaje = "";

            using (SqlConnection cn = new SqlConnection(cadena))
            {
                cn.Open();

                SqlTransaction transaction = cn.BeginTransaction(IsolationLevel.Serializable);

                try
                {

                    SqlCommand cmd_EliminarAlumno = new SqlCommand("sp_eliminar_alumno");
                    cmd_EliminarAlumno.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd_EliminarAlumno.Connection = cn;
                    cmd_EliminarAlumno.Transaction = transaction;

                    cmd_EliminarAlumno.Parameters.AddWithValue("@Codigo", codigoAlumno);

                    int rowAffected_Alumno = cmd_EliminarAlumno.ExecuteNonQuery();


                    SqlCommand cmd_EliminarNota = new SqlCommand("sp_eliminar_notas_por_codigoalumno");
                    cmd_EliminarNota.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd_EliminarNota.Connection = cn;
                    cmd_EliminarNota.Transaction = transaction;

                    cmd_EliminarNota.Parameters.AddWithValue("@Codigo", codigoAlumno);

                    int rowAffected_Nota = cmd_EliminarNota.ExecuteNonQuery();

                    transaction.Commit();
                    mensaje = $"Se ha eliminado {rowAffected_Alumno} alumno con Codigo {codigoAlumno}";
                }
                catch (Exception ex)
                {
                    mensaje = $"Hubo un error al eliminar el cliente con Id {codigoAlumno}";
                    transaction.Rollback();
                }
                finally //ESTE CODIGO SE EJECUTA SIEMPRE
                {
                    cn.Close();
                }

            }

            return mensaje;
        }
    }
}
