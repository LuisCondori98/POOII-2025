using Demo_LuisCondori.Data;
using Demo_LuisCondori.Entidad.Abstraccion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Demo_LuisCondori_Web.Controllers
{

    public class AlumnoController : Controller
    {
        IAlumnoServicio _alumnoServicio = new AlumnoServicio();

        public ActionResult Index()
        {
            return View(_alumnoServicio.filtrarAlumnos());
        }
    }
}