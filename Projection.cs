﻿#define DEBUG
//#undef DEBUG

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinemas
{
    /// <summary>
    /// Projections are events in different <see cref="Auditorium"/>s, about different <see cref="Movie"/>s.
    /// </summary>
    class Projection
    {
        public string Name
        {
            get { return $"Projection of:\"{Name}\""; }
            private set { Name = OwnMovie.Name; }
        }
        public enum Seat
        {
            Available = 0x0,
            UnAvailable = 0x1
        }
        public Auditorium OwnAuditorium { get; private set; }
        public Movie OwnMovie { get; private set; }
        public Seat[,] OwnSeats { get; private set; }

        public Projection(Auditorium Au, Movie Mov)
        {
            #region debug message
#if DEBUG
            Program.LogCaller();
#endif
            #endregion
            OwnAuditorium = Au;
            OwnMovie = Mov;
            OwnSeats = new Seat[Au.Rows,Au.Columns];
            InitSeats();
        }

        private void InitSeats()
        {
            #region debug message
#if DEBUG
            Program.LogCaller();
#endif
            #endregion
            byte rows = OwnAuditorium.Rows;
            byte cols = OwnAuditorium.Columns;
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    OwnSeats[i, j] = Seat.Available;
                }
            }
        }
        public bool ReserveSeat(byte row, byte col)
        {
            #region debug message
#if DEBUG
            Program.LogCaller();
#endif
            #endregion
            if (GetSeatAvailability(row, col) == Seat.Available)
            {
                FlipSeatAvailabilty(row, col);
                return true;
            }
            return false;
        }
        public bool FreeSeat(byte row, byte col)
        {
            #region debug message
#if DEBUG
            Program.LogCaller();
#endif
            #endregion
            if (GetSeatAvailability(row, col) == Seat.UnAvailable)
            {
                FlipSeatAvailabilty(row, col);
                return true;
            }
            return false;
        }
        private Seat GetSeatAvailability(byte row, byte col)
        {
            #region debug message
#if DEBUG
            Program.LogCaller();
#endif
            #endregion
            return OwnSeats[row, col];
        }
        private void FlipSeatAvailabilty(byte row, byte col)
        {
            #region debug message
#if DEBUG
            Program.LogCaller();
#endif
            #endregion
            OwnSeats[row, col] = OwnSeats[row, col] ^ Seat.UnAvailable;
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
            Projection projection = obj as Projection;
            return projection
                   is object
                && String.Equals(Name, projection.Name);
        }
        #endregion
    }
}