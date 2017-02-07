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

        public override void InitializeDatabase(NerdDinners nd)
        {
            base.InitializeDatabase(nd);

            nd.Database.ExecuteSqlCommand(
                "CREATE FUNCTION [dbo].[DistanceBetween](@Lat1 as real, @Long1 as real, @Lat2 as real, @Long2 as real) " +
                "RETURNS real AS BEGIN " +
                "DECLARE @dLat1InRad as float(53); SET @dLat1InRad = @Lat1 * (PI() / 180.0); " +
                "DECLARE @dLong1InRad as float(53); SET @dLong1InRad = @Long1 * (PI() / 180.0); " +
                "DECLARE @dLat2InRad as float(53); SET @dLat2InRad = @Lat2 * (PI() / 180.0); " +
                "DECLARE @dLong2InRad as float(53); SET @dLong2InRad = @Long2 * (PI() / 180.0); " +
                "DECLARE @dLongitude as float(53); SET @dLongitude = @dLong2InRad - @dLong1InRad; " +
                "DECLARE @dLatitude as float(53); SET @dLatitude = @dLat2InRad - @dLat1InRad; " +
                "/* Intermediate result a. */ DECLARE @a as float(53); SET @a = SQUARE(SIN(@dLatitude / 2.0)) + COS(@dLat1InRad) * COS(@dLat2InRad) * SQUARE(SIN(@dLongitude / 2.0)); " +
                "/* Intermediate result c (great circle distance in Radians). */ " +
                "DECLARE @c as real; SET @c = 2.0 * ATN2(SQRT(@a), SQRT(1.0 - @a)); " +
                "DECLARE @kEarthRadius as real; /* SET kEarthRadius = 3956.0 miles */ SET @kEarthRadius = 6376.5; " +
                "/* kms */ DECLARE @dDistance as real; SET @dDistance = @kEarthRadius * @c; " +
                "return (@dDistance); END"
            );

        }

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
                    ContactEmail = "ScottGu@gmail.com",
                    Description = ""
                },
                new Dinner
                {
                    Title = "Sample Dinner 2",
                    EventDate = DateTime.Parse("4/1/2017"),
                    Address = "Two Microsoft Way",
                    Country = countires.Single(c => c.Name == "UK"),
                    ContactEmail = "ScottGu@gmail.com",
                    Description = ""
                }
            };

            dinners.ForEach(d => context.Dinners.Add(d));
        }
    }
}