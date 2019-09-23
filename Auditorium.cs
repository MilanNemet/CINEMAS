#define DEBUG
//#undef DEBUG

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinemas
{
    /// <summary>
    /// A hall of a cinema theatre. The <see cref="Cinema"/> class aggregates the <see cref="Auditorium"/> class.
    /// </summary>
    class Auditorium
    {
        public byte Id { get; private set; }
        public byte Rows { get; private set; }
        public byte Columns { get; private set; }
        public Dictionary<string, Projection> OwnProjections { get; private set; }
        public Cinema OwnerCinema;

        public Auditorium(byte Id, Cinema Owner, byte Rows, byte Columns)
        {
            #region debug message
#if DEBUG
            IO_Handler.LogItsCaller();
#endif
            #endregion
            this.Id = Id;
            this.OwnerCinema = Owner;
            this.Rows = Rows;
            this.Columns = Columns;
            OwnProjections = new Dictionary<string, Projection>();
        }

        public void AddNewProjection()
        {
            #region debug message
#if DEBUG
            IO_Handler.LogItsCaller();
#endif
            #endregion
            if (OwnProjections.Count < 5)
            {
                string movieName = IO_Handler.EnterString($"{OwnerCinema}/{this}:\n" +
                    $"Enter the name of the movie beeing projected: ").ToUpper();
                byte movieLength = IO_Handler.EnterByte("Enter the length of this movie in minutes: ");
                Movie currentMovie = new Movie(movieName, movieLength);
                TestAndCreate(currentMovie);
            }
            else
            {
                throw new OperationCanceledException("Projection Limit Reached");
            }
        }
        private void TestAndCreate(Movie movie)
        {
                try
                {
                    OwnProjections.Add(movie.Name, new Projection(this, movie));
                    IO_Handler.SuccessMessage($"New projection \"{movie.Name}\" has been added");
                }
                catch (ArgumentException)
                {
                    IO_Handler.ErrorMessage("Operation canceled: This movie has already beeing projected here!");
                }
        }

        #region OVERRIDES
        public override string ToString()
        {
            #region debug message
#if DEBUG
            IO_Handler.LogItsCaller();
#endif
            #endregion
            return $"Auditorium No.{Id}.";
        }

        public override bool Equals(object obj)
        {
            #region debug message
#if DEBUG
            IO_Handler.LogItsCaller();
#endif
            #endregion
            return obj is Auditorium auditorium
                && (Id == auditorium.Id);
        }
        #endregion
    }
}
