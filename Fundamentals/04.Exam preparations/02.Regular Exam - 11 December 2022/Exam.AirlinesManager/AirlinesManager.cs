using System;
using System.Collections.Generic;
using System.Linq;

namespace Exam.DeliveriesManager
{
    public class AirlinesManager : IAirlinesManager
    {
        private readonly HashSet<Airline> airlines = new HashSet<Airline>();
        private readonly HashSet<Flight> flights = new HashSet<Flight>();

        public void AddAirline(Airline airline) => airlines.Add(airline);

        public void AddFlight(Airline airline, Flight flight)
        {
            if (!airlines.Contains(airline)) throw new ArgumentException();

            airline.Flights.Add(flight);
            flights.Add(flight);
            flight.Airline = airline;
        }

        public bool Contains(Airline airline) => airlines.Contains(airline);

        public bool Contains(Flight flight) => flights.Contains(flight);

        public void DeleteAirline(Airline airline)
        {
            if (!Contains(airline))
            {
                throw new ArgumentException();
            }

            foreach (var flight in airline.Flights)
            {
                flight.Airline = null;
                flights.Remove(flight);
            }
            airline.Flights.Clear();
            airlines.Remove(airline);
        }
               
        public IEnumerable<Flight> GetAllFlights() => flights;
               
        public Flight PerformFlight(Airline airline, Flight flight)
        {
            if (!Contains(airline) || !Contains(flight))
            {
                throw new ArgumentException();
            }

            flight.IsCompleted = true;

            return flight;
        }

        public IEnumerable<Flight> GetCompletedFlights() => flights.Where(f => f.IsCompleted);

        public IEnumerable<Flight> GetFlightsOrderedByCompletionThenByNumber()
            => flights.OrderBy(f => f.IsCompleted)
                    .ThenBy(f => f.Number);

        public IEnumerable<Airline> GetAirlinesOrderedByRatingThenByCountOfFlightsThenByName()
           => airlines.OrderByDescending(a => a.Rating)
                   .ThenByDescending(a => a.Flights.Count)
                   .ThenBy(a => a.Name);

        public IEnumerable<Airline> GetAirlinesWithFlightsFromOriginToDestination(string origin, string destination)
            => airlines.Where(a => a.Flights.Count(f => f.IsCompleted == false
                                                        && f.Origin == origin
                                                        && f.Destination == destination
                                                  ) > 0);

    }
}
