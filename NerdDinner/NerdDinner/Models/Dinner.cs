using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web.Mvc;

namespace NerdDinner.Models
{
    [Bind(Include = "Title, Description, EventDate, Address, Country, ContactEmail, CountryID, HostedBy, RSVPs, Latitude, Longitude")]
    public class Dinner
    {
        public int DinnerID { get; set; }

        [Required(ErrorMessage = "*Please enter a Dinner Title")]
        [StringLength(20, ErrorMessage = "Title is too long")]
        public string Title { get; set; }

        [Required(ErrorMessage = "*Please enter the Date of the Dinner")]
        public DateTime EventDate { get; set; }

        [Required(ErrorMessage = "*Please enter the Location of the Dinner")]
        [StringLength(100, ErrorMessage = "Address is too long")]
        public string Address { get; set; }

        [Required(ErrorMessage = "*Please enter your email address")]
        [RegularExpression(".+\\@.+\\..+", ErrorMessage = "Please enter a valid email address")]
        public string ContactEmail { get; set; }

        [Required(ErrorMessage = "Please enter who is hosting the Dinner")]
        public string HostedBy { get; set; }

        [Required(ErrorMessage = "*Please enter the Country of the Dinner")]
        [DisplayName("Country")]
        public int CountryID { get; set; }

        public string Description { get; set; }
        public virtual Country Country { get; set; }

        
        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public virtual ICollection<RSVP> RSVPs { get; set; } = new List<RSVP>();

        public bool IsHostedBy(string userName)
        {
            return ContactEmail.Equals(userName, StringComparison.InvariantCultureIgnoreCase);
        }

        public bool IsUserRegistered(string userName)
        {
            return RSVPs.Any(r => r.AttendeeName.Equals(userName, StringComparison.InvariantCultureIgnoreCase));
        }

        public bool IsValid
        {
            get { return (GetRuleViolations().Count() == 0); }
        }

        public IEnumerable<RuleViolation> GetRuleViolations()
        {

            if (String.IsNullOrEmpty(Title))
                yield return new RuleViolation("Title required", "Title");

            if (String.IsNullOrEmpty(Description))
                yield return new RuleViolation("Description required", "Description");

            if (String.IsNullOrEmpty(HostedBy))
                yield return new RuleViolation("HostedBy required", "HostedBy");

            if (String.IsNullOrEmpty(Address))
                yield return new RuleViolation("Address required", "Address");

            if (String.IsNullOrEmpty(ContactEmail))
                yield return new RuleViolation("Email required", "ContactEmail");

            yield break;
        }


    }

    public class RuleViolation
    {
        public string ErrorMessage { get; private set; }
        public string PropertyName { get; private set; }

        public RuleViolation(string errorMessage, string propertyName)
        {
            ErrorMessage = errorMessage;
            PropertyName = propertyName;
        }
    }
}