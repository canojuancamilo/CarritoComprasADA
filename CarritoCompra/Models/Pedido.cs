using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using BCrypt.Net;

namespace CarritoCompra.Models
{
    public class Pedido
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id_pedido { get; set; }

        public int id_producto { get; set; }
        public Producto Producto { get; set; }

        public int cantidad { get; set; }

        public DateTime fecha_pedido { get; set; }
    }
}