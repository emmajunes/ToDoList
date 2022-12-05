
using System.Collections.Generic;
using System.IO.Pipes;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
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

            Console.Clear();
            Console.WriteLine("New created list: " + title);

        }

        public static void ViewAllLists()
        {
            //Console.Clear();
            var json = FileManager.GetJson();

            Console.WriteLine("\nOVERVIEW OF LISTS: \n");

            int index = 1;

            foreach (var lists in json)
            {
                Console.WriteLine("[" + index + "]" + " " + lists.ListTitle);
                index++;
            }

            //ManageList();
        }

        public static void ManageList()
        {
            var json = FileManager.GetJson();
           
            int listIndex;

            if (json.Count == 0)
            {
                Console.Clear();
                Console.WriteLine("No lists available");
                return;
            }

            try
            {
                Console.Write("\nChoose a list to manage that list: ");
                listIndex = Convert.ToInt32(Console.ReadLine());

                ListMenu.CallListMenu(listIndex);
            }
            catch (ArgumentOutOfRangeException)
            {
                Console.Clear();
                ViewAllLists();
                Console.WriteLine("\nId does not exist. Try again!");
                ManageList();

            }
            catch (FormatException)
            {
                Console.Clear();
                ViewAllLists();
                Console.WriteLine("\nId must be a number. Try again!");
                ManageList();

            }

        }

        public static void OpenRecent()
        {
            var json = FileManager.GetJson();

            var lastCreated = json.Count;

            if (json.Count == 0)
            {
                Console.WriteLine("There are no lists available");
                Thread.Sleep(2000);
                StartMenu.CallStartMenu();
            }

            Task.ViewTasks(lastCreated);

            ListMenu.CallListMenu(lastCreated);

        }


        public static void EditList(int listId)
        {
            var json = FileManager.GetJson();
            var currentList = json[listId - 1];

            Console.WriteLine("Write a new name to the list: ");
            var title = Console.ReadLine();

            if (String.IsNullOrEmpty(title))
            {
                Console.WriteLine("List title cannot be empty");

                EditList(listId);
                return;
            }

            currentList.ListTitle = title;

            //Console.WriteLine(json[listId - 1].ListTitle);

            FileManager.UpdateJson(json);
        }

        public static void DeleteList()
        {
            var json = FileManager.GetJson();
            int deleteList;
            ViewAllLists();

            Console.Write("\nChoose a list to delete: ");
            var deleteIndex = Console.ReadLine();

            if (String.IsNullOrEmpty(deleteIndex))
            {
                Console.Clear();
                Console.WriteLine("Id does not exist. Try again!");
                DeleteList();
                return;
            }

            try
            {
                if (Convert.ToInt32(deleteIndex) == 0 || json.Count < Convert.ToInt32(deleteIndex))
                {
                    Console.Clear();
                    Console.WriteLine("Id does not exist. Try again!");
                    DeleteList();
                    return;
                }
                deleteList = Convert.ToInt32(deleteIndex) - 1;

                Console.WriteLine("Do you want to delete this list y/n?");
                var deleteAnswer = Console.ReadLine().ToUpper();

                if (String.IsNullOrWhiteSpace(deleteAnswer))
                {
                    Console.WriteLine("Field cannot be empty");
                    DeleteList();
                    return;
                }

                if (Convert.ToChar(deleteAnswer) == 'Y')
                {
                    Console.Clear();
                    Console.WriteLine("Deleted list: " + json[deleteList].ListTitle);
                    json.RemoveAt(deleteList);
                    FileManager.UpdateJson(json);
                    return;

                }

                if (Convert.ToChar(deleteAnswer) == 'N')
                {
                    return;
                }
                else
                {
                Console.WriteLine("Answer needs to be a letter of y or n");
                    DeleteList();
                    return;
                }
            }

            catch (ArgumentOutOfRangeException)
            {
                Console.Clear();
                Console.WriteLine("Id does not exist. Try again!");
                DeleteList();
                return;
            }
            catch (FormatException)
            {
                Console.Clear();
                Console.WriteLine("Id must be a number. Try again!");
                DeleteList();
                return;
            }

        }

        public static void DeleteAllLists()
        {
            var json = FileManager.GetJson();

            Console.WriteLine("Do you want to delete all lists y/n? : ");
            var deleteAnswer = Console.ReadLine().ToUpper();

            if (String.IsNullOrEmpty(deleteAnswer))
            {
                Console.WriteLine("Input field cannot be empty");
                DeleteAllLists();
                return;
            } 

            try
            {
                if (Convert.ToChar(deleteAnswer) == 'Y')
                {
                    Console.Clear();
                    Console.WriteLine("All lists deleted");
                    json.Clear();
                    FileManager.UpdateJson(json);
                    return;
                }
                if (Convert.ToChar(deleteAnswer) == 'N')
                {
                    return;
                }
                else
                {
                    Console.WriteLine("Answer needs to be a letter of y or n");
                    DeleteAllLists();
                    return;
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("Id must be a number. Try again!");
                DeleteAllLists();
                return;
                
            }

               
        }

    }
}
