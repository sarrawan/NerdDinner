﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NerdDinner.Models;

namespace NerdDinner.Controllers
{

    public class JsonDinner
    {
        public int DinnerID { get; set; }
        public string Title { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Description { get; set; }
        public int RSVPCount { get; set; }
    }

    public class SearchController : Controller
    {
        NerdDinners nerdDinnerDB = new NerdDinners();

        // GET: Search
        [HttpPost]
        public ActionResult SearchByLocation(float longitude, float latitude)
        {
            var dinners = nerdDinnerDB.FindByLocation(latitude, longitude);
            var jsonDinners = from dinner in dinners
                select new JsonDinner
                {
                    DinnerID = dinner.DinnerID,
                    Latitude = dinner.Latitude,
                    Longitude = dinner.Longitude,
                    Title = dinner.Title,
                    Description = dinner.Description,
                    RSVPCount = dinner.RSVPs.Count
                };
    
            return Json(jsonDinners.ToList());
        }
    }
}