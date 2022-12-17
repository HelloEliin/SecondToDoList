using System.Security.Cryptography.X509Certificates;
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

                ToDoListMenu.StartMenu(userIndex);
                menuChoice = Console.ReadLine().ToLower();

                switch (menuChoice)
                {


                    case "o":
                        bool isThereAnyLists = Validation.IsThereAnyLists(userIndex);
                        if (isThereAnyLists == true)
                        {
                            CreateToDoList.RecentList(userIndex);
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
                            CreateToDoList.DeleteList(userIndex);
                        }
                        break;

                    case "q":
                        Console.WriteLine("Do you want to quit? y/n");
                        string yesOrNo = Console.ReadLine().ToLower();

                        if (yesOrNo == "y")
                        {
                            isRunning = false;
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

            



   