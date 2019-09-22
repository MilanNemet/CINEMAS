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
            Program.LogThisCaller();
#endif
            #endregion
            OwnAuditoriums = new Dictionary<int, Auditorium>();
            InitAuditoriums(NumberOfAuditoriums);
        }

        private void InitAuditoriums(byte NumberOfAuditoriums)
        {
            #region debug message
#if DEBUG
            Program.LogThisCaller();
#endif
            #endregion
            for (int i = 0; i < NumberOfAuditoriums; i++)
            {
                byte rows = Program.EnterByte("Enter the number of rows: ");
                byte cols = Program.EnterByte("Enter the number of columns: ");
                OwnAuditoriums.Add(i+1,new Auditorium(rows, cols));
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
            return Name;
        }
        public override bool Equals(object obj)
        {
            #region debug message
#if DEBUG
            Program.LogThisCaller();
#endif
            #endregion
            return obj is Cinema cinema
                 && (Name == cinema.Name);
        }
        #endregion
    }
}
