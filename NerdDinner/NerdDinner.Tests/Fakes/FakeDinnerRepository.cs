using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NerdDinner.Models;

namespace NerdDinner.Tests.Fakes
{
    public class FakeDinnerRepository 
    {
        private List<Dinner> dinnerList;

        public FakeDinnerRepository(List<Dinner> dinners)
        {
            dinnerList = dinners;
        }

        public DbSet<Country> Countries
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public DbSet<Dinner> Dinners
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public DbSet<RSVP> RSVPs
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public DbEntityEntry Entry(object entity)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Dinner> FindByLocation(float latitude, float longitude)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Dinner> FindUpcomingDinners()
        {
            return (from dinner in dinnerList
                    where dinner.EventDate > DateTime.Now
                    select dinner).AsQueryable();
        }

        public void SaveChanges()
        {
            foreach (Dinner dinner in dinnerList)
            {
                //if (!dinner.IsValid)
                  //  throw new ApplicationException("Rule violations");
            }
        }
    }
}
