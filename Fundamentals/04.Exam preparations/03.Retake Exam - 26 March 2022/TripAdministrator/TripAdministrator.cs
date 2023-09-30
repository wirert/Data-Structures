using System;
using System.Collections.Generic;
using System.Linq;

namespace TripAdministrations
{
    public class TripAdministrator : ITripAdministrator
    {
        private HashSet<Trip> trips = new HashSet<Trip>();
        private HashSet<Company> companies = new HashSet<Company>();

        public void AddCompany(Company c)
        {
            if (companies.Add(c) == false)
            {
                throw new ArgumentException();
            }
        }

        public void AddTrip(Company c, Trip t)
        {
            if (Exist(c) == false)
            {
                throw new ArgumentException();
            }

            if (c.Trips.Count == c.TripOrganizationLimit)
            {
                return;
            }

            //c.CurrentTrips++;
            trips.Add(t);
            c.Trips.Add(t);
            t.Company = c;
        }

        public bool Exist(Company c)
            => companies.Contains(c);

        public bool Exist(Trip t)
            => trips.Contains(t);   

        public void RemoveCompany(Company c)
        {
            if (Exist(c) == false)
            {
                throw new ArgumentException();
            }
            
            trips.ExceptWith(c.Trips);
            c.Trips.Clear();
            companies.Remove(c);
        }

        public IEnumerable<Company> GetCompanies() => companies;

        public IEnumerable<Trip> GetTrips() => trips;

        public void ExecuteTrip(Company c, Trip t)
        {
            if (!Exist(c) || !Exist(t) || !c.Trips.Contains(t))
            {
                throw new ArgumentException();
            }

            //c.Trips.Remove(t);
            //c.CurrentTrips--;
            trips.Remove(t);
        }

        public IEnumerable<Company> GetCompaniesWithMoreThatNTrips(int n)
            => companies.Where(c => c.Trips.Count > n);

        public IEnumerable<Trip> GetTripsWithTransportationType(Transportation t)
            => trips.Where(trip => trip.Transportation == t);

        public IEnumerable<Trip> GetAllTripsInPriceRange(int lo, int hi)
            => trips.Where(t => t.Price >= lo && t.Price <= hi);
    }
}
