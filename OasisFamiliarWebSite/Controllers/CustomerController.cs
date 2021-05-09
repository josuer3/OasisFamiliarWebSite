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
                logueado = bd.Usuario.FirstOrDefault(x => x.idUsuario == logueado.idUsuario);
            }

            /*
            Configuracion para usar valores sin quemar*
            var usuario = Session["Data"];
            Usuario logueado = (Usuario)usuario; 
            */

            //Buscar informacion de orden
            cliente.Nombre_Usuario = logueado.Nombre_Usuario;
            cliente.idUsuario = logueado.idUsuario;
            cliente.EstadoCuenta = datosOrden.ObtenerEstadoCuenta(logueado.idUsuario);

            if (cliente.EstadoCuenta != 0)
            {
                using (var bd = new ContextDB())
                {
                    Factura data = bd.Factura.Find(datosOrden.FacturaActual(logueado.idUsuario));

                    if (data.estado == 0)
                    {
                        cliente.FacturaActual = datosOrden.FacturaActual(logueado.idUsuario);
                        cliente.MontoActual = datosOrden.MontoTotal(cliente.FacturaActual);
                    }
                    else 
                    {
                        cliente.FacturaActual = 0;
                        cliente.MontoActual = 0;
                        cliente.EstadoCuenta = 0;
                    }
                }                
            }
            else 
            {
                cliente.FacturaActual = 0;
                cliente.MontoActual = 0;
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
        public ActionResult MenuPage(int idfactura, int idMenu, int cantidad)
        {

            if (idfactura == 0) {
                ViewBag.Error = "Antes de Ordenar favor solicitad una mesa"; 
            }
            else {
                Comprado orden = new Comprado();
                orden.Cantidad = cantidad;
                orden.idFactura = idfactura;
                orden.idMenu = idMenu;
                orden.estado = 1;
                orden.Fecha = DateTime.Now;

                using (var context = new ContextDB())
                {
                    context.Comprado.Add(orden);
                    context.SaveChanges();
                }
            }

            ViewBag.Message = idfactura;
            List<Menu> Listado = null;

            Listado = datosOrden.GetMenu();   
            return View(Listado);
        }
      

        //Revisar detalle de Orden 
        public ActionResult VerFactura(int item)
        {
            int fact = item;

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

                //Usar configuracion de obtener datos 
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


        public ActionResult Cuenta()
        {
            var usuario = Session["Data"];
            Usuario logueado = (Usuario)usuario;

            using (var bd = new ContextDB())
            {
                logueado = bd.Usuario.FirstOrDefault(x => x.idUsuario == logueado.idUsuario);
            }

            CuentaVM Usuario = new CuentaVM();
            Usuario.idUsuario = logueado.idUsuario;
            Usuario.Nombre_Usuario = logueado.Nombre_Usuario;
            Usuario.Promociones = logueado.Promociones;
            Usuario.Correo = logueado.Correo;

            return View(Usuario);
        }


        [HttpPost]
        public ActionResult Cuenta(CuentaVM model)
        {

            if (ModelState.IsValid) 
            {

                using (var bd = new ContextDB())
                {
                    var usuarioActualizar = bd.Usuario.Find(model.idUsuario);

                    usuarioActualizar.Promociones = model.Promociones;
                    usuarioActualizar.Correo = model.Correo;

                    bd.Entry(usuarioActualizar).State = System.Data.Entity.EntityState.Modified;
                    bd.SaveChanges();

                    ViewBag.Message = "Usuario actualizado";
                }
            }

            return View(model);
        }
    }
}