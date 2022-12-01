namespace ToDoList
{
    public class TaskMenu
    {
        public static void CallTaskMenu(int listId)
        {
            bool isRunning = true;

            while (isRunning)
            {
                Task.ViewTasks(listId);

                Console.WriteLine("\nTASK MENU\n");

                Console.WriteLine("[1] Edit task");
                Console.WriteLine("[2] Delete task");
                Console.WriteLine("[3] Archivate task");
                Console.WriteLine("[4] Go back to Start menu");

                Console.WriteLine("\nSelect an option: ");
                var input = Convert.ToInt32(Console.ReadLine());

                switch (input)
                {
                    case 1:
                        Task.EditTask(listId);
                        break;
                    case 2:
                        Task.DeleteTask(listId);
                        break;
                    case 3:
                        Console.WriteLine("Archivate task");
                        Console.ReadLine();
                        break;
                    case 4:
                        Console.WriteLine("Go back to Start menu");
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
