using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DSW2_Proyecto_Huron_Azul.localhost;

namespace DSW2_Proyecto_Huron_Azul.Controllers
{
    public class CitaController : Controller
    {
        Huron_Azul_Service ws = new Huron_Azul_Service();

        List<SelectListItem> Lista_Horas = new List<SelectListItem> {
            new SelectListItem{Selected=true,Value="00:00:00",Text="Seleccione una hora" },
            new SelectListItem{Selected=false,Value="10:00:00",Text="10:00 am" },
            new SelectListItem{Selected=false,Value="10:30:00",Text="10:30 am" },
            new SelectListItem{Selected=false,Value="11:00:00",Text="11:00 am" },
            new SelectListItem{Selected=false,Value="11:30:00",Text="11:30 am" },
            new SelectListItem{Selected=false,Value="12:00:00",Text="12:00 pm" },
            new SelectListItem{Selected=false,Value="12:30:00",Text="12:30 pm" },
            new SelectListItem{Selected=false,Value="13:00:00",Text="01:00 pm" },
            new SelectListItem{Selected=false,Value="13:30:00",Text="01:30 pm" },
            new SelectListItem{Selected=false,Value="14:00:00",Text="02:00 pm" },
            new SelectListItem{Selected=false,Value="14:30:00",Text="02:30 pm" },
            new SelectListItem{Selected=false,Value="15:00:00",Text="03:00 pm" },
            new SelectListItem{Selected=false,Value="15:30:00",Text="03:30 pm" },
            new SelectListItem{Selected=false,Value="16:00:00",Text="04:00 pm" },
            new SelectListItem{Selected=false,Value="16:30:00",Text="04:30 pm" },
            new SelectListItem{Selected=false,Value="17:00:00",Text="05:00 pm" },
            new SelectListItem{Selected=false,Value="17:30:00",Text="05:30 pm" },
            new SelectListItem{Selected=false,Value="18:00:00",Text="06:00 pm" },
            new SelectListItem{Selected=false,Value="18:30:00",Text="06:30 pm" },
            new SelectListItem{Selected=false,Value="19:00:00",Text="07:00 pm" },
            new SelectListItem{Selected=false,Value="19:30:00",Text="07:30 pm" }        
            };

        public ActionResult Registrar()
        {

            BeanUsuario u_sesion = (BeanUsuario)Session["Usuario"];
            if (u_sesion == null) return RedirectToAction("Index", "Logueo");
            ViewBag.horas = new SelectList(Lista_Horas, "Value", "Text");
            ViewBag.sedes = new SelectList(ws.s_listar(), "CODSEDE", "REFSEDE");
            BeanCita c = new BeanCita();
            c.CLIENTE = u_sesion.CODUSUARIO;
            return View(c);
        }

        [HttpPost]
        public ActionResult Registrar(BeanCita c, DateTime fecha, string hora)
        {
            c.FECHA_HORA = fecha.ToString("yyyy-M-d ") + hora;
            string msg = ws.ci_registrar(c);
            ViewBag.horas = new SelectList(Lista_Horas, "Value", "Text", hora);
            ViewBag.sedes = new SelectList(ws.s_listar(), "CODSEDE", "REFSEDE", c.SEDE);
            return RedirectToAction("Index", "Confirmacion", new { mensaje = msg });

        }

        public ActionResult Listar(string usuario)
        {
            BeanCitaTuneado[] lista = ws.ci_listar(usuario);
            if (lista == null) return RedirectToAction("Index", "Home");
            return View(lista);
        }

        public ActionResult Detalle(string nrocita)
        {
            return View(ws.ci_detalle(nrocita));
        }

        public ActionResult Cancelar(string nrocita)
        {
            string msg = ws.ci_cancelar(nrocita);
            return RedirectToAction("Index", "Confirmacion", new { mensaje = msg });
        }
    }
}