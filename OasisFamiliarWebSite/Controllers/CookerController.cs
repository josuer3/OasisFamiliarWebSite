using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model.DataModel;
using OasisFamiliarWebSite.Models;
using OasisFamiliarWebSite.Servics.Call;



    public class CookerController : Controller
    {
    // GET: Cooker

   
     public ActionResult Ordenes()
        {



            /*Proceso:
             1 Modelo para poner datos en pantalla...precisooooo
             2 tomar la informacion de todas las facturas pendienteee!!!!!!!!! Usa un where
             3 crear una nueva lista con el nuevo modal, llenarlo con la informacion de las facturas pendientes
             4 Crear otro for each para poner los datos que no estan en el punto 2
             5 despues de tener la nueva lista del nuevo modelo mandarlo a pantalla                                                                   
             */


            //lista nueva BoldaMixcta

            List<Comprado> ListaFacturasPendientes = null;

            List<CookerVM> ListadoFacturasPemdientes = new List<CookerVM>();


            using (var bd = new ContextDB())
            {
                ListaFacturasPendientes = bd.Comprado.Where(x => x.estado == 1).ToList();
            }

            foreach (var data in ListaFacturasPendientes)
            {
                ListadoFacturasPemdientes.Add(new CookerVM()
                {

                  
                    idFactura = data.idFactura,
                    Fecha = data.Fecha,
                    estado = data.estado,
                    nombre_Producto = "nombre_Producto ",
                    idMesa = 11,
                    Cantidad = data.Cantidad,
                   

            });
            }




            return View(ListadoFacturasPemdientes);



        }
    }
