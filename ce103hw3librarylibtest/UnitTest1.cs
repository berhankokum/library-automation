using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ce103_hw3__library_lib;
using System.IO;

namespace ce103hw3librarylibtest
{
    [TestClass]
    public class UnitTest1
    {
        private LibraryManager _manager;
        private string _testRootPath;

        [TestInitialize]
        public void Setup()
        {
            // Reset manager and ensure fresh state for each test if possible,
            // or we just trust the manager to handle its paths.
            // Since LibraryManager uses AppDomain.BaseDirectory, we test against that.
            _manager = new LibraryManager();
            _testRootPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "LibraryData");
        }

        [TestCleanup]
        public void Cleanup()
        {
            // Clean up created files after tests
            if (Directory.Exists(_testRootPath))
            {
                Directory.Delete(_testRootPath, true);
            }
        }

        [TestMethod]
        public void AddBook_ShouldCreateFile_WhenNewBookAdded()
        {
            // Arrange
            var book = new Book
            {
                Id = 101,
                BookName = "Test Book",
                Author = "Test Author",
                Category = "TestCat",
                Year = 2023,
                Pages = 100,
                Edition = 1,
                Editors = "Ed",
                Publisher = "Pub",
                Price = 10,
                City = "City",
                AuthorKeywords = "Key",
                Tags = "Tag",
                Abstract = "Abs",
                Url = "http",
                Doi = 1,
                Isbn = 123,
                Rack = 1,
                Row = 1
            };

            // Act
            bool result = _manager.AddBook(book);

            // Assert
            Assert.IsTrue(result, "AddBook should return true for new book.");
            string expectedPath = Path.Combine(_testRootPath, "Books", "101.dat");
            Assert.IsTrue(File.Exists(expectedPath), "Book file should exist.");
        }

        [TestMethod]
        public void GetBookContent_ShouldReturnContent_WhenBookExists()
        {
            // Arrange
            var book = new Book { Id = 102, BookName = "Search Me", Category = "General" };
            _manager.AddBook(book);

            // Act
            string content = _manager.GetBookContent(102);

            // Assert
            Assert.IsNotNull(content);
            Assert.IsTrue(content.Contains("Search Me"));
        }

        [TestMethod]
        public void GetBookContent_ShouldReturnNull_WhenBookDoesNotExist()
        {
            // Act
            string content = _manager.GetBookContent(999);

            // Assert
            Assert.IsNull(content);
        }

        [TestMethod]
        public void Category_ShouldCreateAndDelete()
        {
            // Arrange
            string catName = "NewCategory";

            // Act - Add
            bool added = _manager.AddCategory(catName);
            bool exists = Directory.Exists(Path.Combine(_testRootPath, "Categories", catName));

            // Assert - Add
            Assert.IsTrue(added);
            Assert.IsTrue(exists);

            // Act - Delete
            bool deleted = _manager.DeleteCategory(catName);
            bool stillExists = Directory.Exists(Path.Combine(_testRootPath, "Categories", catName));

            // Assert - Delete
            Assert.IsTrue(deleted);
            Assert.IsFalse(stillExists);
        }

        [TestMethod]
        public void DeleteBook_ShouldRemoveFile()
        {
            // Arrange
            var book = new Book { Id = 103, BookName = "To Delete", Category = "Temp" };
            _manager.AddBook(book);

            // Act
            bool deleted = _manager.DeleteBook(103);

            // Assert
            Assert.IsTrue(deleted);
            string path = Path.Combine(_testRootPath, "Books", "103.dat");
            Assert.IsFalse(File.Exists(path), "File should be deleted.");
        }

        [TestMethod]
        public void UpdateBookStatus_ShouldMoveFile()
        {
            // Arrange
            var book = new Book { Id = 104, BookName = "Status Test", Category = "Status" };
            _manager.AddBook(book); // Defaults to Returned (Available)

            // Initial Check
            Assert.AreEqual("Available", _manager.GetBookStatus(104));

            // Act - Borrow
            bool borrowed = _manager.UpdateBookStatus(104, true);

            // Assert - Borrow
            Assert.IsTrue(borrowed);
            Assert.AreEqual("Borrowed", _manager.GetBookStatus(104));

            // Act - Return
            bool returned = _manager.UpdateBookStatus(104, false);

            // Assert - Return
            Assert.IsTrue(returned);
            Assert.AreEqual("Available", _manager.GetBookStatus(104));
        }
    }
}
