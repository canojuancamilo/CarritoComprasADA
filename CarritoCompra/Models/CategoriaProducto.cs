using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarritoCompra.Models
{
    public class CategoriaProducto
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id_categoria { get; set; }

        [DisplayName("Nombre")]
        [Required(ErrorMessage = "El campo es requerido.")]
        [StringLength(50, ErrorMessage = "No puede tener más de 20 caracteres.")]
        public string nombre { get; set; }
    }
}