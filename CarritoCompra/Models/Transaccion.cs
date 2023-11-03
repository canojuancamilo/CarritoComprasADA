using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using BCrypt.Net;

namespace CarritoCompra.Models
{
    public class Transaccion
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id_transaccion { get; set; }
        public int id_usuario { get; set; }

        [DisplayName("Usuario")]
        public Usuario Usuario { get; set; }

        [DisplayName("Fecha")]
        public DateTime fecha_transaccion { get; set; }
    }
}