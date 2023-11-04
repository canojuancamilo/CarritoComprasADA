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
    public class Producto
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id_producto { get; set; }

        public int id_categoria { get; set; }

        public CategoriaProducto CategoriaProducto { get; set; }

        [DisplayName("Nombre")]
        [Required(ErrorMessage = "El campo es requerido.")]
        [StringLength(150, ErrorMessage = "No puede tener más de 150 caracteres.")]
        public string nombre { get; set; }

        [DisplayName("Cantidad disponible")]
        [Required(ErrorMessage = "El campo es requerido.")]
        public int Cantidad_disponible { get; set; }

        [DisplayName("Descripción")]
        [Required(ErrorMessage = "El campo es requerido.")]
        [StringLength(200, ErrorMessage = "No puede tener más de 200 caracteres.")]
        public string Descripcion { get; set; }
        public string url { get; set; }
    }
}