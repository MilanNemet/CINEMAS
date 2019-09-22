#define DEBUG
//#undef DEBUG

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Cinemas
{
    class Program
    {
        static public void LogThisCaller()
        {
            StackFrame frame = new StackFrame(1);
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine($"({frame.GetMethod().DeclaringType.Name}.{frame.GetMethod().Name}) has been called successfully!");
            Console.ResetColor();
        }
        #region STATIC METHODS EnterType()...
        public static string EnterString(string message="")
        {
            Console.Write(message);
            return Console.ReadLine();
        }
        public static byte EnterByte(string message="")
        {
            byte result;
            Console.Write(message);
            while (!byte.TryParse(Console.ReadLine(), out result))
            {
                Console.WriteLine("Incorrect value! \nTry again in range of 1-255.\n\n");
                Console.Write(message);
            }
            return result;
        }
        #endregion

        //Cinema-Auditorium-Movie-Projection should be placed under a new, common "Business Logic Layer" class
        //Class "Presentation Layer" is up to being planned
        static void Main()
        {
            Console.OutputEncoding = Encoding.UTF8;

            //Console.WriteLine("Enter cinema name: ");
            string input1 = "CinemaCity";// Console.ReadLine();

            //Console.WriteLine("Enter number of auditoriums: ");
            byte input2 = 1;// byte.Parse(Console.ReadLine());

            Cinema C1 = new Cinema(input1, input2);

            C1.OwnAuditoriums[1].CreateNewProjection();//kulcs bekérésére kell jó megoldás(jó helyen 1 method)

            //Console.WriteLine(C1.OwnAuditoriums[1].OwnProjections.First().Value.ReserveSeat(4,4));
            //Console.WriteLine(C1.OwnAuditoriums[1].OwnProjections.First().Value.ReserveSeat(3,3));
            //Console.WriteLine(C1.OwnAuditoriums[1].OwnProjections.First().Value.ReserveSeat(3,4));
            //Console.WriteLine(C1.OwnAuditoriums[1].OwnProjections.First().Value.ReserveSeat(3,2));
            //Console.WriteLine(C1.OwnAuditoriums[1].OwnProjections.First().Value.ReserveSeat(3,1));
            //Console.WriteLine("\n\n");
            //C1.OwnAuditoriums[1].OwnProjections.First().Value.PrintOwnSeatsByAvailability();

            Projection p1 = C1.OwnAuditoriums[1].OwnProjections.First().Value;

            Console.WriteLine($"Cinema name: {C1.Name}");
            Console.WriteLine($"\tMovie name: {p1.OwnMovie.Name}");
            Console.WriteLine($"\t{p1.Name}");
        }
    }
}
