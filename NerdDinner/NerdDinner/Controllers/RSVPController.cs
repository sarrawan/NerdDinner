using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NerdDinner.Models;

namespace NerdDinner.Controllers
{
    public class RSVPController : Controller
    {
        NerdDinners nerdDinnerDB = new NerdDinners();


        // GET: RSVP
        [Authorize]
        [HttpPost]
        public ActionResult Register(int id)
        {
            Dinner dinner = nerdDinnerDB.Dinners.Find(id);
            if (!dinner.IsUserRegistered(User.Identity.Name))
            {
                RSVP rsvp = new RSVP();
                rsvp.AttendeeName = User.Identity.Name;

                //if (dinner.RSVPs == null)
                //{
                //    dinner.RSVPs = new List<RSVP>();
                //}
                dinner.RSVPs.Add(rsvp);
                nerdDinnerDB.SaveChanges();
            }
            return Content("Thanks - we'll see you there!");
        }
    }
}