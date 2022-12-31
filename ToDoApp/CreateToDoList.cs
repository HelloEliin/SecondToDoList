using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net.Http.Headers;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ToDoApp
{
    public class CreateToDoList
    {
        public string ListTitle { get; set; }
        public List<Task> Task { get; set; }
        public string Date { get; set; }
        public bool ThisWeek { get; set; }
        public bool Expired { get; set; }
        public int Id { get; set; } 
        public static int startId = 0;

        public static void CreateNewToDoList(int userIndex)
        {
            var json = CreateUserFile.GetJson();
            Console.WriteLine("\n\nENTER NAME OF LIST OR PRESS 'Q' TO QUIT.\n");
            var listName = Console.ReadLine().ToUpper();
            if (listName == "Q")
            {
                return;
            }
            if (String.IsNullOrWhiteSpace(listName))
            {
                Console.WriteLine("You have to put a name on your list.");
                return;
            }


            var newList = new CreateToDoList()
            {
                ListTitle = listName,
                Task = new List<Task>(),
                Date = DateTime.Now.ToString("G"),
                ThisWeek = false,
                Expired = false,
                Id = json[userIndex].ToDoList.Count + 1,

            };


            json[userIndex].ToDoList.Add(newList);
            CreateUserFile.UpDate(json);

            return;
        }

    }
}
