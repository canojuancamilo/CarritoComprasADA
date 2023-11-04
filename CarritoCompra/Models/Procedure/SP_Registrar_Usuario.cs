using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CarritoCompra.Models.Procedure
{
    public class SP_Registrar_Usuario
    {
        public int id_usuario { get; set; }

        public string nombre { get; set; }

        public string identificacion { get; set; }

        public string direccion { get; set; }

        public string telefono { get; set; }

        public string usuario { get; set; }

        public string contrasena { get; set; }

        public int id_perfil { get; set; }
    }
}