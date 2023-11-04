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
    [RoutePrefix("Cliente")]
    public class ClienteController : Controller
    {
        protected ServicioProducto servicioProducto = new ServicioProducto();

        [UsuarioCache]
        [Route("Inicio")]
        public ActionResult Inicio()
        {
            return View(new Usuario() { });
        }

        [Route("ListaProductos")]
        public ActionResult ListaProducto()
        {
            var productos = servicioProducto.ObtenerProductos();

            return PartialView("_ListaProductos", productos);
        }
    }
}