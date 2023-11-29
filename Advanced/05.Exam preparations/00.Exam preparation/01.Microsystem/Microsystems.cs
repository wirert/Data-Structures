namespace _01.Microsystem
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Microsystems : IMicrosystem
    {
        private Dictionary<int, Computer> computersByNumber = new Dictionary<int, Computer>();

        private Dictionary<Brand, HashSet<Computer>> computersByBrand = new Dictionary<Brand, HashSet<Computer>>();

        public void CreateComputer(Computer computer)
        {
            if (Contains(computer.Number))
            {
                throw new ArgumentException();
            }

            computersByNumber.Add(computer.Number, computer);

            if (!computersByBrand.ContainsKey(computer.Brand))
            {
                computersByBrand.Add(computer.Brand, new HashSet<Computer>());
            }

            computersByBrand[computer.Brand].Add(computer);
        }

        public bool Contains(int number) => computersByNumber.ContainsKey(number);

        public int Count() => computersByNumber.Count;

        public Computer GetComputer(int number)
        {
            if (!Contains(number))
            {
                throw new ArgumentException();
            }

            return computersByNumber[number];
        }

        public void Remove(int number)
        {
            var computer = GetComputer(number);

            computersByNumber.Remove(number);
            computersByBrand[computer.Brand].Remove(computer);
        }

        public void RemoveWithBrand(Brand brand)
        {
            if (!computersByBrand.ContainsKey(brand) || computersByBrand[brand].Count == 0)
            {
                throw new ArgumentException();
            }

            var computersToRemove = computersByBrand[brand];

            foreach (var computer in computersToRemove)
            {
                computersByNumber.Remove(computer.Number);
            }

            computersByBrand.Remove(brand);
        }

        public void UpgradeRam(int ram, int number)
        {
            var computer = GetComputer(number);

            if (ram > computer.RAM)
            {
                computer.RAM = ram;
            }
        }

        public IEnumerable<Computer> GetAllFromBrand(Brand brand)
        {
            if (!computersByBrand.ContainsKey(brand) || computersByBrand[brand].Count == 0)
            {
                return Enumerable.Empty<Computer>();
            }

            return computersByBrand[brand]
                .OrderByDescending(c => c.Price);
        }

        public IEnumerable<Computer> GetAllWithScreenSize(double screenSize)
            => computersByNumber.Values
            .Where(c => c.ScreenSize == screenSize)
            .OrderByDescending (c => c.Number);

        public IEnumerable<Computer> GetAllWithColor(string color)
        => computersByNumber.Values
            .Where(c => c.Color == color)
            .OrderByDescending(c => c.Price);

        public IEnumerable<Computer> GetInRangePrice(double minPrice, double maxPrice)
            => computersByNumber.Values
            .Where(c => c.Price >=  minPrice && c.Price <= maxPrice)
            .OrderByDescending(c => c.Price);
    }
}
