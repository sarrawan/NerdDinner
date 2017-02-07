using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using CodeFirstStoreFunctions;


namespace NerdDinner.Models
{
    public class NerdDinners : DbContext
    {
        public DbSet<Dinner> Dinners { get; set; }
        public DbSet<RSVP> RSVPs { get; set; }

        public DbSet<Country> Countries { get; set; }

        public IQueryable<Dinner> FindUpcomingDinners()
        {
            return from dinner in Dinners
                where dinner.EventDate > DateTime.Now
                orderby dinner.EventDate
                select dinner;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Add(new FunctionsConvention<NerdDinners>("dbo"));
        }

        [DbFunction("CodeFirstDatabaseSchema", "DistanceBetween")]
        public static double DistanceBetween(double lat1, double long1, double lat2, double long2)
        {
            throw new NotImplementedException("DistanceBetween Exception");
        }

        public IQueryable<Dinner> NearestDinners(double lat, double lon)
        {
            return from d in Dinners
                where DistanceBetween(lat, lon, d.Latitude, d.Longitude) < 100
                select d;
        }

        public IQueryable<Dinner> FindByLocation(float latitude, float longitude)
        {
            var dinners = from dinner in FindUpcomingDinners()
                join i in NearestDinners(latitude, longitude)
                on dinner.DinnerID equals i.DinnerID
                select dinner;
            return dinners;
        }
    }
}