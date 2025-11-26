using Simulacro.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Simulacro.Controllers
{
    public class NegociosController : Controller
    {
        string connectionString = ConfigurationManager.ConnectionStrings["cadena"].ConnectionString;

        IEnumerable<Producto> productos()
        {
            List<Producto> temporal = new List<Producto>();

            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["cadena"].ConnectionString))
            {
                con.Open();

                SqlCommand cmd = new SqlCommand("EXEC spU_productos", con);

                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    temporal.Add(new Producto()
                    {
                        Id = dr.GetInt32(0),
                        NombreProducto = dr.GetString(1),
                        Precio = dr.GetDecimal(2),
                        Stock = dr.GetInt32(3),
                        Descripcion = dr.GetString(4),
                    });
                }

                con.Close();
            }

            return temporal;
        }

        public Producto getProductoById(int id)
        {
            Producto prod = new Producto();

            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["cadena"].ConnectionString))
            {
                con.Open();

                SqlCommand cmd = new SqlCommand("EXEC spU_productos_id @id", con);

                cmd.Parameters.AddWithValue("@id", id);

                SqlDataReader dr = cmd.ExecuteReader();

                if(dr.Read())
                {
                    prod.Id = dr.GetInt32(0);
                    prod.NombreProducto = dr.GetString(1);
                    prod.Precio = dr.GetDecimal(2);
                    prod.Stock = dr.GetInt32(3);
                    prod.Descripcion = dr.GetString(4);
                }

                con.Close();
            }

            return prod;
        }

        public Producto getProductoByName(string nombre)
        {
            Producto prod = new Producto();

            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["cadena"].ConnectionString))
            {
                con.Open();

                SqlCommand cmd = new SqlCommand("EXEC spU_productos_nombre @nombre", con);

                cmd.Parameters.AddWithValue("@nombre", nombre);

                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    prod.Id = dr.GetInt32(0);
                    prod.NombreProducto = dr.GetString(1);
                    prod.Precio = dr.GetDecimal(2);
                    prod.Stock = dr.GetInt32(3);
                    prod.Descripcion = dr.GetString(4);
                }

                con.Close();
            }

            return prod;
        }

        public ActionResult Index()
        {

            return View(productos());
        }

        public ActionResult Paginacion(int pagina = 0, /*string query = ""*/ int id = 0)
        {
            int cantRegistros = productos().Count();
            int nroRegMostrar = 5;

            int nroPags = cantRegistros % nroRegMostrar == 0 ?
                        cantRegistros / nroRegMostrar
                        :
                        cantRegistros / nroRegMostrar + 1;

            ViewBag.p = pagina;

            ViewBag.npags = nroPags;

            if (id != 0)
            {
                Producto prod = getProductoById(id);

                if (prod == null || prod.Id == 0)
                
                    return View(productos());

                return View(new List<Producto> { prod });
            }
            //else
            //{
            //    Producto prod = getProductoByName(query);

            //    return View(prod);
            //}

            return View(productos().Skip(nroRegMostrar * pagina).Take(nroRegMostrar));
        }

        public ActionResult Details(int id)
        {
            var prod = getProductoById(id);

            return View(prod);
        }
    }
}