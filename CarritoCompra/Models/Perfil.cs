﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarritoCompra.Models
{
    public class Perfil
    {
        [Key]
        public int id_perfil { get; set; }

        [DisplayName("Rol")]
        public string rol { get; set; }
    }
}