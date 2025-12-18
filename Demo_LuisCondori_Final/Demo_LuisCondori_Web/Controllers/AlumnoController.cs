using Demo_LuisCondori.Data;
using Demo_LuisCondori.Entidad.Abstraccion;
using Demo_LuisCondori.Entidad.Entidad;
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
        ICalificacionServicio _calificacionServicio = new CalificacionServicio();

        public ActionResult Details(string codigo = "")
        {
            Alumno alumnoFind = _alumnoServicio.obtenerAlumno(codigo);
            alumnoFind.Calificaciones = _calificacionServicio.listarCalificaciones(codigo).ToList();

            return View(alumnoFind);
        }

        public ActionResult Index()
        {
            return View(_alumnoServicio.filtrarAlumnos());
        }

        public ActionResult Delete(string codigo = "")
        {
            string alumnoDelete = _alumnoServicio.eliminar_alumno(codigo);

            ViewBag.mensaje = alumnoDelete;

            return View("Index", _alumnoServicio.filtrarAlumnos());
        }

        public ActionResult Create()
        {

            return View(new Alumno());
        }

        [HttpPost]
        public ActionResult Create(Alumno alumno)
        {

            string alumnoCreate = _alumnoServicio.insertar_alumno(alumno);

            ViewBag.mensaje = alumnoCreate;

            return View(alumno);
        }

        public ActionResult AgregarNota()
        {

            return View(new Calificacion());
        }

        [HttpPost]
        public ActionResult AgregarNota(Calificacion calificacion)
        {

            return View(calificacion);
        }
    }
}