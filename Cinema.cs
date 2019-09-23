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
    /// <see cref="Cinema"/> is a huge place within one or more <see cref="Auditorium"/>s, where <see cref="Projection"/>s take place about different <see cref="Movie"/>s.
    /// </summary>
    class Cinema : CommonAttributes
    {
        public Dictionary<int, Auditorium> OwnAuditoriums { get; private set; }

        public Cinema(string Name, byte NumberOfAuditoriums) : base(Name)
        {
            #region debug message
#if DEBUG
            IO_Handler.LogItsCaller();
#endif
            #endregion
            OwnAuditoriums = new Dictionary<int, Auditorium>();
            InitAuditoriums(NumberOfAuditoriums);
        }

        private void InitAuditoriums(byte NumberOfAuditoriums)
        {
            #region debug message
#if DEBUG
            IO_Handler.LogItsCaller();
#endif
            #endregion
            for (int indexer = 0; indexer < NumberOfAuditoriums; indexer++)
            {
                byte id = (byte)(indexer + 1);
                byte rows = IO_Handler.EnterByte("Enter the number of rows: ");
                byte cols = IO_Handler.EnterByte("Enter the number of columns: ");
                OwnAuditoriums.Add(id,new Auditorium(id, this, rows, cols));
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
            return Name;
        }
        public override bool Equals(object obj)
        {
            #region debug message
#if DEBUG
            IO_Handler.LogItsCaller();
#endif
            #endregion
            return obj is Cinema cinema
                 && (Name == cinema.Name);
        }
        #endregion
    }
}
