
using System.Collections.Generic;
using System.Xml.Linq;

namespace ToDoList
{
    public class List
    {
        public string ListTitle { get; set; }
        public List<Task> Tasks { get; set; }

        public static void CreateList()
        {
            var json = FileManager.GetJson();

            Console.Write("Enter a name of the new list: ");
            var title = (Console.ReadLine());

            if (String.IsNullOrEmpty(title))
            {
                Console.WriteLine("List title cannot be empty");

                CreateList();
                return;
            }

            var newList = new List()
            {
                ListTitle = title,
                Tasks = new List<Task>()
            };

            json.Add(newList);

            FileManager.UpdateJson(json);
        }

        public static void ViewAllLists()
        {
            Console.Clear();

            var json = FileManager.GetJson();

            int index = 1;

            foreach (var lists in json)
            {
                Console.WriteLine("[" + index + "]" + " " + lists.ListTitle);
                index++;
            }

        }

        public static void ManageList()
        {

            Console.WriteLine("Choose a list to manage that list: ");
            var listIndex = Convert.ToInt32(Console.ReadLine());

            ListMenu.CallListMenu(listIndex);
        }

        public static void OpenRecent()
        {
            var json = FileManager.GetJson();

            var lastCreated = json.Count;

            Task.ViewTasks(lastCreated);
            
            ListMenu.CallListMenu(lastCreated);
        }


        public static void EditList(int listId)
        {
            var json = FileManager.GetJson();
            Console.WriteLine("Write a new name to the list: ");
            var title = Console.ReadLine();

            if (String.IsNullOrEmpty(title))
            {
                Console.WriteLine("List title cannot be empty");

                EditList(listId);
                return;
            }

            json[listId - 1].ListTitle = title;

            //Console.WriteLine(json[listId - 1].ListTitle);
          
            FileManager.UpdateJson(json);
        }

        public static void DeleteList()
        {
            var json = FileManager.GetJson();

            ManageList();

            Console.WriteLine("Choose a list to delete");
            var deleteIndex = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Do you want to delete this list y/n? : ");
            var deleteAnswer = Convert.ToChar(Console.ReadLine());

            if (deleteAnswer == 'y')
            {
                json.RemoveAt(deleteIndex - 1);
            }

            FileManager.UpdateJson(json);

        }


    }
}
