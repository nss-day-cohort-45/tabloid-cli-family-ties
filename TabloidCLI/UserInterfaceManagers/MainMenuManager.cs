using System;
using System.Collections.Generic;

namespace TabloidCLI.UserInterfaceManagers
{
    public class MainMenuManager : IUserInterfaceManager
    {
        private const string CONNECTION_STRING =
            @"Data Source=localhost\SQLEXPRESS;Database=TabloidCLI;Integrated Security=True";

        public IUserInterfaceManager Execute()
        {
            Console.WriteLine("Greetings, Welcome to Tabloid! ");
            Console.WriteLine("Main Menu");

            Console.WriteLine(" 1) Journal Management");
            Console.WriteLine(" 2) Blog Management");
            Console.WriteLine(" 3) Author Management");
            Console.WriteLine(" 4) Post Management");
            Console.WriteLine(" 5) Tag Management");
            Console.WriteLine(" 6) Search by Tag");
            Console.WriteLine(" 0) Exit");

            Console.Write("> ");
            string choice = Console.ReadLine();
      
            //bool runningMenu = true;
            //while (runningMenu)
            //{
                switch (choice)
                {
                    case "1": return new JournalManager(this, CONNECTION_STRING);
                    case "2": throw new NotImplementedException();
                    case "3": return new AuthorManager(this, CONNECTION_STRING);
                    case "4": return new PostManager(this, CONNECTION_STRING);
                    case "5": return new TagManager(this, CONNECTION_STRING);
                    case "6": return new SearchManager(this, CONNECTION_STRING);
                    case "0":
                        Console.WriteLine("Good bye");
                        return null;
                    default:
                        Console.WriteLine("Invalid Selection");
                        return this;
                }
            //}
        }

        //    static string GetMenuSelection()
        //    {
        //        Console.Clear();

        //        List<string> options = new List<string>()
        //    {
        //        "1",
        //        "2",
        //        "3",
        //        "4",
        //        "5",
        //        "6",
        //        "0",
        //    };

        //        for (int i = 0; i < options.Count; i++)
        //        {
        //            Console.WriteLine($"{i + 1}. {options[i]}");
        //        }

        //        while (true)
        //        {
        //            try
        //            {
        //                Console.WriteLine();
        //                Console.Write("Select an option > ");

        //                string input = Console.ReadLine();
        //                int index = int.Parse(input) - 1;
        //                return options[index];
        //            }
        //            catch (Exception)
        //            {

        //                continue;
        //            }
        //        }

        //    }

    }
}
