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
            return View(new Usuario() { });
        }

        [UsuarioCache]
        [Route("ListaProductos")]
        public ActionResult ListaTransacciones()
        {
            var productos = servicioProducto.ObtenerProductos();
            SP_Registrar_Usuario usuarioEnCache = (SP_Registrar_Usuario)HttpContext.Cache["Usuario"];

            ViewBag.IdUsuario = usuarioEnCache.id_usuario;

            return PartialView("ListaTransacciones", productos);
        }
    }
}