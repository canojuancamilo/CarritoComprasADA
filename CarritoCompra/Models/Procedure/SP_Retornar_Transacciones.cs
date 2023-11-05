using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CarritoCompra.Models.Procedure
{
    public class SP_Retornar_Transacciones
    {
        public int id_transaccion { get; set; }
        public int id_usuario { get; set; }
        public DateTime fecha_transaccion { get; set; }
        public string nombre { get; set; }
        public string identificacion { get; set; }
    }
}