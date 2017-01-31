using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace NerdDinner.Models
{
    public class NerdDinnersInitializer : DropCreateDatabaseIfModelChanges<NerdDinners>
    {
        protected override void Seed(NerdDinners context)
        {
            var dinners = new List<Dinner>
            {
                new Dinner
                {
                    Title = "Sample Dinner 1",
                    EventDate = DateTime.Parse("12/31/2017"),
                    Address = "One Microsoft Way",
                    Country = "USA",
                    HostedBy = "ScottGu"
                },
                new Dinner
                {
                    Title = "Sample Dinner 2",
                    EventDate = DateTime.Parse("4/1/2017"),
                    Address = "Two Microsoft Way",
                    Country = "USA",
                    HostedBy = "ScottGu"
                }
            };
            
            dinners.ForEach(d => context.Dinners.Add(d));
        }
    }
}