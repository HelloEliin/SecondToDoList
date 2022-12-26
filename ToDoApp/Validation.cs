using System;
using System.Linq;
using System.Text;
using ToDoApp;

namespace ToDoApp
{
    public class Validation
    {


        public static bool IsThereAnyLists(int user)
        {
            var json = CreateUserFile.GetJson();
            if (json[user].ToDoList.Any() != true)
            {
                Console.WriteLine("You have no lists.");
                return false;
            }
            return true;
        }


        public static bool IsThereValidList(int choosenList, int user)
        {
            var json = CreateUserFile.GetJson();
            if (choosenList > json[user].ToDoList.Count - 1 || choosenList < 0)
            {
                Console.WriteLine("\n\nThat list dont exist.");
                return false;
            }
            return true;
        }


        public static bool IsThereAnyTasks(int choosenList, int user)
        {
            var json = CreateUserFile.GetJson();
            if (json[user].ToDoList[choosenList].Task.Count == 0)
            {
                Console.WriteLine("\n\nYou have no to-do's in this list.");
                return false;
            }
            return true;
        }


        public static bool IsThereValidTask(int task, int choosenList, int user)
        {
            var json = CreateUserFile.GetJson();
            if (json[user].ToDoList[choosenList].Task.Count - 1 < task || json[user].ToDoList[choosenList].Task.Count > 0)
            {
                Console.WriteLine("\n\nThat to-do dont exist.");
                return false;
            }
            return true;
        }


        public static bool IsAllComplete(int num, int user)
        {
            var json = CreateUserFile.GetJson();
            bool allCompleted = json[user].ToDoList[num].Task.All(x => x.Completed == true);
            if (allCompleted)
            {
                Console.WriteLine("\n\n\n\nYou're a star baby!\nAll to-do's are completed in this list!\n");
                Console.WriteLine(json[user].ToDoList[num].ListTitle);
                AddNewTask.EveryTaskInList(num, user);
                return true;
            }
            return false;
        }



    }
}
