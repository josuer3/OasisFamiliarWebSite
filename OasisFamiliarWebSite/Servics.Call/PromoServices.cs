using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OasisFamiliarWebSite.Models;
using Model.DataModel;

namespace OasisFamiliarWebSite.Servics.Call
{
    public class PromoServices
    {

        public Promociones ObtenerPromociones()
        {


            Promociones info = null;

            using (var bd = new ContextDB())
            {
               var data = bd.Promociones.OrderBy(x => x.fechaFin).ToList();
            }
            return info;
        }


    }
}