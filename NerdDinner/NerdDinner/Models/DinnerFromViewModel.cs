using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NerdDinner.Models
{
    public class DinnerFromViewModel
    {

        public Dinner Dinner { get; set; }
        public SelectList Countries { get; set; }

        public DinnerFromViewModel(Dinner dinner)
        {
            Dinner = dinner;
            IEnumerable<string> list = new List<string>
            {
                "USA",
                "UK",
                "Netherlands"
            };
            Countries = new SelectList(list, list.First());
        }

        public enum CountriesEnum
        {
            USA = 1,
            UK = 2,
            Netherlands = 3
        }
    }
}