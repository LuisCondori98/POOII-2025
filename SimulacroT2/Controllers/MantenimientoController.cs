using SimulacroT2.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SimulacroT2.Controllers
{
    public class MantenimientoController : Controller
    {
        readonly string connectionString = ConfigurationManager.ConnectionStrings["cadena"].ConnectionString;

        IEnumerable<Alumno> alumnos()
        {
            List<Alumno> temporal = new List<Alumno>();

            SqlConnection cn = new SqlConnection(connectionString);
            cn.Open();

            SqlCommand cmd = new SqlCommand("exec spU_Alumnos", cn);
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                temporal.Add(new Alumno()
                {
                    Id = dr.GetInt32(0),
                    Codigo = dr.GetString(1),
                    Nombres = dr.GetString(2),
                    ApePaterno = dr.GetString(3),
                    ApeMaterno = dr.GetString(4),
                    T1 = dr.GetDecimal(5),
                    T2 = dr.GetDecimal(6),
                    T3 = dr.GetDecimal(7),
                });

            }
            cn.Close();

            return temporal;
        }

        IEnumerable<Alumno> filtrarAlumnos(string filtro)
        {
            List<Alumno> temporal = new List<Alumno>();

            SqlConnection cn = new SqlConnection(connectionString);
            cn.Open();

            SqlCommand cmd = new SqlCommand("exec spU_filtrar @fil", cn);

            cmd.Parameters.AddWithValue("@fil", filtro);

            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                temporal.Add(new Alumno()
                {
                    Id = dr.GetInt32(0),
                    Codigo = dr.GetString(1),
                    Nombres = dr.GetString(2),
                    ApePaterno = dr.GetString(3),
                    ApeMaterno = dr.GetString(4),
                    T1 = dr.GetDecimal(5),
                    T2 = dr.GetDecimal(6),
                    T3 = dr.GetDecimal(7),
                });

            }
            cn.Close();

            return temporal;
        }

        string insertar_alumno(Alumno alumno)
        {
            string mensaje = "";
            SqlConnection cn = new SqlConnection(connectionString);
            cn.Open();
            SqlTransaction transaction = cn.BeginTransaction(IsolationLevel.Serializable);

            try
            {

                SqlCommand cmd_InsertarAlumno = new SqlCommand("spU_insertar_alumno");
                cmd_InsertarAlumno.CommandType = System.Data.CommandType.StoredProcedure;
                cmd_InsertarAlumno.Connection = cn;
                cmd_InsertarAlumno.Transaction = transaction;

                cmd_InsertarAlumno.Parameters.AddWithValue("@Codigo", alumno.Codigo);
                cmd_InsertarAlumno.Parameters.AddWithValue("@Nombres", alumno.Nombres);
                cmd_InsertarAlumno.Parameters.AddWithValue("@apepatern", alumno.ApePaterno);
                cmd_InsertarAlumno.Parameters.AddWithValue("@apematern", alumno.ApeMaterno);

                int rowAffected_Alumno = cmd_InsertarAlumno.ExecuteNonQuery();

                SqlCommand cmd_InsertarNota = new SqlCommand("spU_insertar_nota", cn);
                cmd_InsertarNota.CommandType = System.Data.CommandType.StoredProcedure;
                cmd_InsertarNota.Connection = cn;
                cmd_InsertarNota.Transaction = transaction;

                cmd_InsertarNota.Parameters.AddWithValue("@CodigoAlumno", alumno.Codigo);
                cmd_InsertarNota.Parameters.AddWithValue("@T1", alumno.T1);
                cmd_InsertarNota.Parameters.AddWithValue("@T2", alumno.T2);
                cmd_InsertarNota.Parameters.AddWithValue("@T3", alumno.T3);

                int rowAffected_Nota = cmd_InsertarNota.ExecuteNonQuery();

                transaction.Commit();

                if (rowAffected_Alumno == 1 && rowAffected_Nota == 1)
                    mensaje = $"Se ha insertado {rowAffected_Alumno} alumno con Codigo {alumno.Codigo}";
                else
                    mensaje = $"Hubo un error al registrar el cliente con Id {alumno.Codigo}";
            }
            catch (SqlException ex)
            {
                mensaje = ex.Message;
                transaction.Rollback();
            }
            finally //ESTE CODIGO SE EJECUTA SIEMPRE
            {
                cn.Close();
            }

            return mensaje;
        }

        public Alumno getAlumnoById(int id)
        {
            Alumno alumno = new Alumno();

            SqlConnection cn = new SqlConnection(connectionString);
            cn.Open();

            SqlCommand cmd = new SqlCommand("exec spU_alumno_id @id", cn);

            cmd.Parameters.AddWithValue("@id", id);

            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                alumno.Id = dr.GetInt32(0);
                alumno.Codigo = dr.GetString(1);
                alumno.Nombres = dr.GetString(2);
                alumno.ApePaterno = dr.GetString(3);
                alumno.ApeMaterno = dr.GetString(4);
                alumno.T1 = dr.GetDecimal(5);
                alumno.T2 = dr.GetDecimal(6);
                alumno.T3 = dr.GetDecimal(7);

            }

            cn.Close();

            return alumno;
        }

        string editarAlumno(Alumno alumno)
        {
            string mensaje = "";
            SqlConnection cn = new SqlConnection(connectionString);
            cn.Open();
            SqlTransaction transaction = cn.BeginTransaction(IsolationLevel.Serializable);

            try
            {

                SqlCommand cmd_InsertarAlumno = new SqlCommand("spU_editar_alumno");
                cmd_InsertarAlumno.CommandType = System.Data.CommandType.StoredProcedure;
                cmd_InsertarAlumno.Connection = cn;
                cmd_InsertarAlumno.Transaction = transaction;

                cmd_InsertarAlumno.Parameters.AddWithValue("@Id", alumno.Id);
                cmd_InsertarAlumno.Parameters.AddWithValue("@Codigo", alumno.Codigo);
                cmd_InsertarAlumno.Parameters.AddWithValue("@Nombres", alumno.Nombres);
                cmd_InsertarAlumno.Parameters.AddWithValue("@ApePatern", alumno.ApePaterno);
                cmd_InsertarAlumno.Parameters.AddWithValue("@ApeMatern", alumno.ApeMaterno);

                int rowAffected_Alumno = cmd_InsertarAlumno.ExecuteNonQuery();

                SqlCommand cmd_InsertarNota = new SqlCommand("spU_insertar_nota", cn);
                cmd_InsertarNota.CommandType = System.Data.CommandType.StoredProcedure;
                cmd_InsertarNota.Connection = cn;
                cmd_InsertarNota.Transaction = transaction;

                cmd_InsertarNota.Parameters.AddWithValue("@CodigoAlumno", alumno.Codigo);
                cmd_InsertarNota.Parameters.AddWithValue("@T1", alumno.T1);
                cmd_InsertarNota.Parameters.AddWithValue("@T2", alumno.T2);
                cmd_InsertarNota.Parameters.AddWithValue("@T3", alumno.T3);

                int rowAffected_Nota = cmd_InsertarNota.ExecuteNonQuery();

                transaction.Commit();

                if (rowAffected_Alumno == 1 && rowAffected_Nota == 1)
                    mensaje = $"Se ha insertado {rowAffected_Alumno} alumno con Codigo {alumno.Codigo}";
                else
                    mensaje = $"Hubo un error al registrar el cliente con Id {alumno.Codigo}";
            }
            catch (SqlException ex)
            {
                mensaje = ex.Message;
                transaction.Rollback();
            }
            finally //ESTE CODIGO SE EJECUTA SIEMPRE
            {
                cn.Close();
            }

            return mensaje;
        }

        // GET: Mantenimiento
        public ActionResult Index(string filtro)
        {
            if(filtro != null)
            {
                return View(filtrarAlumnos(filtro));
            }


            return View(alumnos());
        }

        public ActionResult Create()
        {

            return View(new Alumno());
        }

        [HttpPost]
        public ActionResult Create(Alumno al)
        {
            ViewBag.mensaje = insertar_alumno(al);

            return View(al);
        }

        public ActionResult Details(int id)
        {
            Alumno alumno = getAlumnoById(id);

            return View(alumno);
        }

        public ActionResult Edit(int id)
        {
            Alumno alumno = getAlumnoById(id);

            return View(alumno);
        }

        [HttpPost]
        public ActionResult Edit(Alumno al)
        {
            string mensaje = editarAlumno(al);

            ViewBag.mensaje = mensaje;

            return View(al);
        }

        public ActionResult Delete(int id)
        {
            return View();
        }
    }
}