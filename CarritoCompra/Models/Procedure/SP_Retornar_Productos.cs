using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CarritoCompra.Models.Procedure
{
    public class SP_Retornar_Productos
    {
        public int id_producto { get; set; }
        public int id_categoria { get; set; }
        public string nombre_categoria { get; set; }
        public string nombre { get; set; }
        public int Cantidad_disponible { get; set; }
        public string Descripcion { get; set; }
        public string url { get; set; }
        public int id_usuario { get; set; }
    }
}