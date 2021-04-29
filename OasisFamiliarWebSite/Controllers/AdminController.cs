using Model.DataModel;
using OasisFamiliarWebSite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OasisFamiliarWebSite.Controllers
{
    public class AdminController : Controller
    {
        // Revisar Mesas
        public ActionResult Index()
        {
            List<Mesas> ListaMesas = null;

            List<MesasVM> ListadoMesas = new List<MesasVM>();
            using (var bd = new ContextDB())
            {
                var data = bd.Mesas.ToList();
                ListaMesas = bd.Mesas.ToList();

            }

            foreach (var data in ListaMesas)
            {
                ListadoMesas.Add(new MesasVM()
                {
                    idMesa = data.idMesa,
                    Nombre_Mesa = data.Nombre_Mesa,
                    Disponible = data.Disponible,
                    NumeroFactura = 0,
                    TotalFactura = null,
                });
            }


            return View();
        }


        public ActionResult VerFacturaPendientes()
        {


            return View();
        }


        public ActionResult VerPromociones()
        {


            return View();
        }
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