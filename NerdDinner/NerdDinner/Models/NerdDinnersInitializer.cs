using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Web;

namespace NerdDinner.Models
{
    public class NerdDinnersInitializer : DropCreateDatabaseIfModelChanges<NerdDinners>
    {
        protected override void Seed(NerdDinners context)
        {
            var countires = new List<Country>
            {
                new Country {Name = "USA"},
                new Country {Name = "UK"},
                new Country {Name = "Netherlands"}
            };

            var dinners = new List<Dinner>
            {
                new Dinner
                {
                    Title = "Sample Dinner 1",
                    EventDate = DateTime.Parse("12/31/2017"),
                    Address = "One Microsoft Way",
                    Country = countires.Single(c => c.Name == "USA"),
                    HostedBy = "ScottGu@gmail.com",
                    Description = ""
                },
                new Dinner
                {
                    Title = "Sample Dinner 2",
                    EventDate = DateTime.Parse("4/1/2017"),
                    Address = "Two Microsoft Way",
                    Country = countires.Single(c => c.Name == "UK"),
                    HostedBy = "ScottGu@gmail.com",
                    Description = ""
                }
            };

            dinners.ForEach(d => context.Dinners.Add(d));
        }
    }
}