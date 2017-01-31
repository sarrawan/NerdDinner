using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using NerdDinner.Models;

namespace NerdDinner
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            // when make change to model class, db breaks since if not updated since doesnt
            // recognize change
            // this line updates db if there is a change to keep db up to date 
            Database.SetInitializer<NerdDinners>(new NerdDinnersInitializer());
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
    }
}
