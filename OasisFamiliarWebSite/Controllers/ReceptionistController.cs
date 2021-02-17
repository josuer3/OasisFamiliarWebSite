using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model.DataModel;
using OasisFamiliarWebSite.Models;


namespace OasisFamiliarWebSite.Controllers
{
    public class ReceptionistController : Controller
    {
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

    }
}