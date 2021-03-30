using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model.DataModel;
using OasisFamiliarWebSite.Models;

using OasisFamiliarWebSite.Servics.Call;



namespace OasisFamiliarWebSite.Controllers
{
    public class ReceptionistController : Controller
    {

        UsersServices _manejoUserServices = new UsersServices();


        // GET: Receptionist
        public ActionResult Index()
        {
            List<Mesas> ListaMesas = null;
            using (var bd = new ContextDB())
            {
                ListaMesas = bd.Mesas.ToList();
             }
            return View(ListaMesas);
        }

        public ActionResult Factura()
        {
           
            return View();
        }


        [HttpPost]
        public ActionResult Factura(string Mesa,string Nombre)
        {
            //Recibir nombre del cliente y el ID de la Mesa
            int idMesa = Int32.Parse(Mesa);


            RegisterVM Cliente = new RegisterVM();


            Cliente.Password = "Mesa "+idMesa;
            Cliente.Nombre_Usuario = Nombre;

            //Se crea nuevo cliente
            Register(Cliente);

            Factura nuevaFactura = new Factura();
            DateTime time = DateTime.Now;


            //Encontrar el ultimo valor ingresado en el cliente (usaremos como demo el 6)
            //Encontrar el ultimo del usuario actual (usaremos como demo el 1)-- Mesero que utiliza el programa UsuarioLogueado.IdUsuario
            //Usar el ID de la mesa.

            nuevaFactura.idMesa = idMesa;
            nuevaFactura.Fecha = time;
            nuevaFactura.idCliente = 6;
            nuevaFactura.idVendedor = 1;


            using (var context = new ContextDB())
            {

                context.Factura.Add(nuevaFactura);
                context.SaveChanges();
            }

            return View();
        }

        //Este proceso sera boolean --- si funciona enviar true // sino false
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

            return RedirectToAction("Login", "Account");
        }

        //---------------------------------------------------------------------END POINT--------------------------------------------------------------------------//



        [HttpPost]
        public JsonResult VerifyIfUserExist(string Nombre_Usuario)
        {
            bool Exito = _manejoUserServices.ValidandoNombreDeUsuario(Nombre_Usuario);
            return Json(Exito);
        }



    }
}