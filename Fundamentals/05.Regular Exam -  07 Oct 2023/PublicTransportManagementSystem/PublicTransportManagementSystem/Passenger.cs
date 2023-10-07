namespace PublicTransportManagementSystem
{
    public class Passenger
    {
        public string Id { get; set; }
    
        public string Name { get; set; }

        public override bool Equals(object obj)
        {
            return Id == (obj as Passenger).Id;
        }
    }
}