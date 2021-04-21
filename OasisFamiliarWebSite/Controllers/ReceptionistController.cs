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

        // crear un nuevo modelo llamado detallesvm foreach ejemplo linea 40 - nvestigar que info tengo que meter ahi 
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


            for (int i = 0; i < ListadoMesas.Count; i++) {
                if (ListaMesas[i].Disponible == 1)
                {
                    //Hacer configuracion de obtener datos 
                    using (var context = new ContextDB())
                    {
                        //var factura = context.Fac.SingleOrDefault(b => b.idMesa == idMesa);
                        var cliente = context.Factura.OrderByDescending(x => x.idMesa).ToList();
                        //dato quemado es 10
                        var detalle = (from s in cliente
                                       where s.idMesa == ListadoMesas[i].idMesa
                                       select s).LastOrDefault<Factura>();
                        ListadoMesas[i].NumeroFactura = detalle.idFactura;
                    }
                }
            }

            return View(ListadoMesas);
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
        public ActionResult Factura(string Mesa, string Nombre)
        {
            //Recibir nombre del cliente y el ID de la Mesa
            int idMesa = Int32.Parse(Mesa);

            RegisterVM Cliente = new RegisterVM();

            Cliente.Password = "Mesa " + idMesa;
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
            nuevaFactura.idCliente = cliente.idUsuario;
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
        public ActionResult VerFactura(string mesa, string factura)
        {
            int fact = 0;
            Int32.TryParse(factura, out fact);
            List<Comprado> detalle = null;

            using (var bd = new ContextDB())
            {
                var data = bd.Comprado.Where(x => x.idFactura == fact).ToList();
                detalle = data.ToList();
            }


            List<DetalleFactura> detalleOrden = new List<DetalleFactura>();


            foreach (var data in detalle)
            {
                detalleOrden.Add(new DetalleFactura()
                {
                    idComprado = data.idComprado,
                    idFactura = data.idFactura,
                    idMenu = data.idMenu,
                    Cantidad = data.Cantidad,
                    productoNombre = "",
                    precio = 0
                });
            }


            for (int i = 0; i < detalleOrden.Count; i++)
            {
                int menu = detalleOrden[i].idMenu;

                    //Hacer configuracion de obtener datos 
                using (var context = new ContextDB())
                    {
                    
                    //var factura = context.Fac.SingleOrDefault(b => b.idMesa == idMesa);
                    var producto = context.Menu.SingleOrDefault(x => x.idMenu == menu);
                    //dato quemado es 10
                    detalleOrden[i].productoNombre = producto.nombre_Producto;
                    detalleOrden[i].precio = producto.Precio;
                }

            }


            return View(detalleOrden);

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

            ListaItem = GetMenu(ListaItem);

            return View(ListaItem);
        }

        [HttpPost]
        public ActionResult MenuPage(int Mesa, int Id_Menu, int Cantidad)
        {
            Comprado orden = new Comprado();
            orden.Cantidad = Cantidad;
            orden.idFactura = 21;
            orden.idMenu = Id_Menu;

            using (var context = new ContextDB())
            {

                context.Comprado.Add(orden);
                context.SaveChanges();
            }


            ViewBag.Message = Mesa;
            List<Menu> Listado = null;

            Listado = GetMenu(Listado);

            return View(Listado);
        }
        //---------------------------------------------------------------------END POINT--------------------------------------------------------------------------//



        [HttpPost]
        public JsonResult VerifyIfUserExist(string Nombre_Usuario)
        {
            bool Exito = _manejoUserServices.ValidandoNombreDeUsuario(Nombre_Usuario);
            return Json(Exito);
        }

        public List<Menu> GetMenu(List<Menu> listado) {


            using (var bd = new ContextDB())
            {
                listado = bd.Menu.OrderBy(x => x.Tipo_Producto).ToList();
            }

            return listado;
        }

    }
}