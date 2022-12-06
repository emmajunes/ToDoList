
using System.Collections.Generic;
using System.IO.Pipes;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ToDoList
{
    public class List
    {
        public string ListTitle { get; set; }
        public string TitleColor { get; set; }
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

            int listIndex = json.Count;
            ColorList(listIndex);
        }

        public static void ViewAllLists()
        {
            var json = FileManager.GetJson();
            Console.WriteLine("\nOVERVIEW OF LISTS: \n");
            int index = 1;
            Dictionary<string, int> colors = new()
            {
                { "Magenta", 13 },
                { "Yellow", 14 },
                { "Blue", 9 },
                { "Red", 12 },
                { "Cyan", 11 },
                { "White", 15 }
            };

            foreach (var list in json)
            {
                Console.ForegroundColor = (ConsoleColor)colors[list.TitleColor];

                Console.WriteLine("[" + index + "]" + " " + list.ListTitle);
                index++;
                Console.ForegroundColor = ConsoleColor.White;

            }

        }

        public static void ChooseList()
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
                ChooseList();
            }
            catch (FormatException)
            {
                Console.Clear();
                ViewAllLists();
                Console.WriteLine("\nId must be a number. Try again!");
                ChooseList();
            }

        }

        public static void OpenRecent()
        {
            var json = FileManager.GetJson();

            var lastCreated = json.Count;

            if (json.Count == 0)
            {
                Console.WriteLine("There are no lists available");
                Thread.Sleep(1500);
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

            FileManager.UpdateJson(json);
        }

        public static void DeleteList()
        {
            var json = FileManager.GetJson();
            int deleteList;
            ViewAllLists();

            try
            {
                Console.Write("\nChoose a list to delete: ");
                var deleteIndex = Convert.ToInt32(Console.ReadLine());

                if (deleteIndex == 0 || json.Count < deleteIndex)
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

            Console.WriteLine("Do you want to delete all lists y/n? : "); // Skapa ny metod för y/n confirms?
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
        public static void ColorList(int listId)
        {
            var json = FileManager.GetJson();
            var currentList = json[listId - 1];

            Console.WriteLine("\n[1] Magenta");
            Console.WriteLine("[2] Yellow");
            Console.WriteLine("[3] Blue");
            Console.WriteLine("[4] Red");
            Console.WriteLine("[5] Cyan");
            Console.WriteLine("[6] White");

            Console.WriteLine("\nSelect color to list title: ");
            var colorId = Console.ReadLine();

            switch (colorId)
            {
                case "1":
                    currentList.TitleColor = "Magenta";
                    break;
                case "2":
                    currentList.TitleColor = "Yellow";
                    break;
                case "3":
                    currentList.TitleColor = "Blue";
                    break;
                case "4":
                    currentList.TitleColor = "Red";
                    break;
                case "5":
                    currentList.TitleColor = "Cyan";
                    break;
                case "6":
                    currentList.TitleColor = "White";
                    break;
                default:
                    Console.Clear();
                    Console.WriteLine("There are no option recognized to your input Try again!");
                    ColorList(listId);
                    return;
            }

            FileManager.UpdateJson(json);
        }
    }
}
