using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using T3_CondoriAnayaLuis.Entidad.Abstraccion;
using T3_CondoriAnayaLuis.Entidad.Entidad;

namespace T3_CondoriAnayaLuis.Data
{
    public class PresupuestoDetalleServicio : IPresupuestoDetalleServicio
    {
        readonly string cadena = ConfigurationManager.ConnectionStrings["cadena"].ConnectionString;

        public IEnumerable<PresupuestoDetalle> presupuestoDetalles(int id)
        {
            List<PresupuestoDetalle> listaPresupuestosDetalle = new List<PresupuestoDetalle>();

            SqlConnection cn = new SqlConnection(cadena);

            cn.Open();

            SqlCommand cmd = new SqlCommand("sp_listar_detallepresupuesto", cn);

            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@idpresupuesto", id);

            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                listaPresupuestosDetalle.Add(new PresupuestoDetalle()
                {
                    Id = dr.GetInt32(0),
                    Nombre = dr.GetString(1),
                    Descripcion = dr.GetString(2),
                    SubTotal = dr.GetDecimal(3)
                });
            }

            dr.Close();
            cn.Close();

            return listaPresupuestosDetalle;
        }
    }
}
