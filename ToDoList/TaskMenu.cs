using System.Text.Json.Nodes;

namespace ToDoList
{
    public class TaskMenu
    {  
        public static void CallTaskMenu(int taskId, int listId)
        {
            bool isRunning = true;

            while (isRunning)
            {
                Task.ViewIndividualTask(taskId, listId);

                Console.WriteLine("\nTASK MENU\n");

                Console.WriteLine("[1] Edit task");
                Console.WriteLine("[2] Delete task");
                Console.WriteLine("[3] Go back to List menu");
                Console.WriteLine("[4] Go back to Start menu");

                Console.Write("\nSelect an option: ");
                var input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        Task.EditTask(taskId, listId);
                        break;
                    case "2":
                        Task.DeleteTask(taskId, listId);
                        ListMenu.CallListMenu(listId);
                        return;
                    case "3":
                        Console.Clear();
                        ListMenu.CallListMenu(listId);
                        break;
                    case "4":
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

        public static void ChooseTaskMenu(int listId)
        {
            var json = FileManager.GetJson();
            var tasks = json[listId - 1].Tasks;

            int taskId;

            if (tasks.Count == 0)
            {
                Console.WriteLine("There are no tasks in the list");
                Thread.Sleep(1500);
                return;
            }

            try
            {
                Console.WriteLine("Which task do you want to view?");
                taskId = Convert.ToInt32(Console.ReadLine());

                if (taskId <= 0 || tasks.Count < taskId)
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
    }
}
