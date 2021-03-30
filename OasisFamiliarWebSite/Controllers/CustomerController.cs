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
            //Display customer information
            //Total of bill
            //Table name
            //Customer name
            //

            return View();
        }

        public ActionResult MenuPage()
        {
            List<Menu> ListaItem = null;
            using (var bd = new ContextDB())
            {
                ListaItem = bd.Menu.OrderBy(x => x.Tipo_Producto).ToList();
            }
            return View(ListaItem);
        }
    }
}