namespace ToDoList
{
    public class ListMenu
    {
        public static void CallListMenu(int listId)
        {
            bool isRunning = true;

            while (isRunning)
            {
                Task.ViewTasks(listId);

                Console.WriteLine("\nLIST MENU\n");

                Console.WriteLine("[1] Edit title of list");
                Console.WriteLine("[2] Color title of list");
                Console.WriteLine("[3] Add task");
                Console.WriteLine("[4] View task");
                Console.WriteLine("[5] Toggle task");
                Console.WriteLine("[6] Delete task");
                Console.WriteLine("[7] Go back to Start menu");

                Console.Write("\nSelect an option: ");
                var input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        List.EditList(listId);
                        break;
                    case "2":
                        List.ColorList(listId);
                        break;
                    case "3":
                        Task.AddTask(listId);
                        break;
                    case "4":
                        TaskMenu.ChooseTaskMenu(listId);
                        break;
                    case "5":
                        Task.ToggleTask(listId);
                        break;
                    case "6":
                        Task.DeleteTask(listId);
                        break;
                    case "7":
                        Console.Clear();
                        StartMenu.CallStartMenu();
                        break;
                    default:
                        Console.WriteLine("There are no option recognized to your input. Try again!");
                        Thread.Sleep(1500);
                        break;

                }
            }

        }
    }
}
