namespace _01.Inventory
{
    using _01.Inventory.Interfaces;
    using _01.Inventory.Models;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    public class Inventory : IHolder
    {
        private Dictionary<int, IWeapon> weaponsById = new Dictionary<int, IWeapon>();
        private Dictionary<Category, HashSet<IWeapon>> weaponsByCategory = new Dictionary<Category, HashSet<IWeapon>>();
        private List<IWeapon> weapons = new List<IWeapon>();
        

        public int Capacity => weaponsById.Count;

        public void Add(IWeapon weapon)
        {
            weaponsById.Add(weapon.Id, weapon);

            if (!weaponsByCategory.ContainsKey(weapon.Category))
            {
                weaponsByCategory.Add(weapon.Category, new HashSet<IWeapon>());
            }

            weaponsByCategory[weapon.Category].Add(weapon);
            weapons.Add(weapon);
        }

        public void Clear()
        {
            weaponsById.Clear();
            weaponsByCategory.Clear();
            weapons.Clear();
        }

        public bool Contains(IWeapon weapon) => weaponsById.ContainsKey(weapon.Id);

        public void EmptyArsenal(Category category)
        {
            foreach (var weapon in weaponsByCategory[category])
            {
                weapon.Ammunition = 0;
            }
        }

        public bool Fire(IWeapon weapon, int ammunition)
        {
            ValidateWeapon(weapon);

            if (weapon.Ammunition - ammunition < 0)
            {
                return false;
            }

            weapon.Ammunition -= ammunition;

            return true;
        }

        public IWeapon GetById(int id)
        {
            if (!weaponsById.ContainsKey(id))
            {
                return null;
            }

            return weaponsById[id];
        }

        public IEnumerator GetEnumerator() => weapons.GetEnumerator();

        public int Refill(IWeapon weapon, int ammunition)
        {
            ValidateWeapon(weapon);

            if (weapon.Ammunition + ammunition > weapon.MaxCapacity)
            {
                weapon.Ammunition = weapon.MaxCapacity;
            }
            else
            {
                weapon.Ammunition += ammunition;
            }

            return weapon.Ammunition;
        }

        public IWeapon RemoveById(int id)
        {
            if (!weaponsById.ContainsKey(id))
            {
                throw new InvalidOperationException("Weapon does not exist in inventory!");
            }

            var weapon = weaponsById[id];
            weaponsByCategory[weapon.Category].Remove(weapon);
            weaponsById.Remove(id);
            weapons.Remove(weapon);

            return weapon;
        }

        public int RemoveHeavy()
        {
            var result = weaponsByCategory[Category.Heavy].Count;

            foreach (var weapon in weaponsByCategory[Category.Heavy])
            {
                weaponsById.Remove(weapon.Id);
                weapons.Remove(weapon);
            }

            weaponsByCategory.Remove(Category.Heavy);

            return result;
        }

        public List<IWeapon> RetrieveAll() => weapons;

        public List<IWeapon> RetriveInRange(Category lower, Category upper)
            => weapons.Where(w => w.Category >= lower && w.Category <= upper).ToList();
        
        public void Swap(IWeapon firstWeapon, IWeapon secondWeapon)
        {
            ValidateWeapon(firstWeapon);
            ValidateWeapon(secondWeapon);

            if (firstWeapon.Category != secondWeapon.Category)
            {
                return;
            }

            var firstIndex = weapons.IndexOf(firstWeapon);
            var secondIndex = weapons.IndexOf(secondWeapon);

            weapons[firstIndex] = secondWeapon;
            weapons[secondIndex] = firstWeapon;
        }

        private void ValidateWeapon(IWeapon weapon)
        {
            if (!Contains(weapon))
            {
                throw new InvalidOperationException("Weapon does not exist in inventory!");
            }
        }
    }
}
