using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CarritoCompra.Api.Modelo
{
    public class Api_Ingresar_Producto
    {
        public int id_categoria { get; set; }
        public string nombre { get; set; }
        public string descripcion { get; set; }
        public int cantidad_disponible { get; set; }
        public string url { get; set; }
        public string usuario { get; set; }
        public string contrasena { get; set; }
    }
}