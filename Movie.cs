#define DEBUG
#undef DEBUG

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinemas
{
    class Movie : CommonAttributes
    {
        public TimeSpan MinutesOfLength { get; private set; }

        #region CTOR for Name and MinutesOfLength
        public Movie(string Name, byte MinutesOfLength=0) : base(Name)
        {
            #region debug message
#if DEBUG
            Program.LogThisCaller();
#endif
            #endregion
            this.MinutesOfLength = TimeSpan.FromMinutes(MinutesOfLength);
        }
        #endregion

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
            return obj is Movie movie
                && String.Equals(Name, movie.Name);
        }
        #endregion
    }
}