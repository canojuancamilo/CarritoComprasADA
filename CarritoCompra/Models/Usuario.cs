using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using BCrypt.Net;

namespace CarritoCompra.Models
{
    public class Usuario
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id_usuario { get; set; }

        [Required(ErrorMessage = "El campo es requerido.")]
        [StringLength(20, ErrorMessage = "No puede tener más de 20 caracteres.")]
        public string nombre { get; set; }

        [Required(ErrorMessage = "El campo es requerido.")]
        [StringLength(20, ErrorMessage = "No puede tener más de 20 caracteres.")]
        public string identificacion { get; set; }

        [Required(ErrorMessage = "El campo es requerido.")]
        [StringLength(100, ErrorMessage = "No puede tener más de 100 caracteres.")]
        public string direccion { get; set; }

        [Required(ErrorMessage = "El campo es requerido.")]
        [StringLength(20, ErrorMessage = "No puede tener más de 20 caracteres.")]
        public string telefono { get; set; }

        [Required(ErrorMessage = "El campo es requerido.")]
        [StringLength(20, ErrorMessage = "No puede tener más de 20 caracteres.")]
        public string usuario { get; set; }

        [Required(ErrorMessage = "El campo es requerido.")]
        [StringLength(20, ErrorMessage = "No puede tener más de 20 caracteres.")]
        public string contrasena { get; set; }

        public int id_perfil { get; set; }

        public Perfil Perfil { get; set; }

        public string EncriptarContrasena(string contrasenaIngresada)
        {
            var Salt = BCrypt.Net.BCrypt.GenerateSalt();
            return BCrypt.Net.BCrypt.HashPassword(contrasenaIngresada, Salt);
        }

        public bool VerificarContrasena(string contrasenaIngresada)
        {
            return BCrypt.Net.BCrypt.Verify(contrasenaIngresada, contrasena);
        }
    }
}