using System;
using System.Collections.Generic;

namespace Exam.MobileX
{
    public class Program
    {
        static void Main(string[] args)
        {
            var repo = new VehicleRepository();

            Vehicle vehicle = new Vehicle(1 + "", "BMW", "X5", "Sofia", "Blue", 400, 90000, true);
            Vehicle vehicle2 = new Vehicle(2 + "", "Ford", "Escort", "Plovdiv", "Magenta", 500, 61000, false);
            Vehicle vehicle3 = new Vehicle(3 + "", "Audi", "A3", "Ruse", "Red", 300, 70000, false);
            Vehicle vehicle4 = new Vehicle(4 + "", "Audi", "A3", "Ruse", "Green", 500, 88000, true);
            Vehicle vehicle5 = new Vehicle(5 + "", "Audi", "A3", "Varna", "Magenta", 500, 50000, false);
            Vehicle vehicle6 = new Vehicle(6 + "", "Porsche", "Cayenne", "Plovdiv", "Black", 600, 55000, true);

            Vehicle vehicle7 = new Vehicle(7 + "", "Mercedes-Benz", "C220", "Plovdiv", "Black", 600, 100000, true);
            Vehicle vehicle8 = new Vehicle(8 + "", "Ford", "Mustang", "Plovdiv", "Black", 600, 110000, true);

            repo.AddVehicleForSale(vehicle, "George");
            repo.AddVehicleForSale(vehicle2, "George");
            repo.AddVehicleForSale(vehicle3, "Phill");
            repo.AddVehicleForSale(vehicle4, "Isacc");
            repo.AddVehicleForSale(vehicle5, "Igor");
            repo.AddVehicleForSale(vehicle6, "Donald");
            repo.AddVehicleForSale(vehicle7, "John");
            repo.AddVehicleForSale(vehicle8, "Jerry");

            var vehicles = new List<Vehicle>(repo.GetVehicles(new List<string>(new string[] { "BMW", "Cayenne", "Ruse", "Magenta" })));

            Console.WriteLine(vehicles.Count);
        }
    }
}
