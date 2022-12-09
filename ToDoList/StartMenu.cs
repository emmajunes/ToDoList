using System.Collections.Generic;
using System.Net.NetworkInformation;

namespace ToDoList
{
    public class StartMenu
    {
        public static void CallStartMenu()
        {
            bool isRunning = true;

            while (isRunning)
            {
                Console.WriteLine("\nSTART MENU\n");

                Console.WriteLine("[1] Create a new list");
                Console.WriteLine("[2] View all lists");
                Console.WriteLine("[3] Open recent list");
                Console.WriteLine("[4] Sort lists");
                Console.WriteLine("[5] Delete list");
                Console.WriteLine("[6] Delete all lists");
                Console.WriteLine("[7] Quit");

                Console.Write("\nSelect an option: ");
                var input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        List.CreateList();
                        break;
                    case "2":
                        Console.Clear();
                        List.ViewAllLists();
                        List.ChooseList();
                        break;
                    case "3":
                        List.OpenRecent();
                        break;
                    case "4":
                        List.SortList();
                        break;
                    case "5":
                        List.DeleteList();
                        break;
                    case "6":
                        List.DeleteAllLists();
                        break;
                    case "7":
                        isRunning = CallStopMenu(isRunning);
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

            Console.Write("\nDo you want to quit y/n? ");
            var exit = Console.ReadLine().ToUpper();

            if (String.IsNullOrWhiteSpace(exit))
            {
                Console.WriteLine("Input cannot be empty");
                CallStopMenu(isRunning);
                return isRunning;
            }

            if (exit == "Y")
            {
                isRunning = false;
                return isRunning;
            }

            Console.Clear();

            return isRunning;
        }

    }
}
