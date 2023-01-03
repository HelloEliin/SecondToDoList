using System.Security.Cryptography.X509Certificates;
using System.Text.Json.Serialization;
using ToDoApp;

namespace ToDoList
{
    public class ListProgram
    {

        public static void ToDolistMenu(int userId)
        {
            string menuChoice;
            bool isRunning = true;

            do
            {
                var json = CreateUserFile.GetJson();
                ToDoListMenu.StartMenu(userId);
                menuChoice = Console.ReadLine().ToLower();

                switch (menuChoice)
                {
                    case "o":
                        bool isThereAnyLists = Validation.GetLists(userId);
                        if (isThereAnyLists == true)
                        {
                            ListHandler.RecentList(userId);
                            ToDoListMenu.ListMenu(userId);
                        }
                        break;
                    case "v":
                        isThereAnyLists = Validation.GetLists(userId);
                        if (isThereAnyLists == true)
                        {
                            ToDoListMenu.ListMenu(userId);
                        }
                        break;
                    case "c":
                        CreateToDoList.CreateNewToDoList(userId);
                        break;
                    case "d":
                        isThereAnyLists = Validation.GetLists(userId);
                        if (isThereAnyLists == true)
                        {
                            ListHandler.DeleteList(userId);
                        }
                        break;
                    case "b":
                        if (json[userId].AccessLevelOne == true || json[userId].AccessLevelMod)
                        {
                            UserMenus.UserSystemMenu(userId);
                        }
                        if (json[userId].AccessLevelAdm == true)
                        {
                            UserMenus.UserSystemMenu(userId);
                        }
                        break;
                    default:
                        Console.WriteLine("Try again.");
                        break;
                }

            } while (isRunning);
        }
    }

}





