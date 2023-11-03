using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using BCrypt.Net;

namespace CarritoCompra.Models
{
    public class CategoriaProducto
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id_categoria { get; set; }

        [Required(ErrorMessage = "El campo es requerido.")]
        [StringLength(50, ErrorMessage = "No puede tener más de 20 caracteres.")]
        public string nombre { get; set; }
    }
}