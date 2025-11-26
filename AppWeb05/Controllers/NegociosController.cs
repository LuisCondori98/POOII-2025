using AppWeb05.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AppWeb05.Controllers
{
    public class NegociosController : Controller
    {
        // GET: Negocios
        public ActionResult Index()
        {
            return View();
        }

        IEnumerable<Producto> productos()
        {
            List<Producto> temp = new List<Producto>();

            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["cadena"].ConnectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("exec spU_productos", conn);


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

        public Producto getProducto(int id)
        {
            Producto producto = new Producto();

            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["cadena"].ConnectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("exec spU_Producto_id @id", conn);

                cmd.Parameters.AddWithValue("@id", id);

                SqlDataReader rd = cmd.ExecuteReader();
                Console.WriteLine(producto);
                if (rd.Read())
                {
                    producto.IdProducto = rd.GetInt32(0);
                    producto.Descripcion = rd.GetString(1);
                    producto.PreUni = rd.GetDecimal(2);
                    producto.Stock = rd.GetInt16(3);
                }

                conn.Close();
            }

            return producto;
        }

        public ActionResult Paginacion(int p = 0)
        {
            int c = productos().Count();

            int f = 15;

            int npags = c % f == 0 ? c / f : c / f + 1;

            ViewBag.p = p;

            ViewBag.npags = npags;

            return View(productos().Skip(f * p).Take(f));
        }

        public ActionResult Details(int id)
        {
            Producto producto = getProducto(id);

            return View(producto);
        }


    }
}