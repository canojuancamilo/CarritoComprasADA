using CarritoCompra.Models.Enums;
using CarritoCompra.Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CarritoCompra.Api
{
    public class UsuarioComprasController : ApiController
    {
        // GET api/<controller>
        public IHttpActionResult Get(string usuario, string contrasena)
        {
            if (string.IsNullOrEmpty(usuario))
                return BadRequest("El campo 'usuario' es obligatorio.");

            if (string.IsNullOrEmpty(contrasena))
                return BadRequest("El campo 'contrasena' es obligatorio.");

            var mensajeError = ValidarUsuario(usuario, contrasena);

            if (!string.IsNullOrEmpty(mensajeError))
                return BadRequest(mensajeError);

            ServicioUsuario servicioUsuario = new ServicioUsuario();

            var usuarios = servicioUsuario.ObtenerUsuariosCliente();

            return Ok(new { usuarios });
        }

        public string ValidarUsuario(string usuarioCliente, string contrasena)
        {
            ServicioUsuario servicioUsuario = new ServicioUsuario();

            var usuario = servicioUsuario.ObtenerClienteUsuario(usuarioCliente);

            if (usuario == null)
            {
                return "Usuario o contraseña invalido.";
            }

            if (!BCrypt.Net.BCrypt.Verify(contrasena, usuario.contrasena))
                return "Usuario o contraseña invalido.";

            if (usuario.id_perfil == (int)Rol.Cliente)
                return "Acceso denegado.";

            return "";
        }
    }
}