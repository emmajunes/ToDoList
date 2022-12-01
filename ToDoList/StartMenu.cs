using System.Collections.Generic;

namespace ToDoList
{
    public class StartMenu
    {
        public static void CallStartMenu()
        {
            Console.Clear();

            bool isRunning = true;

            while (isRunning)
            {
                Console.WriteLine("\nSTART MENU\n");
                
                Console.WriteLine("[1] Create a new list");
                Console.WriteLine("[2] View all lists");
                Console.WriteLine("[3] Open recent list");
                Console.WriteLine("[4] Delete list"); //skapa delete all function?
                Console.WriteLine("[5] Quit");

                Console.WriteLine("\nSelect an option: ");
                var input = Convert.ToInt32(Console.ReadLine());

                switch (input)
                {
                    case 1:
                        List.CreateList();
                        break;
                    case 2:
                        Console.WriteLine("\nOVERVIEW OF LISTS: \n");
                        List.ViewAllLists();
                        List.ManageList();
                        break;
                    case 3:
                        List.OpenRecent();
                        break;
                    case 4:
                        List.DeleteList();
                        break;
                    case 5:
                        Console.WriteLine("Do you want to quit y/n? ");
                        var exit = Convert.ToChar(Console.ReadLine());
                        if (exit == 'y')
                        {
                            isRunning = false;
                        }
                        break;
                    default:
                        Console.WriteLine("There are no option recognized to your input. Plese try again");
                        Console.ReadLine();
                        break;
                }


            }

        }
    }
}
