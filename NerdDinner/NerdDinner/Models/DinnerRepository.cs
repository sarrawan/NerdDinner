﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace NerdDinner.Models
{
    public interface IDinnerRepository
    {
        IQueryable<Dinner> FindAllDinners();
        IQueryable<Dinner> FindUpcomingDinners();
        Dinner GetDinner(int id);
        Country GetCountry(int id);
        void Add(Dinner dinner);
        void Delete(Dinner dinner);
        void Save();
        IQueryable<Country> GetCountries();
        IQueryable<Dinner> FindByLocation(float lat, float lon);
    }

    public class DinnerRepository : IDinnerRepository
    {
        private NerdDinners db = new NerdDinners();

        //
        // Query Methods

        public IQueryable<Dinner> FindAllDinners()
        {
            return db.Dinners;
        }

        public IQueryable<Dinner> FindUpcomingDinners()
        {
            return from dinner in db.Dinners
                   where dinner.EventDate > DateTime.Now
                   orderby dinner.EventDate
                   select dinner;
        }

        public Dinner GetDinner(int id)
        {
            return db.Dinners.SingleOrDefault(d => d.DinnerID == id);
        }

        public Country GetCountry(int id)
        {
            return db.Countries.SingleOrDefault(c => c.CountryID == id);
        }

        //
        // Insert/Delete Methods

        public void Add(Dinner dinner)
        {
            db.Dinners.Add(dinner);
        }

        public void Delete(Dinner dinner)
        {
            //db.RSVPs.DeleteAllOnSubmit(dinner.RSVPs);
            //db.Dinners.DeleteOnSubmit(dinner);
            db.RSVPs.RemoveRange(dinner.RSVPs);
            db.Dinners.Remove(dinner);
        }

        //
        // Persistence

        public void Save()
        {
            db.SaveChanges();
        }

        public IQueryable<Country> GetCountries()
        {
            return db.Countries;
        }

        public IQueryable<Dinner> FindByLocation(float latitude, float longitude)
        {
            return db.FindByLocation(latitude, longitude);
        }

    }
}