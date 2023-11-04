using CarritoCompra.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CarritoCompra.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View(new Usuario() { });
        }

        [Route("ValidarUsuario"), HttpPost]
        public ActionResult ValidarUsuario(Usuario model)
        {
            if (ModelState.IsValid)
            {
                return Json(new { }, JsonRequestBehavior.AllowGet);
            }

            return View("Index", model);
        }

        [Route("RegistrarUsuario"), AcceptVerbs(HttpVerbs.Get | HttpVerbs.Head)]
        public ActionResult RegistrarUsuario()
        {
            return View(new Usuario() { });
        }

        [Route("ActualizarUsuario"), HttpPost]
        public ActionResult ActualizarUsuario(Usuario Model)
        {
            if (ModelState.IsValid) 
            {
                return Json(new { }, JsonRequestBehavior.AllowGet);
            }

            return View("RegistrarUsuario", Model);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}