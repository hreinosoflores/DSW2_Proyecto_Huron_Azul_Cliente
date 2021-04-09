using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DSW2_Proyecto_Huron_Azul.localhost;

namespace DSW2_Proyecto_Huron_Azul.Controllers
{
    public class LogueoController : Controller
    {
        Huron_Azul_Service ws = new Huron_Azul_Service();

        // GET: Logueo
        public ActionResult Index(string mensaje)
        {
            ViewBag.mensaje = mensaje;
            return View();
        }

        public ActionResult Log_In(string id, string pwd) {

            BeanUsuario u = ws.u_logueo(id, pwd);
            if (u.CODUSUARIO==null) return RedirectToAction("Index", new { mensaje = "Logueo Incorrecto" });
            Session["Usuario"] = u;
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Log_Out() {
            Session["Usuario"] = null;
            return RedirectToAction("Index", "Home");
        }
    }
}