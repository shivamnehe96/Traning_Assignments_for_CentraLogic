using LibraryManagementSystem.Entity;
using LibraryManagementSystem.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos;
using System.ComponentModel;
using Container = Microsoft.Azure.Cosmos.Container;

namespace LibraryManagementSystem.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        public string URI = "https://localhost:8081";
        public string PrimaryKey = "C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw==";
        public string DatabaseName = "LibraryManager";
        public string ContainerName = "Book";


         public Container Container;

        private Container GetContainer()
        {
            CosmosClient cosmosClient = new CosmosClient(URI, PrimaryKey);
            Database database = cosmosClient.GetDatabase(DatabaseName);
            Container container = database.GetContainer(ContainerName);
            return container;

                
                
        }
        public BookController()
        {
            Container = GetContainer();
        }

        //Adding the Book to the Database
        [HttpPost]
        public async Task<IActionResult> AddBook(Book book)
        {
            book.Id = Guid.NewGuid().ToString();

            var newBook =await Container.CreateItemAsync(book);
            return Ok(newBook);
        }

        // retrving the Book from the Database by its Uid

        [HttpGet]
        public async Task<Book> GetBookByUid(string uId)
        {
            var getBook = Container.GetItemLinqQueryable<Book>(true).Where(q => q.UId == uId).FirstOrDefault();
            return getBook;
        }

        //Retriving the Book from the databse by its name

        [HttpGet]
        public async Task<Book> GetBookByName(string title)
        {
            var bookName=Container.GetItemLinqQueryable<Book>(true).Where(q =>q.Title == title).FirstOrDefault();
            return bookName;
        }

        //Retrive All Books from the database

        [HttpGet]
        public async Task<List<Book>> GetAllBooks()
        {
            var allBooks=Container.GetItemLinqQueryable<Book>(true).ToList();
            return allBooks;
        }

        //Retrive All available books which are not issued

        [HttpGet]
        public async Task <List<Book>> GetAllUnissuedBook()
        {
            var unIssuedBook = Container.GetItemLinqQueryable<Book>(true).Where(q=> q.IsIssued==false).ToList();
            return unIssuedBook;
        }

        //Retrive All Issued books

        [HttpGet]
        public async Task<List<Book>> GetAllIssuedBooks()
        {
            var issuedBook=Container.GetItemLinqQueryable<Book>(true).Where(q=>q.IsIssued==true).ToList();
            return issuedBook;
        }

        // Adding the Book Entity 
        [HttpPost]
        public async Task<Book> AddBookEntity(Book book)
        {
            BookEntity entity = new BookEntity();
            entity.Title = book.Title;
            entity.Author = book.Author;
            entity.PublishedDate = book.PublishedDate;  
            entity.ISBN = book.ISBN;
            entity.IsIssued = book.IsIssued;

            entity.Id = Guid.NewGuid().ToString();
            entity.UId = entity.Id;
            entity.DocumnetType = "Book";
            entity.CreatedBy = "shivam";
            entity.CreatedOn = DateTime.Now;
            entity.UpdatedBy = "shivam";
            entity.UpdatedOn = DateTime.Now;
            entity.Version = 1;
            entity.Active = true;
            entity.Archived = false;

            BookEntity response = await Container.CreateItemAsync(entity);

            Book responseBook = new Book();
            responseBook.Title=response.Title;
            responseBook.Author = response.Author;
            responseBook.PublishedDate = response.PublishedDate;
            responseBook.ISBN = response.ISBN;
            responseBook.IsIssued= response.IsIssued;
            return responseBook;
        }



        //Update the Book
        
        [HttpPost]

        public async Task<Book> UpdateBook(Book book)
        {

            var existingBook = Container.GetItemLinqQueryable<BookEntity>(true).Where(q => q.UId == book.UId && q.Active == true && q.Archived==false).FirstOrDefault();

            if (existingBook == null)
            {
              throw new InvalidOperationException("Book not found or already archived.");
            }
            existingBook.Archived = true;
            existingBook.Active = false;
            await Container.ReplaceItemAsync(existingBook, existingBook.Id);


            existingBook.Id = Guid.NewGuid().ToString();
            existingBook.UpdatedBy = "Shivam";
            existingBook.UpdatedOn = DateTime.Now;
            existingBook.Version = existingBook.Version + 1;
            existingBook.Active = true;
            existingBook.Archived = false;

            existingBook.Title = book.Title;
            existingBook.Author = book.Author;
            existingBook.PublishedDate = book.PublishedDate;
            existingBook.ISBN = book.ISBN;
            existingBook.IsIssued = book.IsIssued;

            existingBook=await Container.CreateItemAsync(existingBook);


            Book responce = new Book();
            responce.UId = existingBook.UId;
            responce.Title = existingBook.Title;
            responce.Author = existingBook.Author;
            responce.PublishedDate = existingBook.PublishedDate;
            responce.ISBN = existingBook.ISBN;
            responce.IsIssued= existingBook.IsIssued;

            return responce;



        }
        

       

    }
}


