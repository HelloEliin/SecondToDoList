using System;
using System.Diagnostics;
using System.Globalization;
using System.Linq.Expressions;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace ToDoApp
{
    public class TaskHandler
    {
        public static void AddTask(int userId)
        {
            bool isAdding = true;
            var json = CreateUserFile.GetJson();
            int num = 0;

            Console.WriteLine("\n\n\nSELECT LIST TO ADD TO-DO'S TO OR PRESS 'Q' TO QUIT.\n");
            ListHandler.EveryListTitleInJson(userId);

            var input = Console.ReadLine().ToLower();

            if (input == "q")
            {
                return;
            }

            int choosenList;

            try
            {
                choosenList = Convert.ToInt32(input);
            }

            catch(FormatException)
            {
                Console.WriteLine("You have to choose a number");
                return;
            }

            try
            {
                var currentList = json[userId].ToDoList[choosenList];
            }
            catch (Exception)
            {

                Console.WriteLine("That list does not exist");
                return;
            }


            while (isAdding)
            {
                Console.WriteLine("\n\nTO-DO TO ADD OR PRESS 'Q' TO QUIT. ");
                string taskToAdd = Console.ReadLine().ToLower();
                if (taskToAdd == "q")
                {
                    isAdding = false;
                    return;
                }

                var task = new Task()
                {
                    TaskTitle = taskToAdd,
                    Completed = false,
                    Id = json[userId].ToDoList[num].Task.Count + 1,
                };

                json[userId].ToDoList[num].Task.Add(task);
                CreateUserFile.Update(json);
            }
        }

        public static void DeleteTask(int userId)
        {
            var json = CreateUserFile.GetJson();
            Console.WriteLine("\n\n\nSELECT LIST TO DELETE TO-DO FROM OR PRESS 'Q' TO QUIT.");
            ListHandler.EveryListTitleInJson(userId);
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
            bool isExisting = Validation.IsThereValidList(num, userId);
            if (!isExisting)
            {
                return;
            }
            bool isTasks = Validation.IsThereAnyTasks(num, userId);
            if (isTasks == false)
            {
                return;
            }

            bool isDeleting = true;
            while (isDeleting)
            {
                Console.WriteLine("\n\n\nSELECT TO-DO TO DELETE OR PRESS 'Q' TO QUIT. ");
                EveryTaskInList(num, userId);

                var index = Console.ReadLine().ToLower();
                int taskToRemove = 0;
                if (index == "q")
                {
                    isDeleting = false;
                    return;
                }
                bool validOrNot = int.TryParse(index, out taskToRemove);
                if (!validOrNot)
                {
                    Console.WriteLine("You have to choose a number.");
                    return;
                }

                bool isTaskExisting = Validation.IsThereValidTask(taskToRemove, num, userId);
                if (!isTaskExisting)
                {
                    return;
                }

                Console.WriteLine("Do you want to delete this to-do? y/n");
                string yesOrNo = Console.ReadLine().ToLower();

                if (yesOrNo == "y")
                {
                    Console.WriteLine("TO-DO REMOVED.");
                    json[userId].ToDoList[num].Task.RemoveAt(taskToRemove);

                    for (int i = 0; i < json[userId].ToDoList[num].Task.Count; i++)
                    {
                        json[userId].ToDoList[num].Task[i].Id = i + 1;
                        CreateUserFile.Update(json);
                    }
                    CreateUserFile.Update(json);
                }
                else if (yesOrNo == "n")
                {
                    Console.WriteLine("TO-DO IS NOT REMOVED.");
                }
                else
                {
                    Console.WriteLine("Only 'y' or 'n'");
                    isDeleting = false;
                    return;
                }

                bool anyLeft = Validation.IsThereAnyTasks(num, userId);
                if (!anyLeft)
                {
                    return;
                }

            }
        }


        public static void ChangeTaskName(int userId)
        {
            var json = CreateUserFile.GetJson();
            Console.WriteLine("\n\n\nSELECT LIST TO EDIT TO-DO IN OR PRESS 'Q' TO QUIT.\n");
            ListHandler.EveryListTitleInJson(userId);
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
            bool isExisting = Validation.IsThereValidList(num, userId);
            if (!isExisting)
            {
                return;
            }
            bool isThereTasks = Validation.IsThereAnyTasks(num, userId);
            if (!isThereTasks)
            {
                return;
            }

            Console.WriteLine("\n\n\nSELECT TO-DO TO RENAME OR PRESS 'Q' TO QUIT");
            EveryTaskInList(num, userId);
            var taskToChange = Console.ReadLine().ToLower();
            if (taskToChange == "q")
            {
                return;
            }
            if (string.IsNullOrEmpty(taskToChange))
            {
                Console.WriteLine("You have to choose a to-do.");
                return;
            }

            int task = 0;
            bool validOrNot = int.TryParse(taskToChange, out task);
            if (!valid)
            {
                Console.WriteLine("You have to choose a number.");
                return;
            }

            bool isTaskExisting = Validation.IsThereValidTask(task, num, userId);
            if (!isTaskExisting)
            {
                return;
            }

            Console.WriteLine("\n\n\nENTER NEW TO-DO NAME OR PRESS 'Q' TO QUIT.");
            string newTaskName = Console.ReadLine().ToLower();
            if (String.IsNullOrEmpty(newTaskName))
            {
                Console.WriteLine("You have to enter a new name.");
                return;
            }
            if (newTaskName == "q")
            {
                return;
            }
            Console.WriteLine("TO-DO CHANGED.");
            json[userId].ToDoList[num].Task[task].TaskTitle = newTaskName;

            CreateUserFile.Update(json);
            return;
        }


        public static void isCompleted(int userId)
        {
            var json = CreateUserFile.GetJson();
            Console.WriteLine("\n\n\nSELECT LIST TO MARK COMPLETED TO-DO'S OR PRESS 'Q' TO QUIT.\n");
            ListHandler.EveryListTitleInJson(userId);
            var listChoice = Console.ReadLine().ToLower();
            if (listChoice == "q")
            {
                return;
            }
            int num = 0;
            bool valid = int.TryParse(listChoice, out num);
            if (!valid)
            {
                Console.WriteLine("You have to choose a number.");
                return;
            }
            bool isThereList = Validation.IsThereValidList(num, userId);
            if (!isThereList)
            {
                return;
            }

            bool isThereTasks = Validation.IsThereAnyTasks(num, userId);
            if (!isThereTasks)
            {
                return;
            }

            bool isAllCompleted = Validation.IsAllComplete(num, userId);
            if (isAllCompleted)
            {
                return;
            }

            bool isToComplete = true;
            while (isToComplete)
            {
                Console.WriteLine("\n\n\nSELECT TO-DO TO MARK AS COMPLETE OR PRESS 'Q' TO QUIT.\n");
                EveryTaskInList(num, userId);
                var whatToDo = Console.ReadLine().ToLower();
                int taskToChange = 0;
                if (whatToDo == "q")
                {
                    return;
                }
                bool validOrNot = int.TryParse(whatToDo, out taskToChange);
                if (!validOrNot)
                {
                    Console.WriteLine("You have to choose a number.");
                    return;
                }
                bool isThereTask = Validation.IsThereValidTask(taskToChange, num, userId);
                if (!isThereTask)
                {
                    return;
                }
                json[userId].ToDoList[num].Task[taskToChange].Completed = true;
                CreateUserFile.Update(json);
                Console.WriteLine("\n\n\nHurray!");
                bool isItComplete = Validation.IsAllComplete(num, userId);
                if (isItComplete)
                {
                    return;
                }
            }
            isToComplete = false;
            return;
        }


        public static void EveryTaskInList(int list, int userId)
        {
            var json = CreateUserFile.GetJson();
            int index = -1;

            foreach (var task in json[userId].ToDoList[list].Task)
            {
                if (task.Completed == true)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                }
                if (task.Completed == false)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                }
                index++;
                Console.WriteLine("[" + index + "] " + task.TaskTitle);
                Console.ForegroundColor = ConsoleColor.White;
            }
        }

    }

}
