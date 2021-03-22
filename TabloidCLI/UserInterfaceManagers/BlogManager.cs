using System;
using System.Collections.Generic;
using TabloidCLI.Models;
using TabloidCLI.Repositories;

namespace TabloidCLI.UserInterfaceManagers
{
    public class BlogManager : IUserInterfaceManager
    {
        private readonly IUserInterfaceManager _parentUI;
        private BlogRepository _BlogRepository;
        private string _connectionString;

        public BlogManager(IUserInterfaceManager parentUI, string connectionString)
        {
            _parentUI = parentUI;
            _BlogRepository = new BlogRepository(connectionString);
            _connectionString = connectionString;
        }

        public IUserInterfaceManager Execute()
        {
            Console.WriteLine("Blog Menu");
            Console.WriteLine(" 1) List Blogs");
            Console.WriteLine(" 2) Add Blog");
            Console.WriteLine(" 3) Edit Blog");
            Console.WriteLine(" 4) Remove Blog");
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
            List<Blog> Blogs = _BlogRepository.GetAll();
            foreach (Blog blog in Blogs)
            {
                Console.WriteLine($"{blog.Title}, {blog.Url}");
            }
        }

        private Blog Choose(string prompt = null)
        {
            if (prompt == null)
            {
                prompt = "Please choose a Blog:";
            }

            Console.WriteLine(prompt);

            List<Blog> Blogs = _BlogRepository.GetAll();

            for (int i = 0; i < Blogs.Count; i++)
            {
                Blog Blog = Blogs[i];
                Console.WriteLine($" {i + 1}) {Blog.Title}");
            }
            Console.Write("> ");

            string input = Console.ReadLine();
            try
            {
                int choice = int.Parse(input);
                return Blogs[choice - 1];
            }
            catch (Exception ex)
            {
                Console.WriteLine("Invalid Selection");
                return null;
            }
        }

        private void Add()
        {
            Console.WriteLine("New Blog");
            Blog blog = new Blog();

            Console.WriteLine("Title: ");
            blog.Title = Console.ReadLine();

            Console.WriteLine("Text Content: ");
            blog.Content = Console.ReadLine();

            //make this an auto time setter
            Console.WriteLine("Date: ");
            blog.CreateDateTime = DateTime.Now;

            _BlogRepository.Insert(blog);
        }

        private void Edit()
        {
            Blog BlogToEdit = Choose("Which Blog would you like to edit?");
            if (BlogToEdit == null)
            {
                return;
            }

            Console.WriteLine();
            Console.Write("New title (blank to leave unchanged: ");
            string title = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(title))
            {
                BlogToEdit.Title = title;
            }
            Console.Write("New content (blank to leave unchanged: ");
            string content = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(content))
            {
                BlogToEdit.Content = content;
            }
            Console.Write("New Date (blank to leave unchanged: ");


            BlogToEdit.CreateDateTime = DateTime.Now;


            _BlogRepository.Update(BlogToEdit);
        }

        private void Remove()
        {
            Blog BlogToDelete = Choose("Which Blog would you like to remove?");
            if (BlogToDelete != null)
            {
                _BlogRepository.Delete(BlogToDelete.Id);
            }
        }
    }
}