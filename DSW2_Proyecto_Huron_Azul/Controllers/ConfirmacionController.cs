using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DSW2_Proyecto_Huron_Azul.Controllers
{
    public class ConfirmacionController : Controller
    {
        // GET: Confirmacion
        public ActionResult Index(string mensaje)
        {
            ViewBag.mensaje = mensaje;
            return View();
        }
    }
}