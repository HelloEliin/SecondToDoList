using System;
using System.Linq;
using System.Text;
using ToDoApp;

namespace ToDoApp
{
    public class Validation
    {


        public static bool GetLists(int userId)
        {
            var json = CreateUserFile.GetJson();
            if (json[userId].ToDoList.Any() != true)
            {
                Console.WriteLine("You have no lists.");
                return false;
            }
            return true;
        }


        public static bool IsThereValidList(int choosenList, int userId)
        {
            var json = CreateUserFile.GetJson();
            if (choosenList > json[userId].ToDoList.Count - 1 || choosenList < 0)
            {
                Console.WriteLine("\n\nThat list does not exist.");
                return false;
            }
            return true;
        }


        public static bool IsThereAnyTasks(int choosenList, int userId)
        {
            var json = CreateUserFile.GetJson();
            if (json[userId].ToDoList[choosenList].Task.Count == 0)
            {
                Console.WriteLine("\n\nYou have no to-do's in this list.");
                return false;
            }
            return true;
        }


        public static bool IsThereValidTask(int task, int choosenList, int userId)
        {
            var json = CreateUserFile.GetJson();
            if (json[userId].ToDoList[choosenList].Task.Count - 1 < task || task < 0)
            {
                Console.WriteLine("\n\nThat to-do does not exist.");
                return false;
            }
            return true;
        }


        public static bool IsAllComplete(int num, int userId)
        {
            var json = CreateUserFile.GetJson();
            bool allCompleted = json[userId].ToDoList[num].Task.All(x => x.Completed == true);
            if (allCompleted)
            {
                Console.WriteLine("\n\n\n\nYou're a star baby!\nAll to-do's are completed in this list!\n");
                Console.WriteLine(json[userId].ToDoList[num].ListTitle);
                TaskHandler.EveryTaskInList(num, userId);
                return true;
            }
            return false;
        }



    }
}
