using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DSW2_Proyecto_Huron_Azul.localhost;
using DSW2_Proyecto_Huron_Azul.Models;

namespace DSW2_Proyecto_Huron_Azul.Controllers
{
    public class HomeController : Controller
    {
        Huron_Azul_Service ws = new Huron_Azul_Service();
        public ActionResult Index()
        {
            if (Session["carrito"] == null) Session["carrito"] = new List<Carrito>();
            if (Session["Usuario"] == null) return View(ws.pro_listar("2","0",""));
            else
            {
                BeanUsuario u = (BeanUsuario)Session["Usuario"];
               return View(ws.pro_listar(u.TIPOUSUARIO.ToString(), "0", ""));       
            }

        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}