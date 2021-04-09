using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DSW2_Proyecto_Huron_Azul.localhost;
using DSW2_Proyecto_Huron_Azul.Models;

namespace DSW2_Proyecto_Huron_Azul.Controllers
{
    public class PedidoController : Controller
    {
        Huron_Azul_Service ws = new Huron_Azul_Service();

        public ActionResult Carrito()
        {
            if (Session["carrito"] == null) Session["carrito"] = new List<Carrito>();
            return View((List<Carrito>)Session["carrito"]);
        }

        public ActionResult Add_producto(long codprod, int cantidad)
        {

            //1. creo una lista de carrito y le asigno la lista de carrito en sesion
            List<Carrito> carrito = (List<Carrito>)Session["carrito"];

            //2.busco en la lista si ya existe un item carrito con el codigo de producto del parametro
            Carrito c = carrito.Where(x => x.codigo == codprod).FirstOrDefault();

            if (c != null)
            {
                //3.si existe, aumentar la cantidad 
                c.cantidad += cantidad;
            }
            else
            {
                //3.si no existe, obtengo el producto que tenga como codigo el parametro codprod                 
                BeanProducto p = ws.pro_listar("1", "0", "").Where(x => x.CODPROD == codprod.ToString()).FirstOrDefault();

                //4. creo una nuevo objeto carrito y le paso informacion del producto obtenido y la cantidad
                c = new Carrito
                {
                    codigo = long.Parse(p.CODPROD),
                    descripcion = p.DESCPROD,
                    precio = decimal.Parse(p.PREPROD),
                    cantidad = cantidad
                };
                //5. añado el objeto carrito a la lista de sesion carrito
                carrito.Add(c);
            }

            //Se redirecciona a la pantalla de confirmacion
            return RedirectToAction("Index", "Confirmacion", new { mensaje = "El producto fue añadido al carrito." });
        }

        public ActionResult Delete_producto(long codprod)
        {
            List<Carrito> carrito = (List<Carrito>)Session["carrito"];
            Carrito c = carrito.Where(x => x.codigo == codprod).FirstOrDefault();
            carrito.Remove(c);
            return RedirectToAction("Carrito");
        }

        public ActionResult Registrar()
        {

            BeanUsuario u_sesion = (BeanUsuario)Session["Usuario"];
            if (u_sesion == null) return RedirectToAction("Index", "Logueo");

            List<Carrito> carrito = (List<Carrito>)Session["carrito"];
            ViewBag.sedes = new SelectList(ws.s_listar(), "CODSEDE", "REFSEDE");
            BeanPedido p = new BeanPedido
            {
                CLIENTE = u_sesion.CODUSUARIO,
                FECPEDIDO = DateTime.Now.ToString("yyyy-M-d HH:mm:ss"),
                MONTO = carrito.Sum(x => x.monto).ToString()
            };
            return View(p);

        }

        [HttpPost]
        public ActionResult Registrar(BeanPedido p)
        {
            string msg;
            List<BeanPedidoDetalle> temp = new List<BeanPedidoDetalle>();
            foreach (Carrito reg in (List<Carrito>)Session["carrito"])
            {
                BeanPedidoDetalle pd = new BeanPedidoDetalle
                {
                    PRODUCTO = reg.codigo.ToString(),
                    CANTIDAD = reg.cantidad.ToString(),
                    MONTO = reg.monto.ToString()
                };
                temp.Add(pd);
            }

            msg = ws.ped_registrar(p, temp.ToArray());
            ws.listaBeanPedidoDetalle(temp.ToArray());

            Session["carrito"] = null;
            ViewBag.sedes = new SelectList(ws.s_listar(), "CODSEDE", "REFSEDE", p.SEDE);
            return RedirectToAction("Index", "Confirmacion", new { mensaje = msg });
        }


        public ActionResult Listar(string usuario)
        {
            BeanPedidoTuneado[] lista = ws.ped_listar(usuario);
            if (lista == null) return RedirectToAction("Index", "Home");
            return View(lista);
        }

        public ActionResult Detalle(string nropedido)
        {
            return View(ws.ped_detalle(nropedido));
        }


    }
}