using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NerdDinner.Models
{
    public class DinnerFromViewModel
    {
        NerdDinners nerdDinnerDB = new NerdDinners();

        public Dinner Dinner { get; set; }
        public SelectList Countries { get; set; }

        public DinnerFromViewModel(Dinner dinner)
        {
            Dinner = dinner;
            Countries = new SelectList(nerdDinnerDB.Countries, dinner.Country);
        }
    }
}