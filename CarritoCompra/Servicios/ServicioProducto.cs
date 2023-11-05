using CarritoCompra.Models;
using CarritoCompra.Models.Procedure;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CarritoCompra.Servicios
{
    public class ServicioProducto
    {
        public List<SP_Retornar_Productos> ObtenerProductos()
        {
            using (var contexto = new BDContextApplication())
            {
                var resultado = contexto.Database.SqlQuery<SP_Retornar_Productos>("SP_Retornar_Productos").ToList();

                return resultado;
            }
        }

        public int IngresarTransaccion(int id_usuario, DateTime fecha_transaccion)
        {
            using (var contexto = new BDContextApplication())
            {
                var resultado = contexto.Database.SqlQuery<int>("SP_Registrar_Transaccion @id_usuario, @fecha_transaccion",
                    new SqlParameter("id_usuario", id_usuario),
                    new SqlParameter("fecha_transaccion", fecha_transaccion)).FirstOrDefault();

                return resultado;
            }
        }

        public int IngresarPedido(int id_producto, int cantidad, DateTime fecha_pedido, int id_transaccion)
        {
            using (var contexto = new BDContextApplication())
            {
                var resultado = contexto.Database.SqlQuery<int>("SP_Registrar_Pedido @id_producto, @cantidad, @fecha_pedido, @id_transaccion",
                    new SqlParameter("id_producto", id_producto),
                    new SqlParameter("cantidad", cantidad),
                    new SqlParameter("fecha_pedido", fecha_pedido),
                    new SqlParameter("id_transaccion", id_transaccion)).FirstOrDefault();

                return resultado;
            }
        }

        public int ValidarCantidadProducto(int id_producto)
        {
            using (var contexto = new BDContextApplication())
            {
                var resultado = contexto.Database.SqlQuery<int>("SP_Validar_cantidad_producto @id_producto",
                    new SqlParameter("id_producto", id_producto)
                ).FirstOrDefault();

                return resultado;
            }
        }

        public List<SP_Retornar_Transacciones> ObtenerTransacciones()
        {
            using (var contexto = new BDContextApplication())
            {
                var resultado = contexto.Database.SqlQuery<SP_Retornar_Transacciones>("SP_Retornar_Transacciones")
                    .OrderByDescending(m=> m.fecha_transaccion) .ToList();

                return resultado;
            }
        }

        public List<SP_Retornar_Pedidos> ObtenerPedidos(int id_transaccion)
        {
            using (var contexto = new BDContextApplication())
            {
                var resultado = contexto.Database.SqlQuery<SP_Retornar_Pedidos>("SP_Retornar_Pedidos @id_transaccion",
                    new SqlParameter("id_transaccion", id_transaccion)).ToList();

                return resultado;
            }
        }
    }
}