using System.Reflection.Metadata;
using System.Text.Json;

namespace ToDoApp
{

    public class CreateUserFile
        {

        private static string _currentDi = Environment.CurrentDirectory;
        private static string _path = Directory.GetParent(_currentDi).Parent.Parent.FullName + @"\UserFile.json";
        public void CreateFile()
        {
            try
            {
                if (!File.Exists(_path))
                {

                    File.WriteAllText(_path, "[]");
                }
            }
            catch (IOException)
            {
                throw new IOException();
            }
        }

        public static void Update(List<CreateUser> lists)
        {
            try
            {
                var jsondata = JsonSerializer.Serialize(lists);
                File.WriteAllText(_path, jsondata);
            }
            catch (IOException)
            {
                throw new IOException();
            }
        }

        public static List<CreateUser> GetJson()
        {
            try
            {
                var jsondata = File.ReadAllText(_path);
                var lists = JsonSerializer.Deserialize<List<CreateUser>>(jsondata);

                return lists;
            }

            catch (IOException)
            {
                throw new IOException();
            }
        }
    }
}
