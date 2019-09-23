#define DEBUG
//#undef DEBUG

using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinemas
{
    class Program
    {
        //Cinema-Auditorium-Movie-Projection should be placed under a new, common "Business Logic Layer" class
        //Class "Presentation Layer" is up to being planned
        static void Main()
        {
            Console.OutputEncoding = Encoding.UTF8;

            Cinema C1 = new Cinema("CinemaCity - Győr", 1);
            //Cinema C2 = new Cinema("CinemaCity - Aréna-Pláza", 2);
            //C1.OwnAuditoriums[1].CreateNewProjection();//kulcs bekérésére kell jó megoldás(jó helyen 1 method)

            try
            {
                C1.OwnAuditoriums[1].AddNewProjection();
                C1.OwnAuditoriums[1].AddNewProjection();
                C1.OwnAuditoriums[1].AddNewProjection();
            }
            catch (OperationCanceledException oc_exception)
            {
                Console.WriteLine(oc_exception.Message);
            }


            Projection p1 = ObjectContainer.FindProjectionByName();
            p1.ReserveSeat(0,0);
            p1.ReserveSeat(0,1);
            p1.ReserveSeat(1,0);
            p1.ReserveSeat(1,1);

            Projection p2 = ObjectContainer.FindProjectionByName();
            p2.ReserveSeat(0, 0);
            p2.ReserveSeat(0, 1);

            Projection Mostviewed = ObjectContainer.FindProjectionMostViewed();
            Console.WriteLine
                ($"Most viewed projection data:\n" +
                $"{Mostviewed}\n" +
                $"{Mostviewed.OwnMovie}\n" +
                $"{Mostviewed.OwnerAuditorium}\n" +
                $"{Mostviewed.OwnerAuditorium.OwnerCinema}");
            /*
            Projection p_first = C1.OwnAuditoriums[1].OwnProjections.First().Value;

            Console.WriteLine($"Cinema name: {C1.Name}");
            Console.WriteLine($"\tMovie name: {p_first.OwnMovie.Name}");
            Console.WriteLine(p_first.Name);

            ObjectContainer.MDB.ForEach(i => Console.WriteLine(i));
            */
        }
    }
}
