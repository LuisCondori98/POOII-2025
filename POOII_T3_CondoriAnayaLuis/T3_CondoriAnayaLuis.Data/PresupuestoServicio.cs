using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using T3_CondoriAnayaLuis.Entidad.Abstraccion;
using T3_CondoriAnayaLuis.Entidad.Entidad;

namespace T3_CondoriAnayaLuis.Data
{
    public class PresupuestoServicio : IPresupuestoServicio
    {
        readonly string cadena = ConfigurationManager.ConnectionStrings["cadena"].ConnectionString;

        public IEnumerable<Presupuesto> filtrarPresupuestos(string filtro = "")
        {
            List<Presupuesto> listaPresupuestos = new List<Presupuesto>();

            SqlConnection cn = new SqlConnection(cadena);

            cn.Open();

            SqlCommand cmd = new SqlCommand("sp_listar_presupuesto", cn);

            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            //cmd.Parameters.AddWithValue("@filtro", filtro);

            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                listaPresupuestos.Add(new Presupuesto()
                {
                    Id = dr.GetInt32(0),
                    Nombre = dr.GetString(1),
                    Descripcion = dr.GetString(2),
                    Total = dr.GetDecimal(3),
                    FechaCreacion = dr.GetDateTime(4)
                });
            }

            dr.Close();
            cn.Close();

            return listaPresupuestos;
        }

        public string insertar_Presupuesto(Presupuesto presupuesto)
        {
            string mensaje = "";

            using (SqlConnection cn = new SqlConnection(cadena))
            {
                cn.Open();

                SqlTransaction transaction = cn.BeginTransaction(IsolationLevel.Serializable);

                try
                {
                    SqlCommand cmd_InsertarPresupuesto = new SqlCommand("sp_inserta_presupuesto");
                    cmd_InsertarPresupuesto.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd_InsertarPresupuesto.Connection = cn;
                    cmd_InsertarPresupuesto.Transaction = transaction;

                    cmd_InsertarPresupuesto.Parameters.AddWithValue("@Codigo", presupuesto.Id);
                    cmd_InsertarPresupuesto.Parameters.AddWithValue("@Nombres", presupuesto.Nombre);
                    cmd_InsertarPresupuesto.Parameters.AddWithValue("@ApePaterno", presupuesto.Descripcion);
                    cmd_InsertarPresupuesto.Parameters.AddWithValue("@ApeMaterno", presupuesto.Total);

                    int rowAffected_Presupuesto = cmd_InsertarPresupuesto.ExecuteNonQuery();


                    if (rowAffected_Presupuesto != 1)
                        throw new Exception();

                    foreach (var detalle in presupuesto.presupuestoDetalle)
                    {
                        SqlCommand cmd_InsertarNota = new SqlCommand("sp_inserta_detalle_presupuesto", cn);
                        cmd_InsertarNota.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd_InsertarNota.Connection = cn;
                        cmd_InsertarNota.Transaction = transaction;

                        cmd_InsertarNota.Parameters.AddWithValue("@idpresupuesto", presupuesto.Id);
                        cmd_InsertarNota.Parameters.AddWithValue("@nombre", detalle.Nombre);
                        cmd_InsertarNota.Parameters.AddWithValue("@descripcion", detalle.Descripcion);
                        cmd_InsertarNota.Parameters.AddWithValue("@subtotal", detalle.SubTotal);

                        int rowAffected_Nota = cmd_InsertarNota.ExecuteNonQuery();

                        if (rowAffected_Nota != 1)
                            throw new Exception();

                    }

                    transaction.Commit();
                    mensaje = $"Se ha insertado {rowAffected_Presupuesto} alumno con Codigo {presupuesto.Id}";
                }
                catch (Exception ex)
                {
                    mensaje = $"Hubo un error al registrar el cliente con Id {presupuesto.Id}" + ex.Message;
                    transaction.Rollback();
                }
                finally //ESTE CODIGO SE EJECUTA SIEMPRE
                {
                    cn.Close();
                }

            }

            return mensaje;
        }

        public Presupuesto obtenerPresupuesto(int id)
        {
            Presupuesto presupuestoDetalleFind = new Presupuesto();

            SqlConnection cn = new SqlConnection(cadena);

            cn.Open();

            SqlCommand cmd = new SqlCommand("SELECT * FROM tb_presupuesto WHERE IdPresupuesto = @Id", cn);

            //cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Id", id);

            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                presupuestoDetalleFind = new Presupuesto()
                {
                    Id = dr.GetInt32(0),
                    Nombre = dr.GetString(1),
                    Descripcion = dr.GetString(2),
                    Total = dr.GetDecimal(3)
                };
            }

            dr.Close();
            cn.Close();

            return presupuestoDetalleFind;
        }

        public string eliminar_Presupuesto(int id)
        {
            string mensaje = "";

            using (SqlConnection cn = new SqlConnection(cadena))
            {
                cn.Open();

                SqlTransaction transaction = cn.BeginTransaction(IsolationLevel.Serializable);

                try
                {

                    SqlCommand cmd_EliminarPresupuesto = new SqlCommand("sp_elimina_presupuesto");
                    cmd_EliminarPresupuesto.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd_EliminarPresupuesto.Connection = cn;
                    cmd_EliminarPresupuesto.Transaction = transaction;

                    cmd_EliminarPresupuesto.Parameters.AddWithValue("@idpresupuesto", id);

                    int rowAffected_Presupuesto = cmd_EliminarPresupuesto.ExecuteNonQuery();

                    transaction.Commit();
                    mensaje = $"“Presupuesto con Id {id} eliminado correctamente”,";
                }
                catch (Exception ex)
                {
                    mensaje = $"Hubo un error al eliminar el presupuesto con Id {id}" + ex.Message;
                    transaction.Rollback();
                }
                finally
                {
                    cn.Close();
                }

            }

            return mensaje;
        }
    }
}
