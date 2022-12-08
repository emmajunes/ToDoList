
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
        public string ListDateTime { get; set; }
        public List<Task> Tasks { get; set; }


        public static void CreateList()
        {
            var json = FileManager.GetJson();

            Console.Write("Enter a title of the new list: ");
            var title = (Console.ReadLine());

            if (String.IsNullOrWhiteSpace(title))
            {
                Console.WriteLine("List title cannot be empty");

                CreateList();
                return;
            }

            var newList = new List()
            {
                ListTitle = title,
                Tasks = new List<Task>(),
                ListDateTime = DateTime.Now.ToString(),
            };

            json.Add(newList);

            FileManager.UpdateJson(json);

            Console.Clear();
            Console.WriteLine("New created list: " + title);

            int createdList = json.Count;
            ColorList(createdList);
            Console.Clear();
        }

        public static void ViewAllLists()
        {
            var json = FileManager.GetJson();

            Dictionary<string, int> colors = new()
            {
                { "Magenta", 13 },
                { "Yellow", 14 },
                { "Blue", 9 },
                { "Red", 12 },
                { "Cyan", 11 },
                { "White", 15 }
            };

            Console.WriteLine("\nOVERVIEW OF LISTS: \n");

            int index = 1;
            foreach (var list in json)
            {
                Console.ForegroundColor = (ConsoleColor)colors[list.TitleColor];

                Console.WriteLine($"[{index}] {list.ListTitle}");
                index++;
                Console.ForegroundColor = ConsoleColor.White;
            }
        }

        public static void ChooseList()
        {
            AvailableLists();

            int listIndex;

            try
            {
                Console.Write("\nChoose a list: ");
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

            AvailableLists();
            Task.ViewTasks(lastCreated);
            ListMenu.CallListMenu(lastCreated);
        }

        public static void EditList(int listId)
        {
            var json = FileManager.GetJson();
            var currentList = json[listId - 1];

            Console.WriteLine("Write a new title to the list: ");
            var title = Console.ReadLine();

            if (String.IsNullOrWhiteSpace(title))
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
            AvailableLists();
            ViewAllLists();
            int deleteList;

            try
            {
                Console.Write("\nChoose a list to delete: ");
                var deleteIndex = Convert.ToInt32(Console.ReadLine());

                if (deleteIndex <= 0 || json.Count < deleteIndex)
                {
                    Console.Clear();
                    Console.WriteLine("Id does not exist. Try again!");
                    DeleteList();
                    return;
                }

                deleteList = deleteIndex - 1;

                Console.WriteLine("Do you want to delete this list y/n? ");
                var deleteAnswer = Console.ReadLine().ToUpper();

                if (String.IsNullOrWhiteSpace(deleteAnswer))
                {
                    Console.WriteLine("Input field cannot be empty");
                    DeleteList();
                    return;
                }

                if (deleteAnswer == "Y")
                {
                    Console.Clear();
                    Console.WriteLine($"Deleted list: {json[deleteList].ListTitle}");
                    json.RemoveAt(deleteList);
                    FileManager.UpdateJson(json);
                    return;
                }

                else if (deleteAnswer == "N")
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
            AvailableLists();

            Console.WriteLine("Do you want to delete all lists y/n? ");
            var deleteAnswer = Console.ReadLine().ToUpper();

            if (deleteAnswer == "Y")
            {
                Console.Clear();
                Console.WriteLine("All lists deleted");
                json.Clear();
                FileManager.UpdateJson(json);
                return;
            }

            else if (deleteAnswer == "N")
            {
                Console.Clear();
                return;
            }

            else
            {
                Console.WriteLine("Answer needs to be a letter of y or n");
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

        public static void SortList()
        {
            var json = FileManager.GetJson();
            AvailableLists();

            Console.WriteLine("\n[1] Sort by latest created list");
            Console.WriteLine("[2] Sort by oldest created list");
            Console.WriteLine("[3] Sort by name");
            Console.WriteLine("[4] Sort by color");

            Console.Write("\nSelect an option: ");
            var input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    json = json.OrderByDescending(x => x.ListDateTime).ToList();
                    break;
                case "2":
                    json = json.OrderBy(x => x.ListDateTime).ToList();
                    break;
                case "3":
                    json = json.OrderBy(x => x.ListTitle).ToList();
                    break;
                case "4":
                    json = json.OrderBy(x => x.TitleColor).ToList();
                    break;
                default:
                    Console.Clear();
                    Console.WriteLine("\nThere are no option recognized to your input. Try again!");
                    SortList();
                    return;
            }

            FileManager.UpdateJson(json);
            Console.Clear();
            ViewAllLists();
        }

        public static void AvailableLists()
        {
            var json = FileManager.GetJson();

            if (json.Count == 0)
            {
                Console.Clear();
                Console.WriteLine("No lists available");
                StartMenu.CallStartMenu();
            }
        }
    }
}
