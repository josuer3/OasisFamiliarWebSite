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

            List<MesasVM> ListadoMesas = null;


            using (var bd = new ContextDB())
            {
                var data = bd.Mesas.ToList();

                ListaMesas = bd.Mesas.ToList();

             }

            /*
            foreach (var data in ListaMesas)
            {
                ListadoMesas.Add(new MesasVM()
                {
                    idMesa = data.idMesa,
                    NumeroFactura = 0,
                });
            }


            for (int i = 0; i < ListadoMesas.Count; i++) {

                if (ListaMesas[i].Disponible == 1)
                {
                    //Hacer configuracion de obtener datos 
                    
                    using (var context = new ContextDB())
                    {
                        //var factura = context.Fac.SingleOrDefault(b => b.idMesa == idMesa);
                        var cliente = context.Factura.OrderByDescending(x => x.idMesa).Where(y=>y.idMesa==ListadoMesas[i].idMesa).FirstOrDefault();
                        //dato quemado es 10
                        ListadoMesas[i].NumeroFactura = 10;
                    }
                }              
            }*/

            return View(ListaMesas);
        }


        //Get temporal
        public ActionResult Factura()
        {
            //usar factura #10
            int factura = 10;
            
            using (var bd = new ContextDB())
            {
                var datosMesa = bd.Factura.First(a => a.idFactura == factura);


            }

            

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

            /*Datos del vendedor*/
            var usuario = Session["Data"];
            Usuario logueado = (Usuario)usuario;

            Usuario cliente = new Usuario();
           

            /*Datos del cliente recien creado*/
            using (var context = new ContextDB()) 
            {
                cliente = context.Usuario.OrderByDescending(x => x.idUsuario).FirstOrDefault();
            }




            nuevaFactura.idMesa = idMesa;
            nuevaFactura.Fecha = time;
            nuevaFactura.idCliente = cliente.idUsuario ;
            nuevaFactura.idVendedor = logueado.idUsuario;


            using (var context = new ContextDB())
            {
                //Tomar valores de la mesa a actualizar
                var mesaUpdate = context.Mesas.SingleOrDefault(b => b.idMesa == idMesa);

                //Establecer nuevo estao de la mesa
                mesaUpdate.Disponible = 1;

                //Agregar nueva factura
                context.Factura.Add(nuevaFactura);
                context.SaveChanges();
            }


            Factura factura = new Factura();

            /*Datos de la Factura recien creado*/
            using (var context = new ContextDB())
            {
                factura = context.Factura.OrderByDescending(x => x.idFactura).FirstOrDefault();
            }


            return RedirectToAction("MenuPage", "Receptionist", factura);
        }

        [HttpPost]
        public ActionResult VerFactura()
        {

            return View("Factura");
            
        }
        public ActionResult VerFacturaPendientes()
        {

            return View();
        }
        public ActionResult Historial()
        {

            return View();
        }
        public ActionResult CerrarSession()
        {

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
        public ActionResult MenuPage(Factura item)
        {

            ViewBag.Message = item.idFactura;


            List<Menu> ListaItem = null;
            using (var bd = new ContextDB())
            {
                ListaItem = bd.Menu.OrderBy(x => x.Tipo_Producto).ToList();
            }
            return View(ListaItem);
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