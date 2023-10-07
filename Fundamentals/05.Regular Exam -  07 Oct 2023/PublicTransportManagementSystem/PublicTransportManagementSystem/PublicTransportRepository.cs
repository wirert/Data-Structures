using System;
using System.Collections.Generic;
using System.Linq;

namespace PublicTransportManagementSystem
{
    public class PublicTransportRepository : IPublicTransportRepository
    {
        private HashSet<Bus> buses = new HashSet<Bus>();
        private HashSet<Passenger> passengers = new HashSet<Passenger>();

        public void RegisterPassenger(Passenger passenger)
             => passengers.Add(passenger);

        public void AddBus(Bus bus) => buses.Add(bus);

        public bool Contains(Passenger passenger) => passengers.Contains(passenger);

        public bool Contains(Bus bus) => buses.Contains(bus);

        public IEnumerable<Bus> GetBuses() => buses;

        public void BoardBus(Passenger passenger, Bus bus)
        {
            if (!Contains(passenger) || !Contains(bus))
            {
                throw new ArgumentException();
            }

            if (bus.Passengers.Count < bus.Capacity)
            {
                bus.Passengers.Add(passenger);
            }
        }

        public void LeaveBus(Passenger passenger, Bus bus)
        {
            if (!Contains(passenger) || !Contains(bus) || !bus.Passengers.Contains(passenger))
            {
                throw new ArgumentException();
            }

            bus.Passengers.Remove(passenger);
        }

        public IEnumerable<Passenger> GetPassengersOnBus(Bus bus) => bus.Passengers;

        public IEnumerable<Bus> GetBusesOrderedByOccupancy() 
            => buses.OrderBy(b => b.Passengers.Count);

        public IEnumerable<Bus> GetBusesWithCapacity(int capacity)
            => buses.Where(b => b.Capacity >= capacity);
    }
}