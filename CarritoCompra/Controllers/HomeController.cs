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
    public class HomeController : Controller
    {
        protected ServicioUsuario servicioUsuario = new ServicioUsuario();

        [UsuarioPublicCache]
        public ActionResult Index()
        {
            return View(new Usuario() { });
        }

        [Route("ValidarUsuario"), HttpPost]
        public ActionResult ValidarUsuario(Usuario model)
        {
            ModelState.Remove("id_usuario");
            ModelState.Remove("nombre");
            ModelState.Remove("identificacion");
            ModelState.Remove("direccion");
            ModelState.Remove("telefono");
            ModelState.Remove("id_perfil");

            if (ModelState.IsValid)
            {
                var usuario = servicioUsuario.ObtenerClienteUsuario(model.usuario);

                if (usuario != null)
                {
                    if (model.VerificarContrasena(usuario.contrasena))
                    {
                        SP_Registrar_Usuario datosEnCache = (SP_Registrar_Usuario)HttpContext.Cache["Usuario"];

                        // Guarda los datos en la caché con una duración de 5 minutos (300 segundos)
                        HttpContext.Cache.Insert("Usuario", usuario, null, DateTime.Now.AddSeconds(300), Cache.NoSlidingExpiration);

                        return Json(new { success = true, redirectTo = Url.Action("Inicio", "Cliente") });
                    }

                    ModelState.AddModelError("contrasena", "Contraseña incorrecta.");
                }
                else
                {
                    ModelState.AddModelError("usuario", "No se encontro este usuario.");
                }
            }

            Response.StatusCode = 400;
            return View("Index", model);
        }

        [UsuarioPublicCache]
        [Route("RegistrarUsuario"), AcceptVerbs(HttpVerbs.Get | HttpVerbs.Head)]
        public ActionResult RegistrarUsuario()
        {
            return View(new Usuario() { });
        }

        [Route("ActualizarUsuario"), HttpPost]
        public ActionResult ActualizarUsuario(Usuario model)
        {
            if (ModelState.IsValid)
            {
                var usuarioRepetido = servicioUsuario.ObtenerClienteUsuarioIdentificacion(model.identificacion, model.usuario);

                if (usuarioRepetido != null)
                {
                    if (usuarioRepetido.usuario == model.usuario)
                    {
                        ModelState.AddModelError("usuario", "Ya existe un cliente con este usuario.");
                    }

                    if (usuarioRepetido.identificacion == model.identificacion)
                    {
                        ModelState.AddModelError("identificacion", "Ya existe un cliente con esta identificación.");
                    }
                }
                else
                {
                    servicioUsuario.IngresarUsuario(model.nombre, model.identificacion, model.direccion, model.telefono,
                         model.usuario, model.EncriptarContrasena(), (int)Rol.Cliente);

                    TempData["Mensaje"] = "Se registró correctamente el usuario, puede iniciar sesión.";
                    return Json(new { success = true, redirectTo = Url.Action("Index") });
                }
            }

            Response.StatusCode = 400;
            return View("RegistrarUsuario", model);
        }
    }
}