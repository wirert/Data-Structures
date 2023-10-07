namespace PublicTransportManagementSystem
{
    using System.Collections.Generic;

    public class Bus
    {
        public string Id { get; set; }
    
        public string Number { get; set; }
    
        public int Capacity { get; set; }

        public HashSet<Passenger>  Passengers { get; set; } = new HashSet<Passenger>();

        public override string ToString()
        {
            return this.Id;
        }
    }
}