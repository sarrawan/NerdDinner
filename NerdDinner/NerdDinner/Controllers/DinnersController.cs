using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NerdDinner.Models;

namespace NerdDinner.Controllers
{
    public class DinnersController : Controller
    {
        NerdDinners nerdDinners = new NerdDinners();

        // GET: Dinners
        public ActionResult Index()
        {
            var allDinners = nerdDinners.Dinners.ToList();
            return View(allDinners);
        }

        public ActionResult Details(int id)
        {
            var singleDinner = nerdDinners.Dinners.Find(id);
            if (singleDinner == null)
            {
                return View("NotFound");
            }
            return View(singleDinner);
        }
    }
}