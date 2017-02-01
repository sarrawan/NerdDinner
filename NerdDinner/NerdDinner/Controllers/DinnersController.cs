using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using NerdDinner.Models;

namespace NerdDinner.Controllers
{
    public class DinnersController : Controller
    {
        NerdDinners nerdDinnersDB = new NerdDinners();

        // GET: Dinners
        public ActionResult Index()
        {
            var allDinners = nerdDinnersDB.Dinners.ToList();
            return View(allDinners);
        }

        // GET: Dinners/Details/1
        public ActionResult Details(int id)
        {
            var singleDinner = nerdDinnersDB.Dinners.Find(id);
            if (singleDinner == null)
            {
                return View("NotFound");
            }
            return View(singleDinner);
        }

        // GET: Dinners/Edit/1
        public ActionResult Edit(int id)
        {
            var dinner = nerdDinnersDB.Dinners.SingleOrDefault(d => d.DinnerID == id);
            return View(dinner);
        }

        // POST: Dinners/Edit/1
        [HttpPost]
        public ActionResult Edit(Dinner dinner, int id)
        {
            if (ModelState.IsValid)
            {
                dinner.DinnerID = id;
                nerdDinnersDB.Entry(dinner).State = EntityState.Modified;
                nerdDinnersDB.SaveChanges();
                return RedirectToAction("Details", new {id = dinner.DinnerID});
            }

            return View(dinner);
        }

        // GET: Dinners/Create
        public ActionResult Create()
        {
            Dinner dinner = new Dinner
            {
                EventDate = DateTime.Now.AddDays(7)
            };
            return View(dinner);
        }

        // POST: /Dinners/Create
        [HttpPost]
        public ActionResult Create(Dinner dinner)
        {
            if (ModelState.IsValid)
            {
                nerdDinnersDB.Dinners.Add(dinner);
                nerdDinnersDB.SaveChanges();
                return RedirectToAction("Details", new {id=dinner.DinnerID});
            }

            return View(dinner);
        }

        //GET: Dinners/Delete/1
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Dinner dinner = nerdDinnersDB.Dinners.Find(id);

            if (dinner == null)
            {
                return View("NotFound");
            }
            return View(dinner);
        }

        // POST: Dinners/Delete/1
        [HttpPost]
        public ActionResult Delete(int id)
        {
            Dinner dinner = nerdDinnersDB.Dinners.Find(id);
            nerdDinnersDB.Dinners.Remove(dinner);
            nerdDinnersDB.SaveChanges();
            return View("Deleted");
        }
    }
}