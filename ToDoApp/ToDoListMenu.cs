using ToDoApp;

namespace ToDoApp
{
    public class ToDoListMenu
    {

        public static void StartMenu(int userId)
        {
            Console.WriteLine("\n\n\n\n[MY DO-DO LISTS]\n\n" +
            "[O]pen recent list\n" +
            "[V]iew lists and listmenu\n" +
            "[C]reate new list\n" +
            "[D]elete list\n" +
            "[B]ack to start");
        }
        public static void ListMenu(int userId)
        {
            var json = CreateUserFile.GetJson();
            Console.WriteLine("\n\n\n\nLISTMENU\n" +
            "\n--- [1] LISTS TO BE COMPLETE WITHIN A WEEK\n" +
            "--- [2] ADD LISTS TO BE COMPLETE WITHIN A WEEK \n" +
            "--- [3] EXPIRED LISTS \n" +
            "--- [4] FINISHED LISTS \n\n" +
            "[S]how all my lists\n" +
            "[V]iew list\n" +
            "[B]ack to startmenu\n" +
            "[R]ename list\n" +
            "[A]dd to-do\n" +
            "[M]ark task as complete\n" +
            "[T]o do menu\n" +
            "[D]elete to-do\n" +
            "[O]rder my lists");

            string choice = Console.ReadLine().ToLower();

            switch (choice)
            {
                case "b":
                    break;
                case "v":
                    ListHandler.ViewOneList(userId);
                    break;
                case "r":
                    ListHandler.ChangeListName(userId);
                    break;
                case "a":
                    TaskHandler.AddTask(userId);
                    break;
                case "m":
                    TaskHandler.isCompleted(userId);
                    break;
                case "t":
                    TaskMenu(userId);
                    break;
                case "d":
                    TaskHandler.DeleteTask(userId);
                    break;
                case "o":
                    ListHandler.SortLists(userId);
                    break;
                case "s":
                    ListHandler.ViewAllList(userId);
                    break;
                case "1":
                    ListHandler.ShowWeeklyLists(userId);
                    break;
                case "2":
                    ListHandler.AddListToCompleteInAWeek(userId);
                    break;
                case "3":
                    ListHandler.UnFinishedLists(userId);
                    break;
                case "4":
                    ListHandler.FinishedLists(userId);
                    break;
                default:
                    Console.WriteLine("Try again.");
                    break;
            }
        }

        public static void TaskMenu(int userId)
        {
            Console.WriteLine("\n\n\nTO-DO MENU\n" +
           "[R]ename to-do\n" +
           "[D]elete to-to\n" +
           "[B]ack to list menu\n");

            var choice = Console.ReadLine().ToLower();

            switch (choice)
            {
                case "r":
                    TaskHandler.ChangeTaskName(userId);
                    break;
                case "d":
                    TaskHandler.DeleteTask(userId);
                    break;
                case "b":
                    ListMenu(userId);
                    break;
                default:
                    Console.WriteLine("Try again.");
                    break;
            }

        }


    }

}