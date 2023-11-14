using CarritoCompra.Models.Enums;
using CarritoCompra.Models.Procedure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CarritoCompra.FiltrosPersonalizados
{
    public class UsuarioCliente : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            SP_Retornar_Usuario usuarioEnCache = (SP_Retornar_Usuario)HttpContext.Current.Session["Usuario"];

            if (usuarioEnCache != null)
            {
                if (usuarioEnCache.id_perfil != (int)Rol.Cliente)
                {
                    filterContext.Result = new RedirectResult("~/Administrador/Inicio");
                }
            }
        }
    }
}