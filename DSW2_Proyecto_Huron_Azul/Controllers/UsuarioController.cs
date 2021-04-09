using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DSW2_Proyecto_Huron_Azul.localhost;

namespace DSW2_Proyecto_Huron_Azul.Controllers
{
    public class UsuarioController : Controller
    {

        Huron_Azul_Service ws = new Huron_Azul_Service();

        public ActionResult Editar()
        {
            return View((BeanUsuario)Session["Usuario"]);
        }

        [HttpPost]
        public ActionResult Editar(BeanUsuario u) 
        {
                     
            if (u.EMAIL == null)
            {
                u.EMAIL = "";
            }
            if (u.TELEFONO == null)
            {
                u.TELEFONO = "";
            }

            string msg = ws.u_editar(u);
            Session["Usuario"] = u;
            return RedirectToAction("Index", "Confirmacion", new { mensaje = msg });

        }


        public ActionResult Registrar() {
            return View(new BeanUsuario());        
        }

        [HttpPost]
        public ActionResult Registrar(BeanUsuario u) {
           
            if (u.EMAIL == null)
            {
                u.EMAIL = "";
            }
            if (u.TELEFONO == null)
            {
                u.TELEFONO = "";
            }

            string msg = ws.u_registrar(u);
            return RedirectToAction("Index", "Confirmacion", new { mensaje = msg });

        }
    }
}