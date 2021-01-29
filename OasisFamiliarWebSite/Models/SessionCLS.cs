using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OasisFamiliarWebSite.Models
{
   
    public class SessionCLS
    {
        string Session;

        public void setSession(string name, object data)
        {
            HttpContext.Current.Session[name] = data;
        }

        public string getSession (string name)
        {
            this.Session = Convert.ToString(HttpContext.Current.Session[name]);
            return Session;
        }

        public void logout()
        {
            HttpContext.Current.Session.Clear();
            HttpContext.Current.Session.Abandon();
            HttpContext.Current.Session.RemoveAll();
        }
    }
}