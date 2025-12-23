using Microsoft.Ajax.Utilities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using T3_CondoriAnayaLuis.Data;
using T3_CondoriAnayaLuis.Entidad.Abstraccion;
using T3_CondoriAnayaLuis.Entidad.Entidad;

namespace POOII_T3_Web.Controllers
{
    public class PresupuestoController : Controller
    {
        IPresupuestoServicio _presupuestoServicio = new PresupuestoServicio();
        IPresupuestoDetalleServicio _presupuestoDetalleServicio = new PresupuestoDetalleServicio();

        public ActionResult Create()
        {
            List<PresupuestoDetalle> listaPresupuestoDetalle = new List<PresupuestoDetalle>();

            if (Session["listaPresDetalle"] != null)

                listaPresupuestoDetalle = JsonConvert.DeserializeObject<List<PresupuestoDetalle>>(Session["listaPresDetalle"].ToString());

            ViewBag.listaPreDetalle = listaPresupuestoDetalle;

            return View(new Presupuesto());
        }

        public ActionResult Index()
        {
            IEnumerable<Presupuesto> presupuesto = _presupuestoServicio.filtrarPresupuestos();

            return View(presupuesto);
        }

        public ActionResult Delete(int id)
        {
            string presupuestoDelete = _presupuestoServicio.eliminar_Presupuesto(id);

            ViewBag.mensaje = presupuestoDelete;

            return View("Index", _presupuestoServicio.filtrarPresupuestos());
        }

        public ActionResult Details(int id)
        {
            IEnumerable<PresupuestoDetalle> pDetalle = _presupuestoDetalleServicio.presupuestoDetalles(id);

            ViewBag.id = id;
            
            return View(pDetalle);
        }

        public ActionResult AgregarDetalle()
        {

            return View(new PresupuestoDetalle());
        }

        /*[HttpPost]
        public ActionResult(Presupuesto presupuesto)
        {

            return 
        }*/
    }
}