namespace MinimalAPI_2
{
    public interface IBookService
    {
        List<Book> GetBooks();
        Book GetBook(int id);
        void AddBook(Book book);
        Book UpdateBook(int id, Book updatedBook);
        bool DeleteBook(int id);
    }
    public class BookService : IBookService
    {
        private readonly List<Book> _books;

        public BookService()
        {
            _books = new List<Book>
            {
                new()
                {
                    Id = 1,
                    Title = "Dependency Injection",
                    Author = "Mark Seemann"
                },
                new()
                {
                    Id = 2,
                    Title = "Clean Code",
                    Author = "Robert C. Martin"
                },
                new()
                {
                    Id = 3,
                    Title = "Programming Entity Framework",
                    Author = "Julia Lerman"
                }
            };
        }

        public Book GetBook(int id)
        {
            var book = this._books.FirstOrDefault(x => x.Id == id);

            return book;
        }

        public List<Book> GetBooks()
        {
          return this._books;
        }

        public void AddBook(Book book)
        {
            book.Id = _books.Any() ? _books.Max(x => x.Id) + 1 : 1;
            _books.Add(book);
        }

        public Book UpdateBook(int id, Book updateBook)
        {
            var book = _books.FirstOrDefault(x => x.Id == id);
            if (book is null) return null;

            book.Title = updateBook.Title;
            book.Author = updateBook.Author;
            return book;
        }

        public bool DeleteBook(int id)
        {
            var book = _books.FirstOrDefault(x => x.Id == id);
            if (book is null) return false;

            _books.Remove(book);
            return true;
        }
    }

    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public string Author { get; set; }
    }
}
