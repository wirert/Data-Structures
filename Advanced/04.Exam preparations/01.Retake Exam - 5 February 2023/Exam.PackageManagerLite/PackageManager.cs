using System;
using System.Collections.Generic;
using System.Linq;

namespace Exam.PackageManagerLite
{
    public class PackageManager : IPackageManager
    {
        private Dictionary<string, Package> packages = new Dictionary<string, Package>();
        private Dictionary<string, Dictionary<string, Package>> versionsByName = new Dictionary<string, Dictionary<string, Package>>();

        public void AddDependency(string packageId, string dependencyId)
        {
            if (!packages.ContainsKey(packageId) || !packages.ContainsKey(dependencyId))
            {
                throw new ArgumentException();
            }

            var package = packages[packageId];
            var dependency = packages[dependencyId];

            package.Dependencies.Add(dependency);
            dependency.Dependants.Add(package);
        }

        public bool Contains(Package package) => packages.ContainsKey(package.Id);

        public int Count() => packages.Count;

        public IEnumerable<Package> GetDependants(Package package)
        {
            if (!packages.ContainsKey(package.Id))
            {
                throw new ArgumentException();
            }

            return package.Dependants;
        }

        public IEnumerable<Package> GetIndependentPackages()
            => packages.Values
            .Where(p => p.Dependencies.Count == 0)
            .OrderByDescending(p => p.ReleaseDate)
            .ThenBy(p => p.Version);

        public IEnumerable<Package> GetOrderedPackagesByReleaseDateThenByVersion()
        {
            var result = new List<Package>();
            foreach (var packByVer in versionsByName.Values)
            {
              result.Add(packByVer.Values.OrderByDescending(p => p.ReleaseDate).First());
            }
              return result.OrderByDescending(p => p.ReleaseDate)
                .ThenBy(p => p.Version);
        }

        public void RegisterPackage(Package package)
        {
            if (versionsByName.ContainsKey(package.Name) && versionsByName[package.Name].ContainsKey(package.Version))
            {
                throw new ArgumentException();            
            }

            packages.Add(package.Id, package);

            if(!versionsByName.ContainsKey(package.Name))
            {
                versionsByName.Add(package.Name, new Dictionary<string, Package>());
            }

            versionsByName[package.Name].Add(package.Version, package);
        }

        public void RemovePackage(string packageId)
        {
            if (!packages.ContainsKey(packageId))
            {
                throw new ArgumentException();
            }

            var package = packages[packageId];
            versionsByName[package.Name].Remove(package.Version);
            if (versionsByName[package.Name].Count == 0)
            {
                versionsByName.Remove(package.Name);
            }
            packages.Remove(packageId);

            foreach (var dependant in package.Dependants)
            {
                dependant.Dependencies.Remove(package);
            }

            foreach (var dependency in package.Dependencies)
            {
                dependency.Dependants.Remove(package);
            }
        }
    }
}
