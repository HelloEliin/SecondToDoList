using System.Security.Cryptography.X509Certificates;
using System.Text.Json.Serialization;
using ToDoApp;

namespace ToDoList
{
    public class ListProgram
    {

        public static void ToDolistMenu(int userIndex)
        {
            string menuChoice;
            bool isRunning = true;

            do
            {
                var json = CreateUserFile.GetJson();
                ToDoListMenu.StartMenu(userIndex);
                menuChoice = Console.ReadLine().ToLower();

                switch (menuChoice)
                {
                    case "o":
                        bool isThereAnyLists = Validation.IsThereAnyLists(userIndex);
                        if (isThereAnyLists == true)
                        {
                            ListHandler.RecentList(userIndex);
                            ToDoListMenu.ListMenu(userIndex);
                        }
                        break;
                    case "v":
                        isThereAnyLists = Validation.IsThereAnyLists(userIndex);
                        if (isThereAnyLists == true)
                        {
                            ToDoListMenu.ListMenu(userIndex);
                        }
                        break;
                    case "c":
                        CreateToDoList.CreateNewToDoList(userIndex);
                        break;
                    case "d":
                        isThereAnyLists = Validation.IsThereAnyLists(userIndex);
                        if (isThereAnyLists == true)
                        {
                            ListHandler.DeleteList(userIndex);
                        }
                        break;
                    case "b":
                        if (json[userIndex].AccessLevelOne == true || json[userIndex].AccessLevelMod)
                        {
                            UserMenus.UserSystemMenu(userIndex);
                        }
                        if (json[userIndex].AccessLevelAdm == true)
                        {
                            UserMenus.UserSystemMenu(userIndex);
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





