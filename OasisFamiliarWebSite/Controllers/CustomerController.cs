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
    

    public class CustomerController : Controller
    {
        // GET: Customer
        CustomerHelper datosOrden = new CustomerHelper();
        CustomerVM cliente = new CustomerVM();

        public ActionResult Index()
        {

            var usuario = Session["Data"];
            Usuario logueado = (Usuario)usuario;

            using (var bd = new ContextDB())
            {
                logueado = bd.Usuario.FirstOrDefault(x => x.idUsuario == 6);
            }


            //Buscar informacion de orden
            cliente.Nombre_Usuario = logueado.Nombre_Usuario;
            cliente.idUsuario = logueado.idUsuario;
            cliente.EstadoCuenta = datosOrden.ObtenerEstadoCuenta(logueado.idUsuario);

            if (cliente.EstadoCuenta != 0) {
                cliente.FacturaActual = datosOrden.FacturaActual(logueado.idUsuario);
                cliente.MontoActual = datosOrden.MontoTotal(cliente.FacturaActual);
            }

            ViewBag.Data = cliente;
            return View();
        }


        public ActionResult MenuPage(int item)
        {

            ViewBag.Message = item;
            List<Menu> ListaItem = null;
            ListaItem = datosOrden.GetMenu();

            return View(ListaItem);
        }

        [HttpPost]
        public ActionResult MenuPage(int Mesa, int Id_Menu, int Cantidad)
        {

            if (Mesa == 0) {

                Comprado orden = new Comprado();
                orden.Cantidad = Cantidad;
                orden.idFactura = 21;
                orden.idMenu = Id_Menu;

                using (var context = new ContextDB())
                {

                    context.Comprado.Add(orden);
                    context.SaveChanges();
                }

            }

            ViewBag.Message = Mesa;
            List<Menu> Listado = null;

            Listado = datosOrden.GetMenu();

            return View(Listado);
        }
    }
}