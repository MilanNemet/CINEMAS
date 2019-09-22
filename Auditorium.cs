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
        static public byte ID { get; private set; } = 0;
        public byte Id { get; private set; }
        public byte Rows { get; private set; }
        public byte Columns { get; private set; }
        public Dictionary<string,Projection> OwnProjections { get; private set; }

        public Auditorium(byte Rows, byte Columns)
        {
            #region debug message
#if DEBUG
            Program.LogThisCaller();
#endif
            #endregion
            Id = ++ID;
            this.Rows = Rows;
            this.Columns = Columns;
            OwnProjections = new Dictionary<string,Projection>();
        }

        public void CreateNewProjection()
        {
            #region debug message
#if DEBUG
            Program.LogThisCaller();
#endif
            #endregion
            if (OwnProjections.Count < 5)
            {
                string movieName = Program.EnterString("Enter the name of the movie beeing projected: ");
                byte movieLength = Program.EnterByte("Enter the length of this movie in minutes: ");
                OwnProjections.Add(movieName, new Projection(this, new Movie(movieName, movieLength)));
            }
        }

        #region OVERRIDES
        public override string ToString()
        {
            #region debug message
#if DEBUG
            Program.LogThisCaller();
#endif
            #endregion
            return $"Auditorium No.{Id}.";
        }

        public override bool Equals(object obj)
        {
            #region debug message
#if DEBUG
            Program.LogThisCaller();
#endif
            #endregion
            return obj is Auditorium auditorium
                && (Id == auditorium.Id);
        }
        #endregion
    }
}
