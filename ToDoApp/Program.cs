using System.Security.Cryptography.X509Certificates;
using ToDoApp;

namespace ToDoApp
{
    internal partial class Program
    {
        static void Main(string[] args)
        {

            bool isRunning = true;
            var json = new CreateUserFile();
            json.CreateFile();

            while (isRunning)
            {
                UserMenus.FrontPageMenu();
                var choice = Console.ReadLine().ToLower();

                switch (choice)
                {
                    case "s":
                        int userId = SignIn.SignInNow();
                        if (userId == -1)
                        {
                            Console.WriteLine("\n\nWrong username or password.");
                        }
                        else if (userId == -10)
                        {
                            break;
                        }
                        else
                        {
                            UserMenus.UserSystemMenu(userId);
                        }
                        break;
                    case "c":
                        CreateUser.CreateNewUser();
                        break;
                    case "q":
                        isRunning = false;
                        break;
                    default:
                        Console.WriteLine("\nChoose option.");
                        break;
                }
            }


        }
    }
}