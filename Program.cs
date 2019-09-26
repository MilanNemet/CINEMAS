#define DEBUG
#undef DEBUG

using System;

namespace Cinemas
{
    class Program
    {
        static void Main()
        {
            Console.CursorVisible = false;
            Console.BackgroundColor = ConsoleColor.DarkMagenta;
            Console.WriteLine("Contorols:");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("Enter menu: EnterKey \nQuit menu: EscapeKey \nNavigate up: UpArrow \nNavigate down: DownArrow \nFirst Menu Item: PageUp \nLast Menu Item: PageDown \n\n");
            Console.ResetColor();
            Console.WriteLine("\tPress any key to continue!");
            Console.ReadKey(false);
            Console.Clear();
            PresentationLayer PL = new PresentationLayer();

            //PL.MainMenu();


            /*
            Cinema C1 = new Cinema("CinemaCity - Győr", 1);
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
            Projection p1 = C1.OwnAuditoriums[1].FindProjectionByName();
            p1.ReserveSeat(0,0);
            p1.ReserveSeat(0,1);
            p1.ReserveSeat(1,0);
            p1.ReserveSeat(1,1);
            Projection p2 = C1.OwnAuditoriums[1].FindProjectionByName();
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
