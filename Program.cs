#define DEBUG
//#undef DEBUG

using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Cinemas
{
    class Program
    {
        static public void LogItsCaller()
        {
            StackFrame frame = new StackFrame(1);
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine($"({frame.GetMethod().DeclaringType.Name}.{frame.GetMethod().Name}) has been called successfully!");
            Console.ResetColor();
        }

        //Cinema-Auditorium-Movie-Projection should be placed under a new, common "Business Logic Layer" class
        //Class "Presentation Layer" is up to being planned
        static void Main()
        {
            Console.OutputEncoding = Encoding.UTF8;

            Cinema C1 = new Cinema("CinemaCity - Győr", 2);
            //Cinema C2 = new Cinema("CinemaCity - Aréna-Pláza", 2);

            //C1.OwnAuditoriums[1].CreateNewProjection();//kulcs bekérésére kell jó megoldás(jó helyen 1 method)

            C1.OwnAuditoriums[2].CreateNewProjection();
            C1.OwnAuditoriums[2].CreateNewProjection();
            C1.OwnAuditoriums[2].CreateNewProjection();
            C1.OwnAuditoriums[2].CreateNewProjection();
            C1.OwnAuditoriums[2].CreateNewProjection();
            C1.OwnAuditoriums[2].CreateNewProjection();
            C1.OwnAuditoriums[2].CreateNewProjection();



            Projection p1 = C1.OwnAuditoriums[1].OwnProjections.First().Value;

            Console.WriteLine($"Cinema name: {C1.Name}");
            Console.WriteLine($"\tMovie name: {p1.OwnMovie.Name}");
            Console.WriteLine();

            ObjectContainer.AllMovies.ForEach(i => Console.WriteLine(i));
        }
    }
}
