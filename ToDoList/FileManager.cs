using System.IO;
using System.Text.Json;

namespace ToDoList
{
    public class FileManager
    {
        private static readonly string _currentDir = Environment.CurrentDirectory;
        private static readonly string _path = Directory.GetParent(_currentDir).Parent.Parent.FullName + @"\ToDoList.json";

        public void CreateJson()
        {
            if (!File.Exists(_path))
            {
                using (var fs = File.Create(_path)) { }

                File.WriteAllText(_path, "[]");
            }
        }

        public static List<List> GetJson()
        {
            var jsonData = File.ReadAllText(_path);

            var lists = JsonSerializer.Deserialize<List<List>>(jsonData);

            return lists;
        }

        public static void UpdateJson(List<List> lists)
        {
            var jsonData = JsonSerializer.Serialize(lists);

            File.WriteAllText(_path, jsonData);
        }

    }

}

