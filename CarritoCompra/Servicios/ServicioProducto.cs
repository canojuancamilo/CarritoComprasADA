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
    }
}