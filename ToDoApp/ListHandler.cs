using System.Globalization;

namespace ToDoApp
{
    public class ListHandler
    {


        public static void ViewAllList(int userId)
        {
            var json = CreateUserFile.GetJson();
            Validation.GetLists(userId);
            Console.WriteLine("\n\n\nALL OF YOUR LISTS\n");
            for(int i = 0; i < json[userId].ToDoList.Count; i++)
            {
                Console.WriteLine("[" + i + "] " + json[userId].ToDoList[i].ListTitle + "\n");
            }
            return;

        }


        public static void DeleteList(int userId)
        {
            var json = CreateUserFile.GetJson();
            int num = 0;
            Console.WriteLine("\n\n\nSELECT LIST TO DELETE OR PRESS 'Q' TO QUIT. \n");
            EveryListTitleInJson(userId);
            var choosenList = Console.ReadLine().ToLower();
            if (choosenList == "q")
            {
                return;
            }

            bool valid = int.TryParse(choosenList, out num);
            if (!valid)
            {
                Console.WriteLine("You have to choose a number.");
                return;
            }

            bool listExists = Validation.IsThereValidList(num, userId);
            if (!listExists)
            {
                return;
            }

            Console.WriteLine("\nDo you want to delete this list? y/n");
            string yesOrNo = Console.ReadLine().ToLower();
            if (yesOrNo == "y")
            {
                Console.WriteLine("LIST DELETED.");
                json[userId].ToDoList.RemoveAt(num);
            }
            else if (yesOrNo == "n")
            {
                return;
            }
            else
            {
                Console.WriteLine("Only 'y' or 'n'.");
            }

            for (int i = 0; i < json[userId].ToDoList.Count; i++)
            {

                json[userId].ToDoList[i].Id = i + 1;

            }
            CreateUserFile.Update(json);
            return;
        }


        public static void ChangeListName(int userId)
        {
            var json = CreateUserFile.GetJson();
            int num = 0;

            Console.WriteLine("\n\n\nSELECT LIST TO RENAME OR PRESS 'Q' TO QUIT. \n");
            EveryListTitleInJson(userId);

            var choosenList = Console.ReadLine().ToLower();
            if (choosenList == "q")
            {
                return;
            }
            bool valid = int.TryParse(choosenList, out num);
            if (!valid)
            {
                Console.WriteLine("You have to choose a number.");
                return;
            }
            bool validOrNot = Validation.IsThereValidList(num, userId);

            if (!validOrNot)
            {
                return;
            }

            Console.WriteLine("ENTER NEW LISTNAME OR PRESS 'Q' TO QUIT.");
            string newListName = Console.ReadLine().ToUpper();

            if (String.IsNullOrEmpty(newListName))
            {
                Console.WriteLine("You have to put a name on your list.");
                return;
            }
            if (newListName == "Q")
            {
                return;
            }

            json[userId].ToDoList[num].ListTitle = newListName;

            CreateUserFile.Update(json);
            return;
        }



        public static void ViewOneList(int userId)
        {
            var json = CreateUserFile.GetJson();

            Console.WriteLine("\n\n\nSELECT LIST TO VIEW PRESS 'Q' TO QUIT.\n");
            EveryListTitleInJson(userId);
            var choosenList = Console.ReadLine().ToLower();
            if (choosenList == "q")
            {
                return;
            }

            int num = 0;
            bool valid = int.TryParse(choosenList, out num);
            if (!valid)
            {
                Console.WriteLine("You have to choose a number.");
                return;
            }
            bool validOrNot = Validation.IsThereValidList(num, userId);
            if (!validOrNot)
            {
                return;

            }
            Console.WriteLine(json[userId].ToDoList[num].ListTitle);
            Validation.IsThereAnyTasks(num, userId);
            TaskHandler.EveryTaskInList(num, userId);
        }


        public static void RecentList(int userId)
        {
            var json = CreateUserFile.GetJson();
            Validation.GetLists(userId);
            json[userId].ToDoList = json[userId].ToDoList.OrderByDescending(ToDoList => ToDoList.Date).ToList();
            Console.WriteLine("\n\n\n" + json[userId].ToDoList[0].ListTitle);

            if (json[userId].ToDoList[0].Task.Count == 0)
            {
                Console.WriteLine("Ooops.. empty! No to-do's here!");
                return;
            }

            for (int i = 0; i < json[userId].ToDoList[0].Task.Count; i++)
            {
                if (json[userId].ToDoList[0].Task[i].Completed == true)
                {

                    Console.ForegroundColor = ConsoleColor.Green;
                }

                if (json[userId].ToDoList[0].Task[i].Completed == false)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                }

                Console.WriteLine(json[userId].ToDoList[0].Task[i].TaskTitle);
                Console.ForegroundColor = ConsoleColor.White;
            }
            return;
        }


        public static void EveryListTitleInJson(int userId)
        {
            var json = CreateUserFile.GetJson();
            int index = -1;

            for (int i = 0; i < json[userId].ToDoList.Count; i++)
            {
                index++;
                Console.WriteLine(json[userId].ToDoList[i].ListTitle + "\nPress: " + "[" + index + "]" + "\n");
            }
        }


        public static void SortLists(int userId)
        {
            Console.WriteLine("\n\nHOW DO YOU WANT TO ORDER YOUR LISTS?\n" +
                "[N]ew lists first\n" +
                "[O]ld list first\n" +
                "[B]y name\n");
            var howToSort = Console.ReadLine().ToLower();
            if (string.IsNullOrEmpty(howToSort))
            {
                Console.WriteLine("Try again");
                return;
            }

            var json = CreateUserFile.GetJson();

            switch (howToSort)
            {
                case "n":
                    SortByNewest(userId);
                    break;
                case "o":
                    SortByOldest(userId);
                    break;
                case "b":
                    SortByName(userId);
                    break;
                default:
                    Console.WriteLine("Try again");
                    break;

            }
        }

        public static void SortByOldest(int userId)
        {
            var json = CreateUserFile.GetJson();
            json[userId].ToDoList = json[userId].ToDoList.OrderBy(ToDoList => ToDoList.Date).ToList();
            CreateUserFile.Update(json);
            Console.WriteLine("NEW ORDER SAVED.");
        }


        public static void SortByNewest(int userId)
        {
            var json = CreateUserFile.GetJson();
            json[userId].ToDoList = json[userId].ToDoList.OrderByDescending(ToDoList => ToDoList.Date).ToList();
            CreateUserFile.Update(json);
            Console.WriteLine("NEW ORDER SAVED.");
        }


        public static void SortByName(int userId)
        {
            var json = CreateUserFile.GetJson();
            json[userId].ToDoList = json[userId].ToDoList.OrderBy(ToDoList => ToDoList.ListTitle).ToList();
            CreateUserFile.Update(json);
            Console.WriteLine("NEW ORDER SAVED.");
        }


        public static void FinishedLists(int userId)
        {
            var json = CreateUserFile.GetJson();
            Console.WriteLine("\n\n\n\n - ALL OF YOUR FINISHED LISTS -\n\n");
            for (int i = 0; i < json[userId].ToDoList.Count; i++)
            {
                var allDone = json[userId].ToDoList[i].Task.All(x => x.Completed == true);
                var orNull = json[userId].ToDoList[i].Task.Any();
                if (allDone == true && orNull == true)
                {
                    Console.WriteLine("\n\n" + json[userId].ToDoList[i].ListTitle);
                    TaskHandler.EveryTaskInList(i, userId);
                }
            }
        }

        public static void AddListToCompleteInAWeek(int userId)
        {
            var json = CreateUserFile.GetJson();
            Console.WriteLine("\n\n\n\nWHAT LIST TO ADD TO BE COMPLETED WITHIN A WEEK? PRESS 'Q' TO QUIT.\n\n");
            for (int i = 0; i < json[userId].ToDoList.Count; i++)
            {
                if (json[userId].ToDoList[i].ThisWeek == false && json[userId].ToDoList[i].Expired == false)
                {
                    Console.WriteLine("[" + i + "] " + json[userId].ToDoList[i].ListTitle);
                }
            }
            var isThereAnyToMove = json[userId].ToDoList.All(x => x.Expired == true);
            if (isThereAnyToMove == true)
            {
                Console.WriteLine("No lists to move :-)");
                return;
            }

            var whichList = Console.ReadLine().ToLower();
            int listToMove = 0;
            if (whichList == "q")
            {
                return;
            }
            bool validOrNot = int.TryParse(whichList, out listToMove);
            if (!validOrNot)
            {
                Console.WriteLine("You have to choose a number.");
                return;
            }
            if (listToMove < json[userId].ToDoList.Count - 1 || listToMove > 0)
            {
                Console.WriteLine("That list don't exist");
                return;
            }
            json[userId].ToDoList[listToMove].ThisWeek = true;
            CreateUserFile.Update(json);
        }



        public static void ShowWeeklyLists(int userId)
        {
            var json = CreateUserFile.GetJson();
            Console.WriteLine("\n\n\nYOUR LISTS TO BE COMPLETED IN A WEEK :-)\n");
            for (int i = 0; i < json[userId].ToDoList.Count; i++)
            {
                if (json[userId].ToDoList[i].ThisWeek == true && json[userId].ToDoList[i].Expired == false)
                {
                    bool complete = json[userId].ToDoList[i].Task.All(x => x.Completed == true);
                    bool empty = json[userId].ToDoList[i].Task.Count == 0;
                    if (!complete || empty)
                    {
                        DateTime start = DateTime.Parse(json[userId].ToDoList[i].Date);
                        DateTime expiry = start.AddDays(7);
                        TimeSpan span = expiry - DateTime.Now;
                        Console.WriteLine("\n\n" + json[userId].ToDoList[i].ListTitle + "\n*" + span.Days + " days left to complete *");
                    }
                }
            }

            var noLists = json[userId].ToDoList.All(x => x.ThisWeek == false);
            if (noLists == true)
            {
                Console.WriteLine("No lists added yet!");
                return;
            }
        }


        public static void UnFinishedLists(int userId)
        {
            var json = CreateUserFile.GetJson();
            Console.WriteLine("\n\n\n- EXPIRED AND UNFINISED LISTS - \n");

            for (int i = 0; i < json[userId].ToDoList.Count; i++)
            {
                DateTime start = DateTime.Parse(json[userId].ToDoList[i].Date);
                DateTime expiry = start.AddDays(7);
                TimeSpan span = start - expiry;

                bool allCompleted = json[userId].ToDoList[i].Task.All(x => x.Completed == true);

                if (DateTime.Now > expiry)
                {
                    json[userId].ToDoList[i].Expired = true;
                    json[userId].ToDoList[i].ThisWeek = false;
                    CreateUserFile.Update(json);

                    if (!allCompleted || json[userId].ToDoList[i].Task.Count == 0)
                    {
                        Console.WriteLine("\n\n" + json[userId].ToDoList[i].ListTitle);
                    }
                }

            }

            var noLists = json[userId].ToDoList.All(x => x.Expired == false);
            if (noLists == true)
            {
                Console.WriteLine("No expired lists :-)");
                return;
            }

        }



    }
}
    
