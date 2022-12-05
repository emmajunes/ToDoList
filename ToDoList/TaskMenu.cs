using System.Text.Json.Nodes;

namespace ToDoList
{
    public class TaskMenu
    {
        public static void ChooseTaskMenu(int listId)
        {
            var json = FileManager.GetJson();

            int taskId;

            var tasks = json[listId - 1].Tasks;

            if(tasks.Count == 0)
            {
                Console.WriteLine("There are no tasks in the list");
                Thread.Sleep(1500);
                return;
            }

            try
            {
                Console.WriteLine("Which task do you want to view?");
                taskId = Convert.ToInt32(Console.ReadLine());

                if (Convert.ToInt32(taskId) == 0 || tasks.Count < Convert.ToInt32(taskId))
                {
                    Console.Clear();
                    Task.ViewTasks(listId);
                    Console.WriteLine("Id does not exist. Try again!");
                    ChooseTaskMenu(listId);
                    return;
                }
            }
            catch (FormatException)
            {
                Console.Clear();
                Task.ViewTasks(listId);
                Console.WriteLine("Id must be a number. Try again!");
                ChooseTaskMenu(listId);
                return;
            }
            
            CallTaskMenu(taskId, listId);

        }
        public static void CallTaskMenu(int taskId, int listId)
        {
            
            bool isRunning = true;

            while (isRunning)
            {
                Task.ViewIndividualTask(taskId, listId);

                Console.WriteLine("\nTASK MENU\n");

                Console.WriteLine("[1] Edit task");
                Console.WriteLine("[2] Delete task");
                Console.WriteLine("[3] Archivate task");
                Console.WriteLine("[4] Go back to Start menu");

                Console.Write("\nSelect an option: ");
                var input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        Task.EditTask(taskId, listId);
                        break;
                    case "2":
                        Task.DeleteTask(listId,taskId);
                        ListMenu.CallListMenu(listId);
                        return;
                    case "3":
                        Console.WriteLine("Archivate task");
                        Console.ReadLine();
                        break;
                    case "4":
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
