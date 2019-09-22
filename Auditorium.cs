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
            Program.LogItsCaller();
#endif
            #endregion
            this.Id = Id;
            this.OwnerCinema = Owner;
            this.Rows = Rows;
            this.Columns = Columns;
            OwnProjections = new Dictionary<string, Projection>();
        }

        public void CreateNewProjection()
        {
            #region debug message
#if DEBUG
            Program.LogItsCaller();
#endif
            #endregion
            if (OwnProjections.Count < 5)
            {
                string movieName = IO_Handler.EnterString($"{OwnerCinema}/{this}\n" +
                    $"Enter the name of the movie beeing projected: ");
                byte movieLength = IO_Handler.EnterByte("Enter the length of this movie in minutes: ");
                Movie currentMovie = new Movie(movieName, movieLength);
                if (ObjectContainer.AllMovies.Contains(currentMovie))
                {
                    OwnProjections.Add(movieName, new Projection(this, currentMovie));
                }
                else
                {
                    OwnProjections.Add(movieName, new Projection(this, new Movie(movieName, movieLength)));
                }
            }
            else
            {
                new OperationCanceledException("Projection Limit Reached");
            }
        }

        #region OVERRIDES
        public override string ToString()
        {
            #region debug message
#if DEBUG
            Program.LogItsCaller();
#endif
            #endregion
            return $"Auditorium No.{Id}.";
        }

        public override bool Equals(object obj)
        {
            #region debug message
#if DEBUG
            Program.LogItsCaller();
#endif
            #endregion
            return obj is Auditorium auditorium
                && (Id == auditorium.Id);
        }
        #endregion
    }
}
