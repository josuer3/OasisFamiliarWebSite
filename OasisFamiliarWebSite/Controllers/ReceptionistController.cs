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
        CustomerHelper customerDetails = new CustomerHelper();

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

  
        public ActionResult VerFactura(string factura)
        {
            int fact = 0;
            Int32.TryParse(factura, out fact);
            List<Comprado> detalle = null;

            using (var bd = new ContextDB())
            {
                var data = bd.Comprado.Where(x => x.idFactura == fact).ToList();
                var facturaDetalle = bd.Factura.SingleOrDefault(x => x.idFactura == fact);

                ViewBag.Message = facturaDetalle.Fecha.ToString();
                ViewBag.Data = facturaDetalle;
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
                    detalleOrden[i].total = detalleOrden[i].precio * detalleOrden[i].Cantidad;
                }

            }

            return View(detalleOrden);

        }

        public ActionResult Pagar(int id_factura) {

            Factura UpdateFactura = null;
            Mesas UpdateMesa = null;

            using (var bd = new ContextDB())
            {

                UpdateFactura = bd.Factura.Find(id_factura);
                UpdateFactura.estado = 1;

                UpdateMesa = bd.Mesas.Find(UpdateFactura.idMesa);
                UpdateMesa.Disponible = 0;

                bd.SaveChanges();
            }

            return RedirectToAction("Index", "Receptionist");
        }





        public ActionResult VerFacturaPendientes()
        {
           


            /*Proceso:
             1 Modelo para poner datos en pantalla...precisooooo
             2 tomar la informacion de todas las facturas pendienteee!!!!!!!!! Usa un where
             3 crear una nueva lista con el nuevo modal, llenarlo con la informacion de las facturas pendientes
             4 Crear otro for each para poner los datos que no estan en el punto 2
             5 despues de tener la nueva lista del nuevo modelo mandarlo a pantalla                                                                   
             */


            //lista nueva BoldaMixcta

            List<Factura> ListaFacturasPendientes = null;

            List<FacturasPendientesVM> ListadoFacturasPemdientes = new List<FacturasPendientesVM>();


           using (var bd = new ContextDB())
            {
                ListaFacturasPendientes = bd.Factura.Where(x => x.estado == 0).ToList();
            }

            foreach (var data in ListaFacturasPendientes)
            {
                ListadoFacturasPemdientes.Add(new FacturasPendientesVM()
                {

                    idFactura = data.idFactura,
                    Fecha = data.Fecha,
                    idCliente = data.idCliente,
                    idVendedor = data.idVendedor,
                    idMesa = data.idMesa,
                    estado = data.estado,
                    Cantidad = 0,
                    usuario = "",
                    total = customerDetails.MontoTotal(data.idFactura)

                });
            }

            for (int i = 0; i < ListadoFacturasPemdientes.Count; i++)
            {
                int cliente = ListadoFacturasPemdientes[i].idCliente;

                //Hacer configuracion de obtener datos 
                using (var context = new ContextDB())
                {
                    Usuario clientee = context.Usuario.Find(cliente);
                    ListadoFacturasPemdientes[i].usuario = clientee.Nombre_Usuario;
                }

            }



            return View(ListadoFacturasPemdientes);


       
        }
        public ActionResult Historial()
        {

            List<Factura> ListaFacturasPendientes = null;

            List<FacturasPendientesVM> ListadoFacturasPemdientes = new List<FacturasPendientesVM>();


            using (var bd = new ContextDB())
            {
                ListaFacturasPendientes = bd.Factura.Where(x => x.estado != 0).ToList();
            }

            foreach (var data in ListaFacturasPendientes)
            {
                ListadoFacturasPemdientes.Add(new FacturasPendientesVM()
                {

                    idFactura = data.idFactura,
                    Fecha = data.Fecha,
                    idCliente = data.idCliente,
                    idVendedor = data.idVendedor,
                    idMesa = data.idMesa,
                    estado = data.estado,
                    Cantidad = 0,
                    usuario = "",
                    total = customerDetails.MontoTotal(data.idFactura)

                });
            }

            for (int i = 0; i < ListadoFacturasPemdientes.Count; i++)
            {
                int cliente = ListadoFacturasPemdientes[i].idCliente;

                //Hacer configuracion de obtener datos 
                using (var context = new ContextDB())
                {
                    Usuario clientee = context.Usuario.Find(cliente);
                    ListadoFacturasPemdientes[i].usuario = clientee.Nombre_Usuario;
                }

            }



            return View(ListadoFacturasPemdientes);
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
            orden.idFactura = Mesa;
            orden.idMenu = Id_Menu;
            orden.estado = 1;
            orden.Fecha = DateTime.Now;


            using (var bd = new ContextDB())
            {
                bd.Comprado.Add(orden);
                bd.SaveChanges();
            }


            ViewBag.Message = Mesa;
            List<Menu> Listado = null;

            Listado = GetMenu(Listado);

            return RedirectToAction("VerFactura", "Receptionist", new { factura = Mesa.ToString() });


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