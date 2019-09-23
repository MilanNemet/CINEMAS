using System;
using System.Linq;
using System.Collections.Generic;

namespace Cinemas
{
    static class ObjectContainer
    {
        public static List<Movie> MDB = new List<Movie>();
        public static List<Projection> PDB = new List<Projection>();

        public static Projection FindProjectionMostViewed()
        {
            Projection MostViewed = PDB.OrderByDescending(i => i.ReservedSeatsCount).FirstOrDefault();
            return MostViewed;
        }
        public static Projection FindProjectionByName()
        {
            if (PDB.Count > 0)
            {
                return ReturnProjectionByName();
            }
            else
            {
                throw new InvalidOperationException("There hasn't been any Projections created yet.");
            }
        }
        private static Projection ReturnProjectionByName()
        {
            string projectionName = "";
            while (!PDB.Contains(PDB.Find(projection => projection.Name == projectionName)))
            {
                projectionName = IO_Handler.EnterString("Name of the movie you are looking for: ").ToUpper();
                if (!PDB.Contains(PDB.Find(i => i.Name == projectionName)))
                {
                    IO_Handler.ErrorMessage("There is no such movie in the database with the given name!");
                    Console.WriteLine("Please, pick one from the following:");
                    IO_Handler.PrintCollection(PDB);
                }
            }
            Projection Result = PDB.Find(i => i.Name == projectionName);
            return Result;
        }
    }
}
