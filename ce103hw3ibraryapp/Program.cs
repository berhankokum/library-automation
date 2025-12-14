using ce103_hw3__library_lib;
using System;
using System.Collections.Generic;

namespace ce103_hw3_library_app
{
    public class Menu
    {
        // Instantiate the manager once
        static LibraryManager _manager = new LibraryManager();

        static void Main(string[] args)
        {
            Console.WriteLine(GetLogo());

            if (!AuthenticateUser())
            {
                return; // Exit if auth fails (though the loop handles retries)
            }

            bool exit = false;
            while (!exit)
            {
                Console.Clear();
                Console.WriteLine(GetMainMenu());
                Console.Write("Enter a choice: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AddBookMenu();
                        break;
                    case "2":
                        ListBooksMenu();
                        break;
                    case "3":
                        SearchBookMenu();
                        break;
                    case "4":
                        CategoriesMenu();
                        break;
                    case "5":
                        DeleteBookMenu();
                        break;
                    case "6":
                        Console.WriteLine("Edit feature not yet implemented.");
                        WaitForEsc();
                        break;
                    case "7":
                        BookStatusMenu();
                        break;
                    case "8":
                        Console.WriteLine("Borrowers feature not yet implemented.");
                        WaitForEsc();
                        break;
                    case "9":
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Press any key to try again.");
                        Console.ReadKey();
                        break;
                }
            }
        }

        static bool AuthenticateUser()
        {
            string password;
            do
            {
                Console.Write("Enter your password: ");
                password = Console.ReadLine();
                if (password == "ce103")
                {
                    Console.WriteLine("Correct.");
                    System.Threading.Thread.Sleep(500);
                    return true;
                }
                Console.WriteLine("Wrong password. Try again.");
            } while (true);
        }

        static void AddBookMenu()
        {
            Console.Clear();
            try
            {
                Book book = new Book();

                Console.Write("1-Book ID: ");
                book.Id = Convert.ToInt32(Console.ReadLine());

                Console.Write("2-Book name: ");
                book.BookName = Console.ReadLine();

                Console.Write("3-Author: ");
                book.Author = Console.ReadLine();

                Console.Write("4-Category: ");
                book.Category = Console.ReadLine();

                Console.Write("5-Year: ");
                book.Year = Convert.ToInt32(Console.ReadLine());

                Console.Write("6-Pages: ");
                book.Pages = Convert.ToInt32(Console.ReadLine());

                Console.Write("7-Edition: ");
                book.Edition = Convert.ToInt32(Console.ReadLine());

                Console.Write("8-Editors: ");
                book.Editors = Console.ReadLine();

                Console.Write("9-Publisher: ");
                book.Publisher = Console.ReadLine();

                Console.Write("10-Price: ");
                book.Price = Convert.ToDouble(Console.ReadLine());

                Console.Write("11-City: ");
                book.City = Console.ReadLine();

                Console.Write("12-Author Keywords: ");
                book.AuthorKeywords = Console.ReadLine();

                Console.Write("13-Tags: ");
                book.Tags = Console.ReadLine();

                Console.Write("14-Abstract: ");
                book.Abstract = Console.ReadLine();

                Console.Write("15-URL: ");
                book.Url = Console.ReadLine();

                Console.Write("16-DOI: ");
                book.Doi = Convert.ToInt32(Console.ReadLine());

                Console.Write("17-ISBN: ");
                book.Isbn = Convert.ToInt32(Console.ReadLine());

                Console.Write("18-Rack no: ");
                book.Rack = Convert.ToInt32(Console.ReadLine());

                Console.Write("19-Row no: ");
                book.Row = Convert.ToInt32(Console.ReadLine());

                if (_manager.AddBook(book))
                {
                    Console.WriteLine("Book added successfully.");
                }
                else
                {
                    Console.WriteLine("Failed to add book. ID might already exist.");
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid input format. Please enter numbers where required.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }

            WaitForEsc();
        }

        static void ListBooksMenu()
        {
            Console.Clear();
            var books = _manager.GetAllBooks();
            if (books.Count == 0)
            {
                Console.WriteLine("No books found.");
            }
            else
            {
                foreach (var b in books)
                {
                    Console.WriteLine(b);
                    Console.WriteLine();
                }
            }
            WaitForEsc();
        }

        static void SearchBookMenu()
        {
            Console.Clear();
            Console.Write("Enter Book ID to search: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                string content = _manager.GetBookContent(id);
                if (content != null)
                {
                    Console.WriteLine(content);
                }
                else
                {
                    Console.WriteLine("Book not found.");
                }
            }
            else
            {
                Console.WriteLine("Invalid ID format.");
            }
            WaitForEsc();
        }

        static void CategoriesMenu()
        {
            bool back = false;
            while (!back)
            {
                Console.Clear();
                Console.WriteLine("▓▓▓▓▓▓▓▓▓▓▓ Category Menu ▓▓▓▓▓▓▓▓▓▓▓");
                Console.WriteLine("1- Add Category");
                Console.WriteLine("2- Delete Category");
                Console.WriteLine("3- Back to Main Menu");
                Console.Write("Enter choice: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Console.Write("Enter category name to add: ");
                        string newCat = Console.ReadLine();
                        if (_manager.AddCategory(newCat))
                            Console.WriteLine("Category added.");
                        else
                            Console.WriteLine("Category already exists or invalid.");
                        WaitForEsc();
                        break;
                    case "2":
                        Console.Write("Enter category name to delete: ");
                        string delCat = Console.ReadLine();
                        if (_manager.DeleteCategory(delCat))
                            Console.WriteLine("Category deleted.");
                        else
                            Console.WriteLine("Category not found.");
                        WaitForEsc();
                        break;
                    case "3":
                        back = true;
                        break;
                    default:
                        Console.WriteLine("Invalid choice.");
                        System.Threading.Thread.Sleep(500);
                        break;
                }
            }
        }

        static void DeleteBookMenu()
        {
            Console.Clear();
            Console.Write("Enter Book ID to delete: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                if (_manager.DeleteBook(id))
                    Console.WriteLine("Book deleted successfully.");
                else
                    Console.WriteLine("Book not found or could not be deleted.");
            }
            else
                Console.WriteLine("Invalid ID.");
            
            WaitForEsc();
        }

        static void BookStatusMenu()
        {
            bool back = false;
            while (!back)
            {
                Console.Clear();
                Console.WriteLine("▓▓▓▓▓▓▓▓▓▓ Book Status Menu ▓▓▓▓▓▓▓▓▓▓");
                Console.WriteLine("1- Check Status");
                Console.WriteLine("2- Update Status");
                Console.WriteLine("3- Back");
                Console.Write("Choice: ");
                string c = Console.ReadLine();

                switch (c)
                {
                    case "1":
                        Console.Write("Enter Book ID: ");
                        if (int.TryParse(Console.ReadLine(), out int id))
                        {
                            Console.WriteLine($"Status: {_manager.GetBookStatus(id)}");
                        }
                        WaitForEsc();
                        break;
                    case "2":
                        Console.Write("Enter Book ID: ");
                        if (int.TryParse(Console.ReadLine(), out int uid))
                        {
                            Console.WriteLine("Set status to: (1) Borrowed (2) Returned/Available");
                            string s = Console.ReadLine();
                            if (s == "1")
                            {
                                if (_manager.UpdateBookStatus(uid, true)) Console.WriteLine("Updated to Borrowed.");
                                else Console.WriteLine("Make sure book exists and is available.");
                            }
                            else if (s == "2")
                            {
                                if (_manager.UpdateBookStatus(uid, false)) Console.WriteLine("Updated to Returned.");
                                else Console.WriteLine("Make sure book exists and is borrowed.");
                            }
                            else Console.WriteLine("Invalid selection.");
                        }
                        WaitForEsc();
                        break;
                    case "3":
                        back = true;
                        break;
                }
            }
        }

        static void WaitForEsc()
        {
            Console.WriteLine("\nPress ESC to go back...");
            while (Console.ReadKey(true).Key != ConsoleKey.Escape) { }
        }

        static string GetLogo()
        {
            return @"
  _      _____ ____  _____           _______     __   _______     _______ _______ ______ __  __ 
 | |    |_   _|  _ \|  __ \    /\   |  __ \ \   / /  / ____\ \   / / ____|__   __|  ____|  \/  |
 | |      | | | |_) | |__) |  /  \  | |__) \ \_/ /  | (___  \ \_/ | (___    | |  | |__  | \  / |
 | |      | | |  _ <|  _  /  / /\ \ |  _  / \   /    \___ \  \   / \___ \   | |  |  __| | |\/| |
 | |____ _| |_| |_) | | \ \ / ____ \| | \ \  | |     ____) |  | |  ____) |  | |  | |____| |  | |
 |______|_____|____/|_|  \_/_/    \_|_|  \_\ |_|    |_____/   |_| |_____/   |_|  |______|_|  |_|
";
        }

        static string GetMainMenu()
        {
            return @"
▓▓▓▓▓▓▓▓▓▓▓▓▓▓ Welcome to Library System ▓▓▓▓▓▓▓▓▓▓▓▓▓▓
                   1- Add Book
                   2- List Books
                   3- Search Book
                   4- Categories
                   5- Delete Book
                   6- Edit Book
                   7- Book status
                   8- Borrowers
                   9- Exit
";
        }
    }
}
