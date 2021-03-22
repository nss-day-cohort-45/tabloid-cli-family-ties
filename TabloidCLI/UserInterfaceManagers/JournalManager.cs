﻿using System;
using System.Collections.Generic;
using TabloidCLI.Models;
using TabloidCLI.Repositories;

namespace TabloidCLI.UserInterfaceManagers
{
    public class JournalManager : IUserInterfaceManager
    {
        private readonly IUserInterfaceManager _parentUI;
        private JournalRepository _JournalRepository;
        private string _connectionString;

        public JournalManager(IUserInterfaceManager parentUI, string connectionString)
        {
            _parentUI = parentUI;
            _JournalRepository = new JournalRepository(connectionString);
            _connectionString = connectionString;
        }

        public IUserInterfaceManager Execute()
        {
            Console.WriteLine("Journal Menu");
            Console.WriteLine(" 1) List Journals");
            Console.WriteLine(" 2) Add Journal");
            Console.WriteLine(" 3) Edit Journal");
            Console.WriteLine(" 4) Remove Journal");
            Console.WriteLine(" 0) Go Back");

            Console.Write("> ");
            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    List();
                    return this;
                case "2":
                    Add();
                    return this;
                case "3":
                    Edit();
                    return this;
                case "4":
                    Remove();
                    return this;
                case "0":
                    return _parentUI;
                default:
                    Console.WriteLine("Invalid Selection");
                    return this;
            }
        }

        private void List()
        {
            List<Journal> Journals = _JournalRepository.GetAll();
            foreach (Journal Journal in Journals)
            {
                Console.WriteLine(Journal);
            }
        }

        private Journal Choose(string prompt = null)
        {
            if (prompt == null)
            {
                prompt = "Please choose a Journal:";
            }

            Console.WriteLine(prompt);

            List<Journal> Journals = _JournalRepository.GetAll();

            for (int i = 0; i < Journals.Count; i++)
            {
                Journal Journal = Journals[i];
                Console.WriteLine($" {i + 1}) {Journal.Title}");
            }
            Console.Write("> ");

            string input = Console.ReadLine();
            try
            {
                int choice = int.Parse(input);
                return Journals[choice - 1];
            }
            catch (Exception ex)
            {
                Console.WriteLine("Invalid Selection");
                return null;
            }
        }

        private void Add()
        {
            Console.WriteLine("New Journal");
            Journal journal = new Journal();

            Console.WriteLine("Title: ");
            journal.Title = Console.ReadLine();

            Console.WriteLine("Text Content: ");
            journal.Content = Console.ReadLine();

            //make this an auto time setter
            Console.WriteLine("Date: ");
            journal.CreateDateTime = DateTime.Now;

            _JournalRepository.Insert(journal);
        }

        private void Edit()
        {
            Journal JournalToEdit = Choose("Which Journal would you like to edit?");
            if (JournalToEdit == null)
            {
                return;
            }

            Console.WriteLine();
            Console.Write("New title (blank to leave unchanged: ");
            string title = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(title))
            {
                JournalToEdit.Title = title;
            }
            Console.Write("New content (blank to leave unchanged: ");
            string content = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(content))
            {
                JournalToEdit.Content = content;
            }
            Console.Write("New Date (blank to leave unchanged: ");
            
           
                JournalToEdit.CreateDateTime = DateTime.Now;
           

            _JournalRepository.Update(JournalToEdit);
        }

        private void Remove()
        {
            Journal JournalToDelete = Choose("Which Journal would you like to remove?");
            if (JournalToDelete != null)
            {
                _JournalRepository.Delete(JournalToDelete.Id);
            }
        }
    }
}

