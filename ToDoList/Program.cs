namespace ToDoList
{
    internal class Program
    {
        static void Main(string[] args)
        {
          
            var jsonFile = new FileManager();
            jsonFile.CreateJson();
            
            StartMenu.CallStartMenu();

        }
    }
}
