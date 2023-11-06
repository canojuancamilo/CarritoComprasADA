using CarritoCompra.Api.Modelo;
using CarritoCompra.Models.Enums;
using CarritoCompra.Servicios;
using CarritoCompra.Models.Procedure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CarritoCompra.Api
{
    public class ProductosController : ApiController
    {
        public IHttpActionResult Get(string usuario, string contrasena)
        {
            if (string.IsNullOrEmpty(usuario))
                return BadRequest("El campo 'usuario' es obligatorio.");

            if (string.IsNullOrEmpty(contrasena))
                return BadRequest("El campo 'contrasena' es obligatorio.");

            var mensajeError = ValidarUsuario(usuario, contrasena);

            if (!string.IsNullOrEmpty(mensajeError))
                return BadRequest(mensajeError);

            ServicioProducto servicioProducto = new ServicioProducto();

            var productos = servicioProducto.ObtenerProductos();

            return Ok(new {productos});
        }

        public HttpResponseMessage Post([FromBody] Api_Ingresar_Producto producto)
        {
            try
            {
                if (string.IsNullOrEmpty(producto.usuario))
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "El campo 'usuario' es obligatorio.");

                if (string.IsNullOrEmpty(producto.contrasena))
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "El campo 'contrasena' es obligatorio.");

                if (producto.id_categoria == 0)
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "El campo 'id_categoria' es obligatorio.");

                if (string.IsNullOrEmpty(producto.nombre))
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "El campo 'nombre' es obligatorio.");

                if (string.IsNullOrEmpty(producto.descripcion))
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "El campo 'descripcion' es obligatorio.");

                if (producto.cantidad_disponible == 0)
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "El campo 'cantidad_disponible' es obligatorio.");

                if (string.IsNullOrEmpty(producto.url))
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "El campo 'url' es obligatorio.");

                var mensajeError = ValidarUsuario(producto.usuario, producto.contrasena);

                if (!string.IsNullOrEmpty(mensajeError))
                    return Request.CreateResponse(HttpStatusCode.BadRequest, mensajeError);

                ServicioProducto servicioProducto = new ServicioProducto();

                int consecutivo = servicioProducto.IngresarProducto(producto.id_categoria, producto.nombre, producto.descripcion, producto.cantidad_disponible, producto.url);

                if (consecutivo != 0)
                {
                    return Request.CreateResponse(HttpStatusCode.Created, $"El producto se registro correctamente: consecutivo '{consecutivo}'");
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "Se genero algun error al crear el producto.");
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Ha ocurrido un error al procesar la solicitud: " + ex.Message);
            }
        }

        public HttpResponseMessage Put(int idProducto, [FromBody] Api_Ingresar_Producto producto)
        {
            try
            {
                if (idProducto == 0)
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "El campo 'idProducto' es obligatorio.");

                if (string.IsNullOrEmpty(producto.usuario))
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "El campo 'usuario' es obligatorio.");

                if (string.IsNullOrEmpty(producto.contrasena))
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "El campo 'contrasena' es obligatorio.");

                if (producto.id_categoria == 0)
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "El campo 'id_categoria' es obligatorio.");

                if (string.IsNullOrEmpty(producto.nombre))
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "El campo 'nombre' es obligatorio.");

                if (string.IsNullOrEmpty(producto.descripcion))
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "El campo 'descripcion' es obligatorio.");

                if (producto.cantidad_disponible == 0)
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "El campo 'cantidad_disponible' es obligatorio.");

                if (string.IsNullOrEmpty(producto.url))
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "El campo 'url' es obligatorio.");

                var mensajeError = ValidarUsuario(producto.usuario, producto.contrasena);

                if (!string.IsNullOrEmpty(mensajeError))
                    return Request.CreateResponse(HttpStatusCode.BadRequest, mensajeError);

                ServicioProducto servicioProducto = new ServicioProducto();

                servicioProducto.ActualizarProducto(idProducto, producto.id_categoria, producto.nombre, producto.descripcion, producto.cantidad_disponible, producto.url);

                return Request.CreateResponse(HttpStatusCode.OK, "Se actualizo el producto correctamente.");
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Ha ocurrido un error al procesar la solicitud: " + ex.Message);
            }
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