using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using NerdDinner.Helpers;
using NerdDinner.Models;

namespace NerdDinner.Controllers
{
    public class DinnersController : Controller
    {
        //NerdDinners nerdDinnersDB = new NerdDinners();
        //private INerdDinners nerdDinnersDB;
        //DinnerRepository dinnerRepository = new DinnerRepository();
        private IDinnerRepository iDinnerRepos;

        public DinnersController() : this(new DinnerRepository())
        {
        }

        public DinnersController(IDinnerRepository repository)
        {
            iDinnerRepos = repository;
        }

        // GET: Dinners
        //      Dinners/page/2
        public ActionResult Index(int? page)
        {
            const int pageSize = 10;
            ////var allDinners = nerdDinnersDB.Dinners.ToList();
            //var upcomingDinners = nerdDinnersDB.FindUpcomingDinners();
            var upcomingDinners = iDinnerRepos.FindUpcomingDinners();
            var paginatedDinners = new PaginatedList<Dinner>(upcomingDinners, page ?? 0, pageSize);
            //    //upcomingDinners.Skip((page ?? 0) * pageSize).Take(pageSize).ToList();
            //return View(paginatedDinners);

            
            return View(paginatedDinners);
        }

        // GET: Dinners/Details/1
        public ActionResult Details(int id)
        {
            //var singleDinner = nerdDinnersDB.Dinners.Find(id);
            var singleDinner = iDinnerRepos.GetDinner(id);
            if (singleDinner == null)
            {
                return View("NotFound");
            }
            return View(singleDinner);
        }

        // GET: Dinners/Edit/1
        [Authorize]
        public ActionResult Edit(int id)
        {
            //var dinner = nerdDinnersDB.Dinners.Find(id);
            var dinner = iDinnerRepos.GetDinner(id);
            //ViewBag.Country = new SelectList(dinner.CountryCollection, "id", "value");
            ViewBag.CountryID = new SelectList(iDinnerRepos.GetCountries(), "CountryID", "Name", dinner.CountryID);
            if (!dinner.IsHostedBy(User.Identity.Name))
            {
                return View("InvalidOwner");
            }
            return View(new DinnerFromViewModel(dinner));
            //return View(dinner);
        }

        // POST: Dinners/Edit/1
        [HttpPost]
        [Authorize]
        public ActionResult Edit(int id, FormCollection formValues)
        {

            Dinner dinner = iDinnerRepos.GetDinner(id);
            if (!dinner.IsHostedBy(User.Identity.Name))
            {
                return View("InvalidOwner");
            }

            try
            {
                //dinner.Title = Request.Form["Title"];
                //dinner.Description = Request.Form["Description"];
                //dinner.EventDate = DateTime.Parse(Request.Form["EventDate"]);
                //dinner.Address = Request.Form["Address"];
                ////dinner.Country = new Country(Request.Form["CountryID"]);
                //dinner.Country = iDinnerRepos.GetCountry(Convert.ToInt32(Request.Form["CountryID"]));
                ////dinner.Country = Request.Form["Country"];
                //dinner.ContactEmail = Request.Form["ContactEmail"];
                UpdateModel(dinner);
                iDinnerRepos.Save();
                ViewBag.CountryID = new SelectList(iDinnerRepos.GetCountries(), "CountryID", "Name", dinner.CountryID);
                return RedirectToAction("Details", new {id = dinner.DinnerID});
            }
            catch
            {
                return View(new DinnerFromViewModel(dinner));
            }

            //if (ModelState.IsValid)
            //{
            //    dinner.DinnerID = id;
            //    if (!dinner.IsHostedBy(User.Identity.Name))
            //    {
            //        return View("InvalidOwner");
            //    }
            //    //nerdDinnersDB.Entry(dinner).State = EntityState.Modified;

            //    nerdDinnersDB.SaveChanges();
            //    return RedirectToAction("Details", new {id = dinner.DinnerID});
            //}
            //ViewBag.CountryID = new SelectList(nerdDinnersDB.Countries, "CountryID", "Name", dinner.CountryID);
            //return View(new DinnerFromViewModel(dinner));
            ////return View(dinner);
        }

        //[Authorize(Roles = "admin")]
        // GET: Dinners/Create
        public ActionResult Create()
        {
            Dinner dinner = new Dinner
            {
                EventDate = DateTime.Now.AddDays(7)
            };
            ViewBag.CountryID = new SelectList(iDinnerRepos.GetCountries(), "CountryID", "Name");
            return View(new DinnerFromViewModel(dinner));
            //return View(dinner);
        }

        // POST: /Dinners/Create
        [HttpPost]
        [Authorize]
        public ActionResult Create(Dinner dinner)
        {
            dinner.Country = iDinnerRepos.GetCountry(Convert.ToInt32(Request.Form["CountryID"]));
            if (ModelState.IsValid)
            {
                dinner.HostedBy = User.Identity.Name;

                RSVP rsvp = new RSVP();
                rsvp.AttendeeName = User.Identity.Name;
                //if (dinner.RSVPs == null)
                //{
                //    dinner.RSVPs = new List<RSVP>();
                //}
                dinner.RSVPs.Add(rsvp);
           
                //nerdDinnersDB.Dinners.Add(dinner);
                iDinnerRepos.Add(dinner);
                //nerdDinnersDB.SaveChanges();
                iDinnerRepos.Save();
                
                return RedirectToAction("Details", new {id=dinner.DinnerID});
            }

            //
            ViewBag.CountryID = new SelectList(iDinnerRepos.GetCountries(), "CountryID", "Name", dinner.CountryID);
            //return View(dinner);
            return View(new DinnerFromViewModel(dinner));
        }

        //GET: Dinners/Delete/1
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Dinner dinner = iDinnerRepos.GetDinner((int)id); //nerdDinnersDB.Dinners.Find(id);

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
            Dinner dinner = iDinnerRepos.GetDinner(id); //nerdDinnersDB.Dinners.Find(id);
            //nerdDinnersDB.Dinners.Remove(dinner);
            iDinnerRepos.Delete(dinner);
            //nerdDinnersDB.SaveChanges();
            iDinnerRepos.Save();
            return View("Deleted");
        }
    }
}