using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CarritoCompra.Models.Procedure
{
    public class SP_Retornar_Pedidos
    {
        public int id_pedido { get; set; }
        public int cantidad { get; set; }
        public int id_transaccion { get; set; }
        public DateTime fecha_pedido { get; set; }
        public string nombre { get; set; }
    }
}