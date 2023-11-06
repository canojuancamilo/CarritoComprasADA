using CarritoCompra.Models;
using CarritoCompra.Models.Procedure;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CarritoCompra.Servicios
{
    public class ServicioUsuario
    {
        public int IngresarUsuario(string nombre, string identificacion, string direccion, string telefono, string usuario,
            string contrasena, int id_perfil = 2)
        {
            using (var contexto = new BDContextApplication())
            {
                var resultado = contexto.Database.SqlQuery<int>("SP_Registrar_Usuario @nombre, @direccion, @telefono, @identificacion, @usuario, @contrasena, @id_perfil",
                    new SqlParameter("nombre", nombre),
                    new SqlParameter("direccion", direccion),
                    new SqlParameter("telefono", telefono),
                    new SqlParameter("identificacion", identificacion),
                    new SqlParameter("usuario", usuario),
                    new SqlParameter("contrasena", contrasena),
                    new SqlParameter("id_perfil", id_perfil)
                ).FirstOrDefault();

                return resultado;
            }
        }

        public SP_Retornar_Usuario ObtenerClienteUsuarioIdentificacion(string identificacion, string usuario)
        {
            using (var contexto = new BDContextApplication())
            {
                var resultado = contexto.Database.SqlQuery<SP_Retornar_Usuario>("SP_Obtener_cliente_identificacion_usuario @identificacion, @usuario",
                    new SqlParameter("identificacion", identificacion),
                    new SqlParameter("usuario", usuario)
                ).FirstOrDefault();

                return resultado;
            }
        }

        public SP_Retornar_Usuario ObtenerClienteUsuario(string usuario)
        {
            using (var contexto = new BDContextApplication())
            {
                var resultado = contexto.Database.SqlQuery<SP_Retornar_Usuario>("SP_Validar_Login @usuario",
                    new SqlParameter("usuario", usuario)
                ).FirstOrDefault();

                return resultado;
            }
        }

        public List<SP_Retornar_Usuario> ObtenerUsuariosCliente()
        {
            using (var contexto = new BDContextApplication())
            {
                var resultado = contexto.Database.SqlQuery<SP_Retornar_Usuario>("SP_Retornar_Clientes").ToList();

                return resultado;
            }
        }
    }
}