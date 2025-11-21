using AppWeb04.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AppWeb04.Controllers
{
    public class NegociosController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        IEnumerable<Pedido> pedidosYear(int y)
        {
            List<Pedido> temp = new List<Pedido>();

            using(SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["cadena"].ConnectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("exec usp_pedidos_year @y", conn);

                cmd.Parameters.AddWithValue("@y", y);

                SqlDataReader rd = cmd.ExecuteReader();

                while (rd.Read())
                {
                    temp.Add(new Pedido()
                    {
                        IdPedido = rd.GetInt32(0),
                        Fecha = rd.GetDateTime(1),
                        Cliente = rd.GetString(2),
                        Direccion = rd.GetString(3),
                        Ciudad = rd.GetString(4),
                    });
                }

                conn.Close();
            }

            return temp;
        }

        IEnumerable<Pedido> pedidosFechas(DateTime f1, DateTime f2)
        {
            List<Pedido> temp = new List<Pedido>();

            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["cadena"].ConnectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("exec usp_pedidos_fechas @f1, @f2", conn);

                cmd.Parameters.AddWithValue("@f1", f1);
                cmd.Parameters.AddWithValue("@f2", f2);

                SqlDataReader rd = cmd.ExecuteReader();

                while (rd.Read())
                {
                    temp.Add(new Pedido()
                    {
                        IdPedido = rd.GetInt32(0),
                        Fecha = rd.GetDateTime(1),
                        Cliente = rd.GetString(2),
                        Direccion = rd.GetString(3),
                        Ciudad = rd.GetString(4),
                    });
                }

                conn.Close();
            }

            return temp;
        }
        
        IEnumerable<Producto> productos()
        {
            List<Producto> temp = new List<Producto>();

            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["cadena"].ConnectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("exec usp_productos", conn);

                SqlDataReader rd = cmd.ExecuteReader();

                while (rd.Read())
                {
                    temp.Add(new Producto()
                    {
                        IdProducto = rd.GetInt32(0),
                        Descripcion = rd.GetString(1),
                        PreUni = rd.GetDecimal(2),
                        Stock = rd.GetInt16(3)
                    });
                }

                conn.Close();
            }

            return temp;
        }

        public ActionResult ConsultaPedidosYear(int y=0)
        {

            return View(pedidosYear(y));
        }

        public ActionResult ConsultaPedidosFechas(DateTime? f1 = null, DateTime? f2 = null)
        {
            DateTime _f1 = f1 == null? DateTime.Today : (DateTime)f1;
            DateTime _f2 = f2 == null ? DateTime.Today : (DateTime)f2;

            return View(pedidosFechas(_f1, _f2));
        }

        public ActionResult Paginacion(int p = 0)
        {
            int c = productos().Count();
            int f = 10;

            int npags = c % f == 0 ? c / f : c / f + 1;

            ViewBag.p = p;

            ViewBag.npags = npags;

            return View(productos().Skip(f*p).Take(f));
        }
    }
}