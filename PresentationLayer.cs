#define DEBUG
#undef DEBUG

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Cinemas
{
    class PresentationLayer
    {
        static int index = 0;
        static int offset = 40;
        static int sleepTime = 1000;
        struct MenuTitle
        {
            string titleName;
            public int Length;
            public static implicit operator MenuTitle(string value)
            {
                return new MenuTitle() { titleName = value.ToUpper(), Length = value.Length };
            }
            public override string ToString()
            {
                return titleName;
            }
        }
        struct MenuItem
        {
            string itemName;
            public int Length;
            public static implicit operator MenuItem(string value)
            {
                return new MenuItem() { itemName = value, Length = value.Length };
            }
            public override string ToString()
            {
                return itemName;
            }
        }

        static MenuTitle mainMenu = "MAIN MENU";
        static MenuItem newCinema = "Create New Cinema";
        static MenuItem cinemas = "Check Cinemas";
        static MenuItem projections = "Check Projections";
        static MenuItem mostProjection = "Most Viewed Projection";
        static MenuItem[] MainMenuItems = {newCinema, cinemas, projections, mostProjection };
        delegate void MenuMethod();
        static Dictionary<int, MenuMethod> MenuMethods = new Dictionary<int, MenuMethod>();
        
        public PresentationLayer()
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.CursorVisible = false;
            Console.WindowWidth = 2 * offset;
            MenuMethods[0] = NewCinemaMenu;
            MenuMethods[1] = CinemasMenu;
        }

        public void MainMenu()
        {
            KeepDoingMenu(mainMenu, MainMenuItems);
        }

        void KeepDoingMenu(MenuTitle menuTitle, MenuItem[] MenuItems)
        {
            index = 0;
            bool run = true;
            while (run)
            {
                ShowMenu(menuTitle, MenuItems);
                run = ControlMenu(MenuItems);
                Console.Clear();
            }
        }
        void ShowMenu(MenuTitle menuTitle, MenuItem[] MenuItems)
        {
            #region debug message
#if DEBUG
            IO_Handler.LogItsCaller();
#endif
            #endregion
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(menuTitle.ToString().PadLeft(offset + menuTitle.Length / 2));
            Console.WriteLine();
            Console.ResetColor();
            for (int i = 0; i < MenuItems.Length; i++)
            {
                if (i == index)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine(MenuItems[i].ToString().PadLeft(offset+ MenuItems[i].Length / 2));
                    Console.ResetColor();
                }
                else
                {
                    Console.WriteLine(MenuItems[i].ToString().PadLeft(offset + MenuItems[i].Length / 2));
                }
            }
        }
        bool ControlMenu(MenuItem[] MenuItems)
        {
            #region debug message
#if DEBUG
            IO_Handler.LogItsCaller();
#endif
            #endregion
            ConsoleKey ck = Console.ReadKey(false).Key;
            switch (ck)
            {
                case ConsoleKey.UpArrow:
                    if (--index == -1) index = MenuItems.Length - 1;
                    goto default;
                case ConsoleKey.DownArrow:
                    if (++index == MenuItems.Length) index = 0;
                    goto default;
                case ConsoleKey.Enter:
                    Console.Clear();
                    try
                    {
                        MenuMethods[index]();
                    }
                    catch (Exception e)
                    {
                        IO_Handler.ErrorMessage("Error while opening this menu!\n\n"+e.Message);
                        Thread.Sleep(sleepTime);
                    }
                    goto default;
                case ConsoleKey.Escape:
                    return false;
                default:
                    return true;
            }
        }
        MenuItem[] ToMenuItemArray<T>(List<T> Collection)
        {
            #region debug message
#if DEBUG
            IO_Handler.LogItsCaller();
#endif
            #endregion
            MenuItem[] MenuItems = new MenuItem[Collection.Count];
            for (int i = 0; i < Collection.Count; i++)
            {
                MenuItems[i] = Collection[i].ToString();
            }
            return MenuItems;
        }



        static MenuTitle newCinemaMenu = "New Cinema Menu";
        static MenuItem[] newCinemaMenuItems = { }; //most elmegy de rossz! Lásd: cinemasMenuItems
        void NewCinemaMenu()
        {
            ShowMenu(newCinemaMenu, newCinemaMenuItems);
            string cinemaName = IO_Handler.EnterString("Please, enter the name of the new cinema: ");
            byte auditoriumCount = IO_Handler.EnterByte("Please, enter the number of auditoriums: ");
            new Cinema(cinemaName, auditoriumCount);
            IO_Handler.SuccessMessage($"New Cinema: \"{cinemaName}\" has been created with {auditoriumCount} Auditorium{(auditoriumCount>1?"s":"")} in it.");
            Thread.Sleep(sleepTime);
        }



        static MenuTitle cinemasMenu = "Cinemas";
        void CinemasMenu()
        {
            #region debug message
#if DEBUG
            IO_Handler.LogItsCaller();
#endif
            #endregion
            MenuItem[] cinemasMenuItems = ToMenuItemArray(ObjectContainer.CDB);
            KeepDoingMenu(cinemasMenu, cinemasMenuItems);
        }
    }
}
