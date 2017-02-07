using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NerdDinner.Models;

namespace NerdDinner.Tests.Models
{

    [TestClass]
    public class DinnerTest
    {

        List<Country> countires = new List<Country>
            {
                new Country {Name = "USA"},
                new Country {Name = "UK"},
                new Country {Name = "Netherlands"}
            };

        [TestMethod]
        public void Dinner_Should_Not_Be_Valid_When_Some_Properties_Incorrect()
        {
            Dinner dinner = new Dinner
            {
                Title = "Test Title",
                Country = countires.Single(c => c.Name == "USA"),
                ContactEmail = "Bogus@g.com"
            };

            //bool isValid = dinner.IsValid;

            //Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void Dinner_Should_Be_Valid_When_All_Properties_Correct()
        {
            Dinner dinner = new Dinner()
            {
                Title = "Test Title",
                Description = "Some description",
                EventDate = DateTime.Now,
                HostedBy = "Blah",
                Address = "One Microsoft Way",
                Country = countires.Single(c => c.Name == "USA"),
                ContactEmail = "blah@y.com",
                Latitude = 93,
                Longitude = -92
            };

            //bool isValid = dinner.IsValid;

            //Assert.IsTrue(isValid);
        }
    }
}
