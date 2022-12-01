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

                Console.WriteLine("[1] Add task");
                Console.WriteLine("[2] Edit name of list");
                Console.WriteLine("[3] Edit task");
                Console.WriteLine("[4] View task");
                Console.WriteLine("[5] Toggle task");
                Console.WriteLine("[6] Delete task");
                Console.WriteLine("[7] Go back to Start menu");

                Console.WriteLine("\nSelect an option: ");
                var input = Convert.ToInt32(Console.ReadLine());

                switch (input)
                {
                    case 1:
                        Task.AddTask(listId);
                        break;
                    case 2:
                        List.EditList(listId);
                        break;
                    case 3:
                        Task.EditTask(listId);
                        break;
                    case 4:
                        //Task.ViewTasks(listId);
                        TaskMenu.CallTaskMenu(listId);
                        break;
                    case 5:
                        Task.ToggleTask(listId);
                        break;
                    case 6:
                        Task.DeleteTask(listId);
                        break;
                    case 7: Console.WriteLine("Go back to Start menu");
                        StartMenu.CallStartMenu();
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
