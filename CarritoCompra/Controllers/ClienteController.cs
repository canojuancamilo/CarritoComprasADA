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
        [UsuarioCliente]
        [Route("Inicio")]
        public ActionResult Inicio()
        {
            return View(new Usuario() { });
        }

        [UsuarioCache]
        [Route("ListaProductos")]
        public ActionResult ListaProducto()
        {
            var productos = servicioProducto.ObtenerProductos();
            SP_Retornar_Usuario usuarioEnCache = (SP_Retornar_Usuario)HttpContext.Cache["Usuario"];

            ViewBag.IdUsuario = usuarioEnCache.id_usuario;

            return PartialView("_ListaProductos", productos);
        }

        [UsuarioCache]
        [HttpPost]
        [Route("ValidarCantidadProductos")]
        public ActionResult ValidarCantidadProductos(List<SP_Retornar_Productos> ListaProductos)
        {
            //Para eliminar los registros duplicados y dejar uno solo y seguir con las validaciones
            var productosSinDuplicados = RetornarDatosSinrepetir(ListaProductos);

            List<SP_Retornar_Productos> listProductosSinStock = new List<SP_Retornar_Productos>();

            foreach (var producto in productosSinDuplicados)
            {
                var cantidadStock = servicioProducto.ValidarCantidadProducto(producto.id_producto);
                if (cantidadStock < producto.Cantidad_disponible)
                {
                    listProductosSinStock.Add(new SP_Retornar_Productos()
                    {
                        Cantidad_disponible = cantidadStock,
                        nombre = producto.nombre
                    });
                }
            }

            return Json(new { productosSinStock = (listProductosSinStock.Count > 0), lista = listProductosSinStock });
        }

        [UsuarioCache]
        [HttpPost]
        [Route("GuardarDatos")]
        public ActionResult ActualizarTransaccion(List<SP_Retornar_Productos> ListaProductos)
        {
            //Para eliminar los registros duplicados y dejar uno solo y seguir con las validaciones
            var productosSinDuplicados = RetornarDatosSinrepetir(ListaProductos);
            int IdTransaccion = 0;

            if (productosSinDuplicados.Count > 0) 
            {
                IdTransaccion= servicioProducto.IngresarTransaccion(productosSinDuplicados.FirstOrDefault().id_usuario, DateTime.Now);
            }

            foreach (var producto in productosSinDuplicados)
            {
                var cantidadStock = servicioProducto.ValidarCantidadProducto(producto.id_producto);

                if (cantidadStock < producto.Cantidad_disponible)
                {
                    producto.Cantidad_disponible = cantidadStock;
                }

                if (producto.Cantidad_disponible != 0) 
                {
                    servicioProducto.IngresarPedido(producto.id_producto, producto.Cantidad_disponible, DateTime.Now, IdTransaccion);
                }
            }

            TempData["MensajeLista"] = "Se inserto correctamente la transaccion";

            return Content(string.Empty);
        }

        [NonAction]
        public List<SP_Retornar_Productos> RetornarDatosSinrepetir(List<SP_Retornar_Productos> ListaProductos)
        {
            return ListaProductos.GroupBy(p => p.id_producto)
                    .Select(g => new SP_Retornar_Productos
                    {
                        id_producto = g.Key,
                        id_usuario = g.First().id_usuario,
                        Cantidad_disponible = g.Sum(p => p.Cantidad_disponible),
                        nombre = g.First().nombre
                    }).ToList();
        }
    }
}