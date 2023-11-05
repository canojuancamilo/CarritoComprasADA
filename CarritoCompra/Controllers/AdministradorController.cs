using CarritoCompra.FiltrosPersonalizados;
using CarritoCompra.Models;
using CarritoCompra.Models.Enums;
using CarritoCompra.Models.Procedure;
using CarritoCompra.Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Caching;
using System.Web.Mvc;

namespace CarritoCompra.Controllers
{
    [RoutePrefix("Administrador")]
    public class AdministradorController : Controller
    {
        protected ServicioProducto servicioProducto = new ServicioProducto();

        [UsuarioCache]
        [Route("Inicio")]
        public ActionResult Inicio()
        {
            return View();
        }

        [UsuarioCache]
        [Route("ListaProductos")]
        public ActionResult ListaTransacciones()
        {
            var transacciones = servicioProducto.ObtenerTransacciones();

            return PartialView("_ListaTransacciones", transacciones);
        }

        [UsuarioCache]
        [Route("ListaProductos")]
        public ActionResult ListaPedidos(int transaccion)
        {
            var pedidos = servicioProducto.ObtenerPedidos(transaccion);

            return PartialView("_ListaPedidos", pedidos);
        }
    }
}