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

                Console.WriteLine("[1] Edit name of list");
                Console.WriteLine("[2] Add task");
                Console.WriteLine("[3] View task");
                Console.WriteLine("[4] Toggle task");
                Console.WriteLine("[5] Delete task");
                Console.WriteLine("[6] Go back to Start menu");

                Console.Write("\nSelect an option: ");
                var input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        List.EditList(listId);
                        break;
                    case "2":
                        Task.AddTask(listId);
                        break;
                    case "3":
                        TaskMenu.ChooseTaskMenu(listId);
                        break;
                    case "4":
                        Task.ToggleTask(listId);
                        break;
                    case "5":
                        Task.DeleteTask(listId);
                        break;
                    case "6":
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
