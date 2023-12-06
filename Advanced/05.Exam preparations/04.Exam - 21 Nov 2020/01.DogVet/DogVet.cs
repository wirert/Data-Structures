namespace _01.DogVet
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class DogVet : IDogVet
    {
        private Dictionary<string, Dog> dogsById = new Dictionary<string, Dog>();
        private Dictionary<string, Owner> ownersById = new Dictionary<string, Owner>();
        

        public int Size => dogsById.Count;

        public bool Contains(Dog dog) => dogsById.ContainsKey(dog.Id);

        public void AddDog(Dog dog, Owner owner)
        {
            if (Contains(dog) || owner.Dogs.ContainsKey(dog.Name)) 
            {
                throw new ArgumentException();
            }

            if (!ownersById.ContainsKey(owner.Id))
            {
                ownersById.Add(owner.Id, owner);
            }

            owner.Dogs.Add(dog.Name, dog);
            dog.Owner = owner;
            dogsById.Add(dog.Id, dog);
        }

        public Dog GetDog(string name, string ownerId)
        {
            if (!ownersById.ContainsKey(ownerId) || 
                !ownersById[ownerId].Dogs.ContainsKey(name))
            {
                throw new ArgumentException();
            }

            return ownersById[ownerId].Dogs[name];
        }

        public Dog RemoveDog(string name, string ownerId)
        {
            var dog = GetDog(name, ownerId);

            dogsById.Remove(dog.Id);
            ownersById[ownerId].Dogs.Remove(name); 
            
            return dog;
        }

        public IEnumerable<Dog> GetDogsByOwner(string ownerId)
        {
            if (!ownersById.ContainsKey(ownerId))
            {
                throw new ArgumentException();
            }

            return ownersById[ownerId].Dogs.Values;
        }

        public IEnumerable<Dog> GetDogsByBreed(Breed breed)
        {
            var dogs = dogsById.Values
                .Where(d => d.Breed == breed);
            if (dogs.Count() == 0)
            {
                throw new ArgumentException();
            }

            return dogs;
        }

        public void Vaccinate(string name, string ownerId)
        {
            var dog = GetDog(name, ownerId);
            dog.Vaccines++;
        }

        public void Rename(string oldName, string newName, string ownerId)
        {
            var dog = GetDog(oldName, ownerId);

            dog.Name = newName;

            ownersById[ownerId].Dogs.Remove(oldName);
            ownersById[ownerId].Dogs.Add(newName, dog);
        }

        public IEnumerable<Dog> GetAllDogsByAge(int age)
        {
            var dogs = dogsById.Values.Where(d => d.Age == age);

            if (dogs.Count() == 0) { throw new ArgumentException(); }

            return dogs;
        }

        public IEnumerable<Dog> GetDogsInAgeRange(int lo, int hi)
            => dogsById.Values.Where(d => d.Age >= lo && d.Age <= hi);

        public IEnumerable<Dog> GetAllOrderedByAgeThenByNameThenByOwnerNameAscending()
            => dogsById.Values
            .OrderBy(d => d.Age)
            .ThenBy(d => d.Name)
            .ThenBy(d => d.Owner.Name);
    }
}