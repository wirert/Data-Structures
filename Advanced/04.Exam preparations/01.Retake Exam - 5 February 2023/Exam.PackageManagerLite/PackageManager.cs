using System;
using System.Collections.Generic;
using System.Linq;

namespace Exam.PackageManagerLite
{
    public class PackageManager : IPackageManager
    {
        private Dictionary<string, Package> packages = new Dictionary<string, Package>();
        private Dictionary<string, Dictionary<string, Package>> packageVersionsByName = new Dictionary<string, Dictionary<string, Package>>();

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
            foreach (var packagesByVersion in packageVersionsByName.Values)
            {
                var latestPackageVersion = packagesByVersion.Values
                    .OrderByDescending(p => p.ReleaseDate)
                    .First();

                result.Add(latestPackageVersion);
            }
            return result
                .OrderByDescending(p => p.ReleaseDate)
                .ThenBy(p => p.Version);
        }

        public void RegisterPackage(Package package)
        {
            if (
                packageVersionsByName.ContainsKey(package.Name) &&
                packageVersionsByName[package.Name].ContainsKey(package.Version)
                )
            {
                throw new ArgumentException();
            }

            packages.Add(package.Id, package);

            if (!packageVersionsByName.ContainsKey(package.Name))
            {
                packageVersionsByName.Add(package.Name, new Dictionary<string, Package>());
            }

            packageVersionsByName[package.Name].Add(package.Version, package);
        }

        public void RemovePackage(string packageId)
        {
            if (!packages.ContainsKey(packageId))
            {
                throw new ArgumentException();
            }

            var package = packages[packageId];
            packageVersionsByName[package.Name].Remove(package.Version);

            if (packageVersionsByName[package.Name].Count == 0)
            {
                packageVersionsByName.Remove(package.Name);
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
