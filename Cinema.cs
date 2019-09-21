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
    class Cinema
    {
        public string Name { get; private set; }
        public Dictionary<int, Auditorium> OwnAuditoriums { get; private set; } //= new HashSet<Auditorium>();
        public Cinema(string Name, byte NumberOfAuditoriums)
        {
            #region debug message
#if DEBUG
            Program.LogCaller();
#endif
            #endregion
            this.Name = Name;
            OwnAuditoriums = new Dictionary<int, Auditorium>();
            InitAuditoriums(NumberOfAuditoriums);
        }

        private void InitAuditoriums(byte NumberOfAuditoriums)
        {
            #region debug message
#if DEBUG
            Program.LogCaller();
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
            Program.LogCaller();
#endif
            #endregion
            return Name;
        }
        public override bool Equals(object obj)
        {
            #region debug message
#if DEBUG
            Program.LogCaller();
#endif
            #endregion
            Cinema cinema = obj as Cinema;
            return 
                    (cinema is object)
                 && (Name == cinema.Name);
        }
        #endregion
    }
}
