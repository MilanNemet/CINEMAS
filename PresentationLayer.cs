#define DEBUG
//#undef DEBUG

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;

namespace Cinemas
{
    class PresentationLayer
    {
        static int index = 0;
        static int cinemaCounter = 0;
        static Cinema activeCinema;
        static Auditorium activeAuditorium;
        static string[] mostViewedProjectionData = new string[2];
        static int offset = 40;
        static int sleepTime = 1000;
        static string appName = AppDomain.CurrentDomain.FriendlyName.ToString().Replace(".exe","");
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
        static MenuItem cinemas = "Cinemas Menu";
        //static MenuItem projections = "Check Projections";
        static MenuItem mostProjection = "Most Viewed Projection";
        static MenuItem[] MainMenuItems = {newCinema, cinemas, mostProjection };
        delegate void MenuMethod();
        static Dictionary<int, MenuMethod> MainMenuMethods = new Dictionary<int, MenuMethod>();
        static Dictionary<int, MenuMethod> CinemasMenuMethods = new Dictionary<int, MenuMethod>();
        static Dictionary<int, MenuMethod> MostViewedProjectionMenuMethods = new Dictionary<int, MenuMethod>();
        static Dictionary<int, MenuMethod> InCinemaMenuMethods = new Dictionary<int, MenuMethod>();
        static Dictionary<int, MenuMethod> InAuditoriumMenuMethods = new Dictionary<int, MenuMethod>();
        static Dictionary<int, MenuMethod> InProjectionMenuMethods = new Dictionary<int, MenuMethod>();
        static Dictionary<string, Dictionary<int, MenuMethod>> DelegateDictionaries = new Dictionary<string, Dictionary<int, MenuMethod>>();

        public PresentationLayer()
        {
            #region debug message
#if DEBUG
            IO_Handler.LogItsCaller();
#endif
            #endregion
            Console.OutputEncoding = Encoding.UTF8;
            Console.CursorVisible = false;
            Console.WindowWidth = 2 * offset;
            Console.Title = appName;
            MainMenuMethods[0] = NewCinemaMenu;
            MainMenuMethods[1] = CinemasMenu;
            MainMenuMethods[2] = MostViewedProjectionMenu;
            MostViewedProjectionMenuMethods[0] = SaveMostViewed;

            DelegateDictionaries["MainMenu"] = MainMenuMethods;
            DelegateDictionaries["CinemasMenu"] = CinemasMenuMethods;
            DelegateDictionaries["MostViewedProjectionMenuMethods"] = MostViewedProjectionMenuMethods;

            DelegateDictionaries["InCinemaMenu"] = InCinemaMenuMethods;
            DelegateDictionaries["InAuditoriumMenu"] = InAuditoriumMenuMethods;
            DelegateDictionaries["InProjectionMenu"] = InProjectionMenuMethods;
        }

        public void MainMenu()
        {
            #region debug message
#if DEBUG
            IO_Handler.LogItsCaller();
#endif
            #endregion
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
            bool invalid = true;
            while (invalid)
            {
                ConsoleKey ck = Console.ReadKey(false).Key;
                switch (ck)
                {
                    case ConsoleKey.UpArrow:
                        if (--index == -1) index = MenuItems.Length - 1;
                        goto case ConsoleKey.Clear;
                    case ConsoleKey.DownArrow:
                        if (++index == MenuItems.Length) index = 0;
                        goto case ConsoleKey.Clear;
                    case ConsoleKey.Enter:
                        Console.Clear();
                        try
                        {
                            StackFrame sf = new StackFrame(2);
                            if (sf.GetMethod().Name.Equals("InCinemaMenu"))
                            {
                                try
                                {
                                    activeAuditorium = activeCinema.OwnAuditoriums[(byte)(index+1)];
                                    IO_Handler.SuccessMessage("ACTIVE AUDITORIUM GOT SET NOW!");
                                    Thread.Sleep(2500);
                                }
                                catch (Exception e)
                                {
                                    Console.WriteLine("active auditorium not set:\nLOC-170" + e.Message);
                                }
                            }
                            GetThisMenuMethods()[index]();
                        }
                        catch (Exception e)
                        {
                            IO_Handler.ErrorMessage("Error while opening this menu!\nLOC-177\n" + e.Message);
                            Thread.Sleep(sleepTime);
                        }
                        goto case ConsoleKey.Clear;
                    case ConsoleKey.Escape:
                        index = 0;
                        return false;
                    case ConsoleKey.Clear:
                        return true;
                    default:
                        invalid = true;
                        break;
                }
            }
            return true;
        }
        Dictionary<int, MenuMethod> GetThisMenuMethods()
        {
            StackFrame frame = new StackFrame(3);
            try
            {
                Dictionary<int, MenuMethod> ResultDict = DelegateDictionaries[frame.GetMethod().Name];
                return ResultDict;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        MenuItem[] ListToMenuItemArray<T>(List<T> Collection)
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
        MenuItem[] DictToMenuItemArray<K,V>(Dictionary<K,V> Collection)
        {
            #region debug message
#if DEBUG
            IO_Handler.LogItsCaller();
#endif
            #endregion
            List<V> Helper = new List<V>();
            foreach (var item in Collection)
            {
                Helper.Add(item.Value);
            }
            return ListToMenuItemArray(Helper);
        }
        void SaveMostViewed()//Western technique: container for IO_Handler.SaveToFile, due to compatibility with delegate void MenuMethod()
        {
            IO_Handler.SaveToFile(mostViewedProjectionData);
        }

        //================================================ HERE COMES THE SUB MENUS ================================================

        static MenuTitle mostViewedProjectionMenu = "Most Viewed Projection";
        static MenuItem[] mostViewedProjectionMenuItems = { "Save to file" };
        void MostViewedProjectionMenu()
        {
            try
            {
                Projection mostViewed = ObjectContainer.FindProjectionMostViewed();
                string[] mostViewedData = {
                $"{mostViewed.OwnMovie} ({mostViewed.OwnMovie.MinutesOfLength})",
                $"On schedule: \n{mostViewed.OwnerAuditorium} in {mostViewed.OwnerAuditorium.OwnerCinema}"
                };
                Console.WriteLine(mostViewedData[0]);
                Console.WriteLine(mostViewedData[1]);
                mostViewedProjectionData = mostViewedData;
                KeepDoingMenu(mostViewedProjectionMenu, mostViewedProjectionMenuItems);
            }
            catch (Exception)
            {
                Console.WriteLine("\n\n");
                Console.WriteLine("Nothing to show yet.".PadLeft(offset+10));
                Thread.Sleep(1500);
            }
        }



        static MenuTitle newCinemaMenu = "New Cinema Menu";
        static MenuItem[] newCinemaMenuItems = { }; //most elmegy de rossz! Lásd: cinemasMenuItems
        void NewCinemaMenu()
        {
            ShowMenu(newCinemaMenu, newCinemaMenuItems);
            string cinemaName = IO_Handler.EnterString("Please, enter the name of the new cinema: ");
            byte auditoriumCount = IO_Handler.EnterByte("Please, enter the number of auditoriums: ");
            new Cinema(cinemaName, auditoriumCount);
            CinemasMenuMethods[cinemaCounter++] = InCinemaMenu;
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
            MenuItem[] cinemasMenuItems = ListToMenuItemArray(ObjectContainer.CDB);
            KeepDoingMenu(cinemasMenu, cinemasMenuItems);
        }



        static MenuTitle inCinemaMenu = "In Cinema: ";
        void InCinemaMenu()
        {
            #region debug message
#if DEBUG
            IO_Handler.LogItsCaller();
            Thread.Sleep(500);
#endif
            #endregion
            MenuItem[] inCinemaMenuItems = DictToMenuItemArray(ObjectContainer.CDB[index].OwnAuditoriums);
            string currentCinema = inCinemaMenu+ObjectContainer.CDB[index].Name;
            activeCinema = ObjectContainer.CDB[index];
            for (int i = 0; i < activeCinema.OwnAuditoriums.Count; i++)
            {
                InCinemaMenuMethods[i] = InAuditoriumMenu;
            }
            KeepDoingMenu(currentCinema, inCinemaMenuItems);
        }



        static MenuTitle inAuditoriumMenu = "In ";
        void InAuditoriumMenu()
        {
            #region debug message
#if DEBUG
            IO_Handler.LogItsCaller();
            Thread.Sleep(500);
#endif
            #endregion
            MenuItem[] inAuditoriumMenuItems = { "-Add Nem Projection-", "Projections" };
            InAuditoriumMenuMethods[0] = activeAuditorium.AddNewProjection;
            InAuditoriumMenuMethods[1] = InProjectionMenu;
            string currentAuditorium = inAuditoriumMenu + activeCinema.OwnAuditoriums[(byte)(index+1)].ToString()+$"@{ activeCinema}";

            //for (int i = 1; i <= activeAuditorium.OwnProjections.Count; i++)
            //{
            //    InAuditoriumMenuMethods[i] = InProjectionMenu;
            //}
            KeepDoingMenu(currentAuditorium, inAuditoriumMenuItems);
        }



        static MenuTitle inProjectionMenu = $"Projections of ";
        void InProjectionMenu()
        {
            MenuTitle current = inProjectionMenu+$"{activeAuditorium}@{activeCinema}";
            MenuItem[] inProjectionMenuItems = DictToMenuItemArray(activeAuditorium.OwnProjections);
            KeepDoingMenu(current, inProjectionMenuItems);
        }
    }
}
