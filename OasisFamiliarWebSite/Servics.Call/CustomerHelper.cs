using Model.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OasisFamiliarWebSite.Servics.Call
{
    public class CustomerHelper
    {

        public int ObtenerEstadoCuenta(int ID) {

            int estado = 0;

            using (var bd = new ContextDB())
            {
                
                var facturas = bd.Factura.OrderByDescending(x => x.Fecha).ToList();

                var facturaDetalle = (from s in facturas
                                      where s.idCliente == ID
                                      select s).LastOrDefault<Factura>();

                //var facturaDetalle = facturas.SingleOrDefault(x => x.idCliente == ID);

                if (facturaDetalle == null)
                {
                    estado = 0;
                }
                else
                { 
                    estado = 1; 
                }

            }
            return estado;
        }

        public double MontoTotal(int factura)
        {

            double monto = 0;

            using (var bd = new ContextDB())
            {

                var ordenesDetalles = bd.Comprado.Where(x => x.idFactura == factura).ToList();
                var menu = bd.Menu.ToList();

                foreach (Comprado orden in ordenesDetalles) {

                    var producto = menu.SingleOrDefault(x => x.idMenu == orden.idMenu);

                    monto += orden.Cantidad * producto.Precio;
                }

            }
            return monto;
        }

        public int FacturaActual(int ID)
        {

            int fact = 0;

            using (var bd = new ContextDB())
            {
                var facturas = bd.Factura.OrderByDescending(x => x.Fecha).ToList();
                var facturaDetalle = facturas.SingleOrDefault(x => x.idCliente == ID);
                fact = facturaDetalle.idFactura;
            }
            return fact;
        }



        public List<Menu> GetMenu()
        {

            List<Menu> listado = null;
            using (var bd = new ContextDB())
            {
                listado = bd.Menu.OrderBy(x => x.Tipo_Producto).ToList();
            }

            return listado;
        }


    }
}