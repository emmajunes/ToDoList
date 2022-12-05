using System;
using System.Collections.Generic;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace ToDoList
{
    public class Task
    {
        public string TaskTitle { get; set; }
        public string TaskDescription { get; set; }
        public bool Completed { get; set; }


        public static void AddTask(int listId)
        {
            var json = FileManager.GetJson();
            var currentList = json[listId - 1];

            Console.WriteLine("What task do you want to add?: ");
            var task = Console.ReadLine();

            Console.WriteLine("What description do you want to add to the task?: ");
            var description = Console.ReadLine();

            if (String.IsNullOrEmpty(task) || String.IsNullOrEmpty(description))
            {
                Console.WriteLine("You cannot add an empty field");

                AddTask(listId);
                return;
            }

            var newTask = new Task()
            {
                TaskTitle = task,
                TaskDescription = description

            };

            currentList.Tasks.Add(newTask);

            FileManager.UpdateJson(json);
        }

        public static void ViewTasks(int listId)
        {
            Console.Clear();

            var json = FileManager.GetJson();
            var currentList = json[listId - 1];

            Console.WriteLine("\n" + currentList.ListTitle.ToUpper() + ":\n");

            var index = 1;

            foreach (var task in currentList.Tasks)
            {
                if (task.Completed)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                }

                Console.WriteLine("[" + index + "]\n" + "Title: " + task.TaskTitle);
                Console.WriteLine("Descripton: " + task.TaskDescription + "\n");
                index++;

                Console.ForegroundColor = ConsoleColor.White;
            }

        }

        public static void ViewIndividualTask(int taskId, int listId)
        {
            Console.Clear();

            var json = FileManager.GetJson();
            var currentList = json[listId - 1];
            var currentTask = currentList.Tasks[taskId - 1];
            Task individualTask;

            individualTask = currentTask;


            if (individualTask.Completed)
            {
                Console.ForegroundColor = ConsoleColor.Green;
            }

            Console.WriteLine("[" + taskId + "]\n" + "Title: " + individualTask.TaskTitle);
            Console.WriteLine("Descripton: " + individualTask.TaskDescription);

            Console.ForegroundColor = ConsoleColor.White;

        }

        public static void EditTask(int taskId, int listId)
        {
            var json = FileManager.GetJson();

            var currentList = json[listId - 1];
            var currentTask = currentList.Tasks[taskId - 1];

            Console.WriteLine("Do you want to write a new title of the task y/n? ");
            var inputTitle = Console.ReadLine().ToUpper();

            if (String.IsNullOrWhiteSpace(inputTitle))
            {
                Console.WriteLine("Input cannot be empty");
                EditTask(taskId, listId);
                return;
            }

            if (inputTitle == "Y")
            {
                Console.WriteLine("Write a new title: ");
                var newTitle = Console.ReadLine();

                if (String.IsNullOrWhiteSpace(newTitle))
                {
                    Console.WriteLine("List title cannot be empty");
                    return;
                }

                currentTask.TaskTitle = newTitle;
            }

            Console.WriteLine("Do you want to write a new description of the task y/n? ");
            var inputDescription = Console.ReadLine().ToUpper();

            if (String.IsNullOrWhiteSpace(inputDescription))
            {
                Console.WriteLine("Input cannot be empty");
                EditTask(taskId, listId);
                return;
            }

            if (inputDescription == "Y")
            {
                Console.WriteLine("Write a new description: ");
                var newDescription = Console.ReadLine();

                if (String.IsNullOrWhiteSpace(newDescription))
                {
                    Console.WriteLine("List description cannot be empty");
                    return;
                }

                currentTask.TaskDescription = newDescription;
            }

            FileManager.UpdateJson(json);
        }


        public static void ToggleTask(int listId)
        {
            var json = FileManager.GetJson();

            Console.WriteLine("Select the number of the task that you want to toggle: ");
            var taskId = Console.ReadLine();

            if (String.IsNullOrWhiteSpace(taskId))
            {
                Console.WriteLine("Input cannot be empty");
                ToggleTask(listId);
                return;
            }

            try
            {
                var currentList = json[listId - 1];
                var currentTask = currentList.Tasks[Convert.ToInt32(taskId) - 1];

                currentTask.Completed = !currentTask.Completed;
            }
            catch (ArgumentOutOfRangeException)
            {
                Console.WriteLine("Id does not exist. Try again!");
                ToggleTask(listId);
                return;

            }
            catch (FormatException)
            {
                Console.WriteLine("Id must be a number. Try again!");
                ToggleTask(listId);
                return;

            }

            FileManager.UpdateJson(json);
        }

        public static void DeleteTask(int listId)
        {
            var json = FileManager.GetJson();

            int deleteIndex;

            try
            {
                Console.WriteLine("Choose a task to delete");
                deleteIndex = Convert.ToInt32(Console.ReadLine());
            }
            catch (FormatException)
            {
                Console.WriteLine("Id must be a number");
                DeleteTask(listId);
                return;
            }

            var currentList = json[listId - 1];
            var tasks = json[listId - 1].Tasks;
            var deleteTaskIndex = deleteIndex - 1;

            if (deleteIndex == 0 || deleteTaskIndex >= tasks.Count)
            {
                Console.WriteLine("Id does not exist. Try again");
                DeleteTask(listId);
                return;
            }

            Console.WriteLine("Do you want to delete this task y/n? : ");
            var deleteAnswer = Console.ReadLine().ToUpper();

            if (String.IsNullOrWhiteSpace(deleteAnswer))
            {
                Console.WriteLine("Input field cannot be empty!");
                DeleteTask(listId);
                return;
            }

            if (deleteAnswer == "Y")
            {
                currentList.Tasks.RemoveAt(deleteTaskIndex);
                FileManager.UpdateJson(json);
            }

        }

        public static void DeleteTask(int listId, int taskId)
        {
            var json = FileManager.GetJson();

            var currentList = json[listId - 1];
            var tasks = json[listId - 1].Tasks;
            var deleteTaskIndex = taskId - 1;

            if (taskId == 0 || deleteTaskIndex >= tasks.Count)
            {
                Console.WriteLine("Id does not exist. Try again");
                DeleteTask(listId, taskId);
                return;
            }

            Console.WriteLine("Do you want to delete this task y/n? : ");
            var deleteAnswer = Console.ReadLine().ToUpper();

            if (String.IsNullOrWhiteSpace(deleteAnswer))
            {
                Console.WriteLine("Input field cannot be empty!");
                DeleteTask(listId, taskId);
                return;
            }

            if (deleteAnswer == "Y")
            {
                currentList.Tasks.RemoveAt(deleteTaskIndex);
                FileManager.UpdateJson(json);
            }

            
        }
      
    }
}
