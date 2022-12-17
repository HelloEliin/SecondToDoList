using ToDoApp;

namespace ToDoApp
{
    public class ToDoListMenu
    {

        public static void StartMenu(int userIndex)
        {
            Console.WriteLine("\n\n\n\n[MY DO-DO LISTS]\n\n" +
            "[O]pen recent list\n" +
            "[V]iew lists and listmenu\n" +
            "[C]reate new list\n" +
            "[D]elete list\n" +
            "[Q]uit");
        }





        public static void ListMenu(int userIndex)
        {
            var json = CreateUserFile.GetJson();
            Console.WriteLine("\n\n\n\nLISTMENU\n" +
            "\n--- [1] LISTS TO BE COMPLETE WITHIN A WEEK\n" +
            "--- [2] ADD LISTS TO BE COMPLETE WITHIN A WEEK \n" +
            "--- [3] EXPIRED LISTS \n" +
            "--- [4] FINISHED LISTS \n\n" +
            "[V]iew lists\n" +
            "[B]ack to startmenu\n" +
            "[R]ename list\n" +
            "[A]dd to-do\n" +
            "[M]ark task as complete\n" +
            "[T]o do menu\n" +
            "[D]elete to-do\n" +
            "[S]ort lists");

            string choice = Console.ReadLine().ToLower();

            switch (choice)
            {
                case "b":
                    break;
                case "v":
                    CreateToDoList.ViewOneList(userIndex);
                    break;

                case "r":
                    CreateToDoList.ChangeListName(userIndex);
                    break;
                case "a":
                    AddNewTask.AddTask(userIndex);
                    break;
                case "m":
                    AddNewTask.isCompleted(userIndex);
                    break;
                case "t":
                    TaskMenu(userIndex);
                    break;
                case "d":
                    AddNewTask.DeleteTask(userIndex);
                    break;
                //case "s":
                //    CreateToDoList.SortLists(userIndex);
                //    break;
                case "1":
                    CreateToDoList.ShowWeeklyLists(userIndex);
                    break;
                case "2":
                    CreateToDoList.AddListToCompleteInAWeek(userIndex);
                    break;
                case "3":
                    CreateToDoList.UnFinishedLists(userIndex);
                    break;
                case "4":
                    CreateToDoList.FinishedLists(userIndex);
                    break;
                default:
                    Console.WriteLine("Try again.");
                    break;
            }
        }



        public static void TaskMenu(int userIndex)
        {
            Console.WriteLine("\n\n\nTO-DO MENU\n" +
           "[R]ename to-do\n" +
           "[D]elete to-to\n" +
           "[B]ack to list menu\n");

            var choice = Console.ReadLine().ToLower();

            switch (choice)
            {
                case "r":
                    AddNewTask.ChangeTaskName(userIndex);
                    break;
                case "d":
                    AddNewTask.DeleteTask(userIndex);
                    break;
                case "b":
                    ListMenu(userIndex);
                    break;
                default:
                    Console.WriteLine("Try again.");
                    break;
            }

        }


    }

}