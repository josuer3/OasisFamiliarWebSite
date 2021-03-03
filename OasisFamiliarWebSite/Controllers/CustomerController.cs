using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model.DataModel;


namespace OasisFamiliarWebSite.Controllers
{
    public class CustomerController : Controller
    {
        // GET: Customer
        public ActionResult Index()
        {
            List<Menu> ListaItem = null;
            using (var bd = new ContextDB())
            {
                ListaItem = bd.Menu.ToList();
            }
            return View(ListaItem);
        }
    }
}