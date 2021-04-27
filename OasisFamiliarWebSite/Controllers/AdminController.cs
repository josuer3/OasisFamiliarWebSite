using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OasisFamiliarWebSite.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }


        // Revisar Mesas



        // Revisar Facturas Pendientes


        // Revisar Historial de Facturas


        // Inventario
        // Inventario Actual
        // Agregar inventario


        // Promociones
        public ActionResult Promociones() {

            return View();
        }


        // Agregar Promocion
        public ActionResult NuevaPromociones()
        {

            return View();
        }


    }
}