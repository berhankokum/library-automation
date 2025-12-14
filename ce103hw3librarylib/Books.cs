using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ce103_hw3__library_lib
{
    // Clean Book Model
    public class Book
    {
        public int Id { get; set; }
        public string BookName { get; set; }
        public string Author { get; set; }
        public string Category { get; set; }
        public int Pages { get; set; }
        public int Year { get; set; }
        public int Edition { get; set; }
        public string Editors { get; set; }
        public string Publisher { get; set; }
        public string City { get; set; }
        public double Price { get; set; }
        public string AuthorKeywords { get; set; }
        public string Tags { get; set; }
        public string Abstract { get; set; }
        public string Url { get; set; }
        public int Doi { get; set; }
        public int Isbn { get; set; }
        public int Rack { get; set; }
        public int Row { get; set; }

        public override string ToString()
        {
            return $"ID: {Id}\nBook Name: {BookName}\nAuthor: {Author}\nCategory: {Category}\nYear: {Year}\nPages: {Pages}\nEdition: {Edition}\nEditor: {Editors}\nPublisher: {Publisher}\nPrice: {Price}\nCity: {City}\nAuthor Keywords: {AuthorKeywords}\nTags: {Tags}\nAbstract: {Abstract}\nURL: {Url}\nDOI: {Doi}\nISBN: {Isbn}\nRack no: {Rack}\nRow no: {Row}\n-------------------------";
        }
    }

    // Service class to handle logic
    public class LibraryManager
    {
        // Dynamic root path
        private readonly string _rootPath;
        private readonly string _booksPath;
        private readonly string _categoriesPath;
        private readonly string _borrowedPath;
        private readonly string _returnedPath;

        public LibraryManager()
        {
            // Use BaseDirectory for portability
            _rootPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "LibraryData");
            _booksPath = Path.Combine(_rootPath, "Books");
            _categoriesPath = Path.Combine(_rootPath, "Categories");
            _borrowedPath = Path.Combine(_rootPath, "BookStatus", "Borrowed");
            _returnedPath = Path.Combine(_rootPath, "BookStatus", "Returned");

            InitializeDirectories();
        }

        private void InitializeDirectories()
        {
            Directory.CreateDirectory(_rootPath);
            Directory.CreateDirectory(_booksPath);
            Directory.CreateDirectory(_categoriesPath);
            Directory.CreateDirectory(_borrowedPath);
            Directory.CreateDirectory(_returnedPath);
        }

        public bool AddBook(Book book)
        {
            try
            {
                string filePath = Path.Combine(_booksPath, $"{book.Id}.dat");
                if (File.Exists(filePath))
                {
                    return false; // Book already exists
                }

                // Create Category Directory if not exists
                string categoryPath = Path.Combine(_categoriesPath, book.Category);
                Directory.CreateDirectory(categoryPath);

                // Save to Books folder
                File.WriteAllText(filePath, book.ToString(), Encoding.GetEncoding("Windows-1254"));

                // Save to Categories folder (Original logic copied for consistency)
                string catFilePath = Path.Combine(categoryPath, $"{book.BookName}.dat");
                File.WriteAllText(catFilePath, book.ToString(), Encoding.GetEncoding("Windows-1254"));

                // Default status: Returned (Available)
                string statusPath = Path.Combine(_returnedPath, $"{book.Id}.dat");
                File.WriteAllText(statusPath, book.ToString(), Encoding.GetEncoding("Windows-1254"));

                return true;
            }
            catch (Exception)
            {
                // In a real app, log the exception
                return false;
            }
        }

        public string GetBookContent(int id)
        {
            string filePath = Path.Combine(_booksPath, $"{id}.dat");
            if (File.Exists(filePath))
            {
                try
                {
                    return File.ReadAllText(filePath, Encoding.GetEncoding("Windows-1254"));
                }
                catch
                {
                    return null;
                }
            }
            return null;
        }

        public List<string> GetAllBooks()
        {
            var books = new List<string>();
            try
            {
                // Assuming we just read all files in Books directory
                var files = Directory.GetFiles(_booksPath, "*.dat");
                foreach (var file in files)
                {
                    books.Add(File.ReadAllText(file, Encoding.GetEncoding("Windows-1254")));
                }
            }
            catch
            {
                // Handle error
            }
            return books;
        }

        public bool DeleteBook(int id)
        {
            try
            {
                string filePath = Path.Combine(_booksPath, $"{id}.dat");
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                    
                    // Also try to delete from status folders to keep clean
                    string borrowedFile = Path.Combine(_borrowedPath, $"{id}.dat");
                    if (File.Exists(borrowedFile)) File.Delete(borrowedFile);

                    string returnedFile = Path.Combine(_returnedPath, $"{id}.dat");
                    if (File.Exists(returnedFile)) File.Delete(returnedFile);

                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        public bool AddCategory(string categoryName)
        {
            try
            {
                string path = Path.Combine(_categoriesPath, categoryName);
                if (Directory.Exists(path))
                {
                    return false;
                }
                Directory.CreateDirectory(path);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool DeleteCategory(string categoryName)
        {
            try
            {
                string path = Path.Combine(_categoriesPath, categoryName);
                if (Directory.Exists(path))
                {
                    Directory.Delete(path, true);
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        public string GetBookStatus(int id)
        {
            if (File.Exists(Path.Combine(_borrowedPath, $"{id}.dat")))
                return "Borrowed";
            if (File.Exists(Path.Combine(_returnedPath, $"{id}.dat")))
                return "Available";
            
            return "NotFound";
        }

        public bool UpdateBookStatus(int id, bool toBorrowed)
        {
            try
            {
                string sourceDir = toBorrowed ? _returnedPath : _borrowedPath;
                string targetDir = toBorrowed ? _borrowedPath : _returnedPath;

                string sourceFile = Path.Combine(sourceDir, $"{id}.dat");
                string targetFile = Path.Combine(targetDir, $"{id}.dat");

                if (File.Exists(sourceFile))
                {
                    if (File.Exists(targetFile)) File.Delete(targetFile); // Ensure target is clear
                    File.Move(sourceFile, targetFile);
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }
    }
}
