using System;

namespace Exam.MovieDatabase
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var db = new MovieDatabase();
            var actor1 = new Actor(Guid.NewGuid().ToString(), "Pesho", 29);
            db.AddActor(actor1);

            db.AddMovie(actor1, new Movie(Guid.NewGuid().ToString(), 120, "new movie", 5.5, 12000000));

            var movie = new Movie(Guid.NewGuid().ToString(), 143, "new movie 2", 5.5, 120.67);
            
            db.AddMovie(actor1, movie);
        }
    }
}
