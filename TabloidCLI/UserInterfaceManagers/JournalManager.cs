using System;
using System.Collections.Generic;
using Journal.Models;
using Journal.Repositories;

namespace TabloidCLI.UserInterfaceManagers
  //private const string CONNECTION_STRING = @"server=localhost\SQLExpress;database=Roommates;integrated security=true";
{
    public class JournalManager : IUserInterfaceManager
    {
        JournalRepository journalRepo = new JournalRepository(CONNECTION_STRING);
        
        bool runProgram = true;
        while (runProgram)
        {
            string choice = GetMenuSelection();
            switch (choice)

            {
                case "1":
                    Console.Write("Please enter entry title.");
                    string journalTitle = Console.ReadLine();

                    Console.Write("Please enter entry content.");
                    string journalContent = Console.ReadLine();

                    Journal entryToAdd = new Journal()
                    {
                        Title = journalTitle,
                        Content = journalContent,
                        CreationDate =
                                    };
                    journalRepo.Insert(entryToAdd);
                        Console.WriteLine($"{entryToAdd.Title} has been added and assigned an Id of {entryToAdd.Id}");
                        Console.Write("Press any key to continue");
                        Console.ReadKey();
                break;
            }

            static string GetMenuSelection()
            {
                Console.Clear();

                List<string> options = new List<string>()
            {
                "Add Journal Entry",
                "Back to Main Menu"
            };

                for (int i = 0; i < options.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {options[i]}");
                }

                while (true)
                {
                    try
                    {
                        Console.WriteLine();
                        Console.Write("Select an option > ");

                        string input = Console.ReadLine();
                        int index = int.Parse(input) - 1;
                        return options[index];
                    }
                    catch (Exception)
                    {

                        continue;
                    }
                }

            }
    }
}