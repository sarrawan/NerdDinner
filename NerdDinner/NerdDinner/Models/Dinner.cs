using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web.Mvc;

namespace NerdDinner.Models
{
    [Bind(Include = "Title, Description, EventDate, Address, Country, HostedBy, CountryID")]
    public class Dinner
    {
        public int DinnerID { get; set; }

        [Required(ErrorMessage = "*Please enter a Dinner Title")]
        [StringLength(20, ErrorMessage = "Title is too long")]
        public string Title { get; set; }

        [Required(ErrorMessage = "*Please enter the Date of the Dinner")]
        public DateTime EventDate { get; set; }

        [Required(ErrorMessage = "*Please enter the Location of the Dinner")]
        [StringLength(30, ErrorMessage = "Address is too long")]
        public string Address { get; set; }

        [Required(ErrorMessage = "*Please enter your email address")]
        [RegularExpression(".+\\@.+\\..+", ErrorMessage = "Please enter a valid email address")]
        public string HostedBy { get; set; }

        [Required(ErrorMessage = "*Please enter the Country of the Dinner")]
        [DisplayName("Country")]
        public int CountryID { get; set; }

        public string Description { get; set; }
        public virtual Country Country { get; set; }

        public virtual ICollection<RSVP> RSVPs { get; set; }


    }
}