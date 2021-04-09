using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using DSW2_Proyecto_Huron_Azul.localhost;

namespace DSW2_Proyecto_Huron_Azul.Controllers
{
    public class ProductoController : Controller
    {
        Huron_Azul_Service ws = new Huron_Azul_Service();

        public ActionResult Registrar()
        {
            SelectList cboCat = new SelectList(ws.cat_listar(), "CODCAT", "DESCCAT");
            ViewBag.cat = cboCat;
            BeanProducto p = new BeanProducto();
            return View(p);
        }

        [HttpPost]
        public ActionResult Registrar(BeanProducto p, HttpPostedFileBase archivo)
        {
            string msg;
            SelectList cboCat = new SelectList(ws.cat_listar(), "CODCAT", "DESCCAT", p.CATPROD);

            if (archivo == null)
            {
                p.FOTOPROD = "-";
                msg = ws.pro_registrar(p);
            }
            else
            {
                bool q = Path.GetExtension(archivo.FileName) == ".jpg";
                bool r = Path.GetExtension(archivo.FileName) == ".png";
                bool s = Path.GetExtension(archivo.FileName) == ".gif";

                //si el archivo no es jpg, png o gif
                if (!q && !r && !s)
                {
                    ViewBag.ext_invalida = "Debe seleccionar una imagen en formato jpg,png o gif";
                    ViewBag.cat = cboCat;
                    return View(p);
                }

                try
                {
                    string foto = "prod" + ws.pro_autogenera() + Path.GetExtension(archivo.FileName);
                    archivo.SaveAs(Server.MapPath("~/imagenes/productos/" + foto));

                    p.FOTOPROD = foto;
                    msg = ws.pro_registrar(p); 

                }
                catch (Exception e)
                {
                    msg = "Hubo un error al cargar el archivo: " + e.Message;

                }
            }
            ViewBag.cat = cboCat;
            return RedirectToAction("Index", "Confirmacion", new { mensaje = msg });
        }

        public ActionResult Editar(string codprod)
        {
            BeanProducto p = ws.pro_listar("1","0","").Where(x => x.CODPROD == codprod).FirstOrDefault();
            SelectList cboCat = new SelectList(ws.cat_listar(), "CODCAT", "DESCCAT");
            ViewBag.cat = cboCat;
            return View(p);
        }

        [HttpPost]
        public ActionResult Editar(BeanProducto p, HttpPostedFileBase archivo)
        {
            string msg;
            SelectList cboCat = new SelectList(ws.cat_listar(), "CODCAT", "DESCCAT", p.CATPROD);

            if (archivo == null)
            {
                msg = ws.pro_editar(p);
            }
            else
            {

                bool q = Path.GetExtension(archivo.FileName) == ".jpg";
                bool r = Path.GetExtension(archivo.FileName) == ".png";
                bool s = Path.GetExtension(archivo.FileName) == ".gif";


                //si el archivo no es jpg, png o gif
                if (!q && !r && !s)
                {
                    ViewBag.ext_invalida = "Debe seleccionar una imagen en formato jpg,png o gif";
                    ViewBag.cat = cboCat;
                    return View(p);
                }

                try
                {
                    string foto = "prod" + p.CODPROD + Path.GetExtension(archivo.FileName);
                    archivo.SaveAs(Server.MapPath("~/imagenes/productos/" + foto));
                    p.FOTOPROD = foto;
                    msg = ws.pro_editar(p);

                }
                catch (Exception e)
                {
                    msg = "Hubo un error al cargar el archivo: " + e.Message;

                }
            }
            ViewBag.cat = cboCat;
            return RedirectToAction("Index", "Confirmacion", new { mensaje = msg });

        }

        public ActionResult Inactivar(string codprod)
        {
            string msg = ws.pro_inactivar(codprod);
            return RedirectToAction("Index", "Confirmacion", new { mensaje = msg });
        }
    }
}