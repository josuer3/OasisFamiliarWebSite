using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OasisFamiliarWebSite.Models;
using Model.DataModel;
using OasisFamiliarWebSite.Servics.Call;
using System.Web.Security;

namespace OasisFamiliarWebSite.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        UsersServices _manejoUserServices = new UsersServices();
        SessionCLS sessiom = new SessionCLS();

        // GET: Account
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login( LoginVM Model)
        {
            if (ModelState.IsValid)
            {
                if (_manejoUserServices.IniciarSesion(Model))
                {
                    //Obtener rol
                    string rol = _manejoUserServices.getRoll(Model);

                    //Calcular accion para Rol
                    switch (rol)
                    { 
                        case "Cliente":
                            return RedirectToAction("Index", "Customer");

                        case "Mesero":
                            return RedirectToAction("Index", "Receptionist");

                        case "Admin":
                            return RedirectToAction("Index", "Admin");

                        default:
                            return RedirectToAction("About", "Home");
                    }


                }
            }
            @ViewBag.Message = "Error";
            return View(Model);
        }
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Register(RegisterVM model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            else
            {
                _manejoUserServices.CrearUsuario(model);
            }

            return RedirectToAction("Login","Account");
        }

        //---------------------------------------------------------------------END POINT--------------------------------------------------------------------------//
        
        
        
        [HttpPost]
        public JsonResult VerifyIfUserExist(string Nombre_Usuario)
        {
                bool Exito = _manejoUserServices.ValidandoNombreDeUsuario(Nombre_Usuario);
                return Json(Exito);
         }

        public ActionResult Salir()
        {
            //  FormsAuthentication.SignOut();
            sessiom.logout();
            return this.RedirectToAction("Login");
        }

    }
}