using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text.Json;

namespace ToDoApp
{

    public class CreateToDoListFile
    {

  
        private static string _currentDi = Environment.CurrentDirectory;
        private static string _path = Directory.GetParent(_currentDi).Parent.Parent.FullName + @"\ToDoList.json";


        public void CreateFile()
        {
            if (!File.Exists(_path))
            {
                using (var fs = File.Create(_path)) { }
                File.WriteAllText(_path, "[]");


            }

        }


        public static void UpDate(List<CreateToDoList> lists)
        {
            var jsondata = JsonSerializer.Serialize(lists);
            File.WriteAllText(_path, jsondata);
        }

        public static List<CreateToDoList> GetJson()
        {
            var jsondata = File.ReadAllText(_path);
            var lists = JsonSerializer.Deserialize<List<CreateToDoList>>(jsondata);

            return lists;
        }









    }
}