using Model.DataModel;
using OasisFamiliarWebSite.Models;
using OasisFamiliarWebSite.Servics.Call;
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
        MailServices mailServices = new MailServices();
        public ActionResult Index()
        {

            List<Promociones> ListaPromocionesDB = null;

            List<PromoMV> ListaPromo = new List<PromoMV>();

            using (var bd = new ContextDB())
            {
                var data = bd.Promociones.ToList();
                ListaPromocionesDB = bd.Promociones.ToList();

            }

            foreach (var data in ListaPromocionesDB)
            {
                ListaPromo.Add(new PromoMV()
                {
                    idPromociones = data.idPromociones,
                    Titulo = data.Titulo,
                    fechaInicio = data.fechaInicio,
                    fechaFin = data.fechaFin,
                    Activar = false
                });
            }

            return View(ListaPromo);
        }




        public ActionResult CrearPromo()
        {
            PromoMV registro = new PromoMV();

            return View(registro);
        }

        [HttpPost]
        public ActionResult CrearPromo(PromoMV model)
        {

            if (ModelState.IsValid)
            {
                Promociones newPromo = new Promociones();
                //fill data
                newPromo.fechaFin = model.fechaFin;
                newPromo.fechaInicio = model.fechaInicio;
                newPromo.Titulo = model.Titulo;
                newPromo.Descripcion = model.Descripcion;
                if (model.Activar == true)
                {
                    newPromo.Active = 0;
                    //enviar mensajes
                    if (mailServices.PublicarPromo(model.Titulo, model.Descripcion))
                    {
                        ViewBag.Message = "Promociones enviadas";
                    }
                    else {
                        ViewBag.Message = "Revisar correo de clientes, no se enviaron mensajes";
                    }                    
                }
                else 
                {
                    newPromo.Active = 1;
                }


                using (var bd = new ContextDB())
                {
                    bd.Promociones.Add(newPromo);
                    bd.SaveChanges();
                    ViewBag.Message += "\tPromoción guardada en Base de Dato";
                }
            }
            return View(model);
        }


    }
}