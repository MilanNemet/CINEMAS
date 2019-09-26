#define DEBUG
#undef DEBUG

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
        public string Name { get; private set; }
        public enum Seat
        {
            Available = 0x0,
            UnAvailable = 0x1
        }
        public Auditorium OwnerAuditorium { get; private set; }
        public Movie OwnMovie { get; private set; }
        public Seat[,] Seats { get; private set; }
        public short ReservedSeatsCount { get; private set; } = 0;

        public Projection(Auditorium OwnerAuditorium, Movie OwnMovie)
        {
            #region debug message
#if DEBUG
            Program.LogThisCaller();
#endif
            #endregion
            this.OwnerAuditorium = OwnerAuditorium;
            this.OwnMovie = OwnMovie;
            Name = OwnMovie.Name;
            Seats = new Seat[OwnerAuditorium.Rows,OwnerAuditorium.Columns];
            InitSeats();
            if (!ObjectContainer.PDB.Contains(this))
            {
                ObjectContainer.PDB.Add(this);

            }
        }

        #region Seating Methods
        private void InitSeats()
        {
            #region debug message
#if DEBUG
            Program.LogThisCaller();
#endif
            #endregion
            byte rows = OwnerAuditorium.Rows;
            byte cols = OwnerAuditorium.Columns;
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    Seats[i, j] = Seat.Available;
                }
            }
        }
        public void PrintOwnSeatsByAvailability()
        {
            #region debug message
#if DEBUG
            Program.LogThisCaller();
#endif
            #endregion
            char Av = '■';
            char UnAv = '■';
            //□■ ○● ☺☻
            byte rows = OwnerAuditorium.Rows;
            byte cols = OwnerAuditorium.Columns;
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    switch (Seats[i,j])
                    {
                        case Seat.Available:
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.Write($"{Av} ");
                            goto ResetColor;
                        case Seat.UnAvailable:
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.Write($"{UnAv} ");
                            goto ResetColor;
                        default:
                            break;
                        ResetColor:
                            Console.ResetColor();
                            break;
                    }
                }
                Console.WriteLine();
            }
            Console.ReadKey(false);
        }
        public void ReserveSeat()
        {
            #region debug message
#if DEBUG
            Program.LogThisCaller();
#endif
            #endregion
            byte row = (byte)(CheckAndReturnSeat(true) - 1);
            byte col = (byte)(CheckAndReturnSeat(false) - 1);
            if (GetSeatAvailability(row, col) == Seat.Available)
            {
                FlipSeatAvailabilty(row, col);
                ReservedSeatsCount++;
            }
            else
            {
                IO_Handler.ErrorMessage("Already taken!");
            }
        }
        public void FreeSeat()
        {
            #region debug message
#if DEBUG
            Program.LogThisCaller();
#endif
            #endregion
            byte row = (byte)(CheckAndReturnSeat(true)-1);
            byte col = (byte)(CheckAndReturnSeat(false)-1);
            if (GetSeatAvailability(row, col) == Seat.UnAvailable)
            {
                FlipSeatAvailabilty(row, col);
                ReservedSeatsCount--;
            }
            else
            {
                IO_Handler.ErrorMessage("Still free!");
            }
        }
        private Seat GetSeatAvailability(byte row, byte col)
        {
            #region debug message
#if DEBUG
            Program.LogThisCaller();
#endif
            #endregion
            return Seats[row, col];
        }
        private void FlipSeatAvailabilty(byte row, byte col)
        {
            #region debug message
#if DEBUG
            Program.LogThisCaller();
#endif
            #endregion
            Seats[row, col] = Seats[row, col] ^ Seat.UnAvailable;
        }
        private byte CheckAndReturnSeat(bool row)//this is sooo BAD!!!
        {
            byte input;
            if (row)
            {
                do
                {
                    input = IO_Handler.EnterByte("Please, enter the number of rows: "); 
                } while (input < 0 || input > OwnerAuditorium.Rows);
            }
            else
            {
                do
                {
                    input = IO_Handler.EnterByte("Please, enter the number of columns: "); 
                } while (input < 0 || input > OwnerAuditorium.Columns);
            }
            return input;
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
            return $"Projection of:\"{Name}\"";
        }
        public override bool Equals(object obj)
        {
            #region debug message
#if DEBUG
            Program.LogThisCaller();
#endif
            #endregion
            return obj is Projection projection
                && String.Equals(this.Name, projection.Name)
                && this.OwnerAuditorium.Equals(projection.OwnerAuditorium);
        }
        #endregion
    }
}
