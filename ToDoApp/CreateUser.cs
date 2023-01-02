using System;
using System.Collections.Concurrent;
using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.Json.Nodes;
using System.Text.RegularExpressions;
using System.Xml.Serialization;

namespace ToDoApp
{
    public class CreateUser
    {
        public int Id { get; set; } 
        public static int startId = 0;
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool AccessLevelOne { get; set; }
        public bool AccessLevelMod { get; set; }
        public bool AccessLevelAdm { get; set; }
        public List<CreateToDoList> ToDoList { get; set; }  

        public static void CreateNewUser()
        {
            var json = CreateUserFile.GetJson();

            Console.WriteLine("\nENTER USERNAME (OR PRESS 'Q' TO QUIT)");
            var userName = Console.ReadLine();
            if (userName == "q" || userName == "Q")
            {
                return;
            }

            if (String.IsNullOrWhiteSpace(userName) || userName.Any(char.IsWhiteSpace))
            {
                do
                {
                    Console.WriteLine("You have to choose username");
                    userName = Console.ReadLine();
                    if (userName == "q" || userName == "Q")
                    {
                        return;
                    }
                } while (userName == "" || userName.Any(char.IsWhiteSpace));
            }


            bool usernameAvalible = UserHandler.IsUserNameAvalible(userName);

            while (!usernameAvalible)
            {
                Console.WriteLine("\nSomeone else already uses this username");
                userName = Console.ReadLine();
                if (userName == "q" || userName == "Q")
                {
                    return;
                }

                if (string.IsNullOrWhiteSpace(userName))
                {
                    Console.WriteLine("You have to choose username (OR PRESS 'Q' TO QUIT)");

                }
                usernameAvalible = UserHandler.IsUserNameAvalible(userName);

            }


            Console.WriteLine("ENTER FIRST NAME (OR PRESS 'Q' TO QUIT)");
            var firstName = Console.ReadLine();

            if (firstName == "q" || firstName == "Q")
            {
                return;
            }

            if (String.IsNullOrEmpty(firstName) || firstName.Any(char.IsWhiteSpace))
            {
                do
                {
                    Console.WriteLine("You have to enter first name (OR PRESS 'Q' TO QUIT)");
                    firstName = Console.ReadLine();
                    if (firstName == "q" || firstName == "Q")
                    {
                        return;
                    }
                } while (firstName == "" || firstName.Any(char.IsWhiteSpace));

            }




            Console.WriteLine("\nENTER YOUR LASTNAME (OR PRESS 'Q' TO QUIT)");
            var lastName = Console.ReadLine();
            if (lastName == "q" || lastName == "Q")
            {
                return;
            }

            if (String.IsNullOrWhiteSpace(lastName) || lastName.Any(char.IsWhiteSpace))
            {
                do
                {
                    Console.WriteLine("You have to enter lastname (OR PRESS 'Q' TO QUIT)");
                    lastName = Console.ReadLine();
                    if (lastName == "q" || lastName == "Q")
                    {
                        return;
                    }
                } while (lastName == "" || lastName.Any(char.IsWhiteSpace));

            }



            Console.WriteLine("\nENTER YOUR EMAIL (OR PRESS 'Q' TO QUIT)");
            string email = Console.ReadLine();
            if(email == "q" || email == "Q")
            {
                return;
            }

            string theEmail = UserHandler.IsValidEmail(email);

            if (theEmail == "-1")
            {
                do
                {
                    Console.WriteLine("ENTER NEW EMAIL (OR PRESS 'Q' TO QUIT)");
                    email = Console.ReadLine();
                    theEmail = UserHandler.IsValidEmail(email);

                } while (theEmail == "-1");
            }

            if (theEmail == "-2")
            {
                return;
            }

            email = theEmail;



            Console.WriteLine("\nENTER A PASSWORD. (MIN 8 LETTERS, 1 BIG LETTER AND ONE SYMBOL OR PRESS 'Q' TO QUIT)");
            var password = UserHandler.ReadPassword();

            if (password == "q" || password == "Q")
            {
                return;
            }
            bool validOrNot = UserHandler.ValidPassword(password);
            if (!validOrNot)
            {
                do
                {
                    Console.WriteLine("\nENTER NEW PASSWORD (OR PRESS 'Q' TO QUIT)");
                    password =   UserHandler.ReadPassword();
                    validOrNot = UserHandler.ValidPassword(password);
                    if (password == "q" || password == "Q")
                    {
                        return;
                    }

                } while (!validOrNot);
            }


            Console.WriteLine("\n\n\nYAY! ACCOUNT CREATED, YOU CAN NOW SIGN IN!");


            var user = new CreateUser()
            {
                Id = startId++,
                FirstName = firstName,
                LastName = lastName,
                UserName = userName,
                Email = email,
                Password = password,
                AccessLevelOne = true,
                AccessLevelMod = false,
                AccessLevelAdm = false,
                ToDoList =  new List<CreateToDoList>()


            };
            json.Add(user);
            CreateUserFile.Update(json);
            return;

        }

    }


}
