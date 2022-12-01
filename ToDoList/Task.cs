using System;
using System.Collections.Generic;
using System.Reflection;
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

            Console.WriteLine("What task do you want to add?: ");
            var task = Console.ReadLine();

            if (String.IsNullOrEmpty(task))
            {
                Console.WriteLine("You cannot add an empty task");
            }

            Console.WriteLine("What description do you want to add to the task?: ");
            var description = Console.ReadLine();

            var newTask = new Task()
            {
                TaskTitle = task,
                TaskDescription = description

            };

            json[listId - 1].Tasks.Add(newTask);


            FileManager.UpdateJson(json);
        }
       
        public static void ViewTasks(int listId)
        {
            Console.Clear();

            var json = FileManager.GetJson();

            Console.WriteLine("\n" + json[listId -1].ListTitle.ToUpper() + ":\n");
            var index = 1;

            foreach (var task in json[listId - 1].Tasks)
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

        //Måste göras om lite då det blir fel när man väljer nej, det blir då blank rad
        public static void EditTask(int listId) 
        {
            var json = FileManager.GetJson();

            Console.WriteLine("Select the number of the task that you want to edit: ");
            var taskId = Convert.ToInt32(Console.ReadLine());

            //Lägg till validering för taskId

            var currentList = json[listId - 1];
            var currentTask = currentList.Tasks[taskId - 1];

            Console.WriteLine("Do you want to write a new title of the task y/n? ");
            var inputTitle = Convert.ToChar(Console.ReadLine());

            if(inputTitle == 'y' || inputTitle == 'Y')
            {
                Console.WriteLine("Write a new title: ");
                var newTitle = Console.ReadLine();

                if (String.IsNullOrEmpty(newTitle))
                {
                    Console.WriteLine("List title cannot be empty");
                    return;
                }

                currentTask.TaskTitle = newTitle;
            }
            
            Console.WriteLine("Do you want to write a new description of the task y/n? ");
            var inputDescription = Convert.ToChar(Console.ReadLine());

            if (inputDescription == 'y' || inputDescription == 'Y')
            {
                Console.WriteLine("Write a new description: ");
                var newDescription = Console.ReadLine();

                if (String.IsNullOrEmpty(newDescription))
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
            var taskId = Convert.ToInt32(Console.ReadLine());

            var currentList = json[listId - 1];
            var currentTask = currentList.Tasks[taskId - 1];

            currentTask.Completed = !currentTask.Completed;

            FileManager.UpdateJson(json);
        }

        public static void DeleteTask(int listId)
        {
            var json = FileManager.GetJson();

            Console.WriteLine("Choose a task to delete");
            var deleteIndex = Convert.ToInt32(Console.ReadLine());

            var deleteTaskIndex = deleteIndex - 1;
            var currentList = json[listId - 1];

            Console.WriteLine("Do you want to delete this task y/n? : ");
            var deleteAnswer = Convert.ToChar(Console.ReadLine());

            if (deleteAnswer == 'y')
            {
                currentList.Tasks.RemoveAt(deleteTaskIndex);
            }

            FileManager.UpdateJson(json);
        }
    }
}
