using System.Collections.Generic;
using System.Net.NetworkInformation;

namespace ToDoList
{
    public class StartMenu
    {
        public static void CallStartMenu()
        {
            //Console.Clear();

            bool isRunning = true;

            while (isRunning)
            {
                //Console.Clear(); vart ska jag ha denna egentligen?

                Console.WriteLine("\nSTART MENU\n");
                
                Console.WriteLine("[1] Create a new list");
                Console.WriteLine("[2] View all lists");
                Console.WriteLine("[3] Open recent list");
                Console.WriteLine("[4] Delete list"); //skapa delete all function?
                Console.WriteLine("[5] Delete all lists");
                Console.WriteLine("[6] Quit");

                Console.Write("\nSelect an option: ");
                var input = Console.ReadLine();

                if (String.IsNullOrEmpty(input))
                {
                    Console.Clear();
                    Console.WriteLine("Input cannot be empty");
                    CallStartMenu();
                }

                switch (input)
                {
                    case "1":
                        List.CreateList();
                        break;
                    case "2":
                        List.ViewAllLists();
                        List.ManageList();
                        break;
                    case "3":
                        List.OpenRecent();
                        break;
                    case "4":
                        List.DeleteList();
                        break;
                    case "5":
                        List.DeleteAllLists();
                        break;
                    case "6":
                        isRunning = StartMenu.CallStopMenu(isRunning);
                        break;
                    default:
                        Console.Clear();
                        Console.WriteLine("There are no option recognized to your input. Try again!");
                        break;
                }

            }

        }

        public static bool CallStopMenu(bool isRunning)
        {
            char exit;
            try
            {
                Console.WriteLine("Do you want to quit y/n? ");
                exit = Convert.ToChar(Console.ReadLine().ToUpper());

                if (exit == 'Y')
                {
                    isRunning = false;
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("Answer needs to be a character");
                
            }

            return isRunning;
        }

    }
}
