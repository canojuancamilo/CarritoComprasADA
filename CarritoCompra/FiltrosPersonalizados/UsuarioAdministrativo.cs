using CarritoCompra.Models.Enums;
using CarritoCompra.Models.Procedure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CarritoCompra.FiltrosPersonalizados
{
    public class UsuarioAdministrativo : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            SP_Retornar_Usuario usuarioEnCache = (SP_Retornar_Usuario)filterContext.HttpContext.Cache["Usuario"];

            if (usuarioEnCache != null)
            {
                if (usuarioEnCache.id_perfil != (int)Rol.Administrador)
                {
                    filterContext.Result = new RedirectResult("~/Cliente/Inicio");
                }
            }
        }
    }
}