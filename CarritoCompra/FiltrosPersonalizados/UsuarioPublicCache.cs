using CarritoCompra.Models.Enums;
using CarritoCompra.Models.Procedure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CarritoCompra.FiltrosPersonalizados
{
    public class UsuarioPublicCache : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            SP_Registrar_Usuario usuarioEnCache = (SP_Registrar_Usuario)filterContext.HttpContext.Cache["Usuario"];

            if (usuarioEnCache != null)
            {
                if(usuarioEnCache.id_perfil == (int)Rol.Cliente) 
                {
                    // Los datos no están en la caché, redirige al login
                    filterContext.Result = new RedirectResult("~/Cliente/Inicio");
                }
            }
        }
    }
}