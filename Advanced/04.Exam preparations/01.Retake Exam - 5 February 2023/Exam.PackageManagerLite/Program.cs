using System;
using System.Linq;

namespace Exam.PackageManagerLite
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var packageManager = new PackageManager();
            packageManager.RegisterPackage(new Package("1", "Test", DateTime.Now, "v.1"));
            var package = new Package("2", "Test1", DateTime.Now, "v.1");
            packageManager.RegisterPackage(package);

            packageManager.AddDependency("1", "2");

            var result = packageManager.GetOrderedPackagesByReleaseDateThenByVersion();

            Console.WriteLine(packageManager.GetIndependentPackages().First().Name);
        }
    }
}
