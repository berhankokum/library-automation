# Library Automation System

A C# Console Application for managing library operations, built with a focus on Clean Code principles and modular architecture.

## 🚀 Features

- **Book Management**: Add, delete, and list books with detailed metadata (Title, Author, Category, ISBN, etc.).
- **Search System**: Search for books by ID.
- **Category Management**: Create and organize book categories dynamically.
- **Status Tracking**: Track books as "Borrowed" or "Available".
- **Data Persistence**: Uses a file-based system (`.dat` files) to store library data permanently.
- **Clean Architecture**: Separated Logic (`LibraryManager`) from UI (`Program.cs`).

## 🛠 Technologies

- **Language**: C#
- **Framework**: .NET Framework
- **Testing**: MSTest (Unit Testing)
- **Data Storage**: File System (Local Data)

## 📂 Project Structure

- **`ce103hw3ibraryapp`**: The Console User Interface (UI). Handles user input and displays menus.
- **`ce103hw3librarylib`**: The Core Library. Contains the `Book` model and `LibraryManager` service.
- **`ce103hw3librarylibtest`**: Unit Tests. Ensures the reliability of core functions.

## ⚙️ Setup & Usage

1. **Clone the repository**:
   ```bash
   git clone https://github.com/yourusername/library-automation.git
   ```

2. **Open the Solution**:
   Open `ce103hw3.sln` in Visual Studio.

3. **Build & Run**:
   - Set `ce103hw3ibraryapp` as the startup project.
   - Run the application.
   - Default Password: `ce103`

4. **Running Tests**:
   - Open **Test Explorer** in Visual Studio.
   - Click **Run All** to execute unit tests.

## 🧪 Unit Tests

The project includes comprehensive unit tests validating:
- Book Creation & Persistence
- File I/O Operations
- Category Management Logic
- Book Status Transitions (Borrowed <-> Returned)
- Error Handling

## 📝 License

This project is licensed under the MIT License.
