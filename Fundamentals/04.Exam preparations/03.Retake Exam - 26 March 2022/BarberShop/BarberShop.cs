using System;
using System.Collections.Generic;
using System.Linq;

namespace BarberShop
{
    public class BarberShop : IBarberShop
    {
        private HashSet<Client> clients = new HashSet<Client> ();
        private HashSet<Barber> barbers = new HashSet<Barber>();

        public void AddBarber(Barber b)
        {
            if (barbers.Add(b) == false)
            {
                throw new ArgumentException();
            }
        } 


        public void AddClient(Client c)
        {
            if (clients.Add(c) == false)
            {
                throw new ArgumentException();
            }
        }
        
        public bool Exist(Barber b) => barbers.Contains(b);

        public bool Exist(Client c) => clients.Contains(c);

        public IEnumerable<Barber> GetBarbers() 
            => barbers.AsEnumerable();

        public IEnumerable<Client> GetClients() 
            => clients.AsEnumerable();

        public void AssignClient(Barber b, Client c)
        {
            if (!Exist(b) || !Exist(c))
            {
                throw new ArgumentException();
            }
            
            c.Barber = b;
            b.Clients.Add(c);
        }

        public void DeleteAllClientsFrom(Barber b)
        {
            if (!Exist(b))
            {
                throw new ArgumentException();
            }
                        
            //foreach (var client in b.Clients)
            //{
            //    client.Barber = null;
            //}

            b.Clients.Clear();
        }

        public IEnumerable<Client> GetClientsWithNoBarber()
            => clients.Where(c => c.Barber == null);

        public IEnumerable<Barber> GetAllBarbersSortedWithClientsCountDesc()
            => barbers.OrderByDescending(b => b.Clients.Count);

        public IEnumerable<Barber> GetAllBarbersSortedWithStarsDecsendingAndHaircutPriceAsc()
            => barbers.OrderByDescending(b => b.Stars)
                      .ThenBy(b => b.HaircutPrice);

        public IEnumerable<Client> GetClientsSortedByAgeDescAndBarbersStarsDesc()
            => clients.Where(c => c.Barber != null)
                      .OrderByDescending(c => c.Age)
                      .ThenByDescending(c => c.Barber.Stars);
    }
}
