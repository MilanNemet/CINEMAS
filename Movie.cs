#define DEBUG
//#undef DEBUG

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinemas
{
    class Movie
    {
        public string Name { get; private set; }
        public TimeSpan MinutesOfLength { get; private set; }

        #region CTOR for Name and MinutesOfLength
        public Movie(string Name, byte MinutesOfLength=0)
        {
            #region debug message
#if DEBUG
            Program.LogCaller();
#endif
            #endregion
            this.Name = Name;
            this.MinutesOfLength = TimeSpan.FromMinutes(MinutesOfLength);
        }
        #endregion

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
            Movie movie = obj as Movie;
            return movie
                   is object
                && String.Equals(Name, movie.Name);
        }
        #endregion
    }
}