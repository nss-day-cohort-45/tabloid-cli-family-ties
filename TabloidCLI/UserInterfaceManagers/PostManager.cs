using System;
using System.Collections.Generic;
using TabloidCLI.Models;
using TabloidCLI.Repositories;

namespace TabloidCLI.UserInterfaceManagers
{
    public class PostManager : IUserInterfaceManager
    {
        private readonly IUserInterfaceManager _parentUI;
        private PostRepository _PostRepository;
        private AuthorRepository _AuthorRepository;
        private string _connectionString;

        public PostManager(IUserInterfaceManager parentUI, string connectionString)
        {
            _parentUI = parentUI;
            _PostRepository = new PostRepository(connectionString);
            _AuthorRepository = new AuthorRepository(connectionString);
            _connectionString = connectionString;
        }

        public IUserInterfaceManager Execute()
        {
            Console.WriteLine("Post Menu");
            Console.WriteLine(" 1) List Posts");
            Console.WriteLine(" 2) Add Post");
            Console.WriteLine(" 3) Edit Post");
            Console.WriteLine(" 4) Remove Post");
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
            List<Post> Posts = _PostRepository.GetAll();
            foreach (Post Post in Posts)
            {
                Console.WriteLine($"{Post.Title}, {Post.URL}");
            }
        }

        private Post Choose(string prompt = null)
        {
            if (prompt == null)
            {
                prompt = "Please choose a Post:";
            }

            Console.WriteLine(prompt);

            List<Post> Posts = _PostRepository.GetAll();

            for (int i = 0; i < Posts.Count; i++)
            {
                Post Post = Posts[i];
                Console.WriteLine($" {i + 1}) {Post.Title}");
            }
            Console.Write("> ");

            string input = Console.ReadLine();
            try
            {
                int choice = int.Parse(input);
                return Posts[choice - 1];
            }
            catch (Exception ex)
            {
                Console.WriteLine("Invalid Selection");
                return null;
            }
        }

        private void Add()
        {
            Console.WriteLine("New Post");
            Post Post = new Post();

            Console.WriteLine("Title: ");
            Post.Title = Console.ReadLine();

            Console.WriteLine("Text URL: ");
            Post.URL = Console.ReadLine();
            Console.WriteLine("Please Choose an Author:");

            List<Author> authors = _AuthorRepository.GetAll();

            for (int i = 0; i < authors.Count; i++)
            {
                Author author = authors[i];
                Console.WriteLine($" {i + 1}) {author.FullName}");
            }
            Console.Write("> ");

            string input = Console.ReadLine();
            try
            {
                int choice = int.Parse(input);
                Author authorObject = authors.Find(a => a.Id == choice);
                Post.Author = authorObject;

            }
            catch (Exception)
            {
                Console.WriteLine("Invalid Selection");
            }
            

            Console.WriteLine("Please choose the blog this post came from");



            Post.PublishDateTime = DateTime.Now;


            _PostRepository.Insert(Post);
        }

        private void Edit()
        {
            Post PostToEdit = Choose("Which Post would you like to edit?");
            if (PostToEdit == null)
            {
                return;
            }

            Console.WriteLine();
            Console.Write("New title (blank to leave unchanged: ");
            string title = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(title))
            {
                PostToEdit.Title = title;
            }
            Console.Write("New Url (blank to leave unchanged: ");
            string Url = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(Url))
            {
                PostToEdit.URL = Url;
            }



            _PostRepository.Update(PostToEdit);
        }

        private void Remove()
        {
            Post PostToDelete = Choose("Which Post would you like to remove?");
            if (PostToDelete != null)
            {
                _PostRepository.Delete(PostToDelete.Id);
            }
        }
    }
}

