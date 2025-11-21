using AppWeb02.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AppWeb02.Controllers
{
    public class UsuarioController : Controller
    {
        public IEnumerable<Usuario> usuarios = new List<Usuario>()
        {
            new Usuario() {name="Luis", lastname = "Condori", age = 27, email = "lcond@gmail.com", address = "Psj Belen 897"},
            new Usuario() {name="Jorge", lastname = "Cubas", age = 29, email = "jcub_88@gmail.com", address = "Av Peru 489"},
            new Usuario() {name="Sandra", lastname = "Meza", age = 35, email = "sandra_1990@gmail.com", address = "Jr Union 203"}
        };

        // GET: Usuario
        public ActionResult Index()
        {
            return View(usuarios);
        }
    }
}