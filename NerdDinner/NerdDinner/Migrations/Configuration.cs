using System.Collections.Generic;
using NerdDinner.Models;

namespace NerdDinner.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<NerdDinner.Models.NerdDinners>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            ContextKey = "NerdDinner.Models.NerdDinners";
        }

        protected override void Seed(NerdDinner.Models.NerdDinners context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            var dinners = new List<Dinner>
            {
                new Dinner
                {
                    Title = "Sample Dinner 1",
                    EventDate = DateTime.Parse("12/31/2017"),
                    Address = "One Microsoft Way",
                    Country = "USA",
                    HostedBy = "ScottGu@gmail.com",
                    Description = ""
                },
                new Dinner
                {
                    Title = "Sample Dinner 2",
                    EventDate = DateTime.Parse("4/1/2017"),
                    Address = "Two Microsoft Way",
                    Country = "USA",
                    HostedBy = "ScottGu@gmail.com",
                    Description = ""
                }
            };

            dinners.ForEach(d => context.Dinners.Add(d));
        }
    }
}
