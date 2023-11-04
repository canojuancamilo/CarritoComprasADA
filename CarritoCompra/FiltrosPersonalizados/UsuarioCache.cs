using CarritoCompra.Models.Procedure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CarritoCompra.FiltrosPersonalizados
{
    public class UsuarioCache: AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            SP_Registrar_Usuario usuarioEnCache = (SP_Registrar_Usuario)filterContext.HttpContext.Cache["Usuario"];

            if (usuarioEnCache == null)
            {
                // Los datos no están en la caché, redirige al login
                filterContext.Result = new RedirectResult("~/Home/Index");
            }
        }
    }
}