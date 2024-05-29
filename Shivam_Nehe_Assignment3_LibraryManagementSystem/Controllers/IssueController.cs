using LibraryManagementSystem.Entity;
using LibraryManagementSystem.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos;
using System.Reflection.Metadata;
using System.Security.Policy;

namespace LibraryManagementSystem.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class IssueController : ControllerBase
    {
        public string URI = "https://localhost:8081";
        public string PrimaryKey = "C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw==";
        public string DatabaseName = "LibraryManager";
        public string ContainerName = "Issue";


        public Container Container;

        private Container GetContainer()
        {
            CosmosClient cosmosClient = new CosmosClient(URI, PrimaryKey);
            Database database = cosmosClient.GetDatabase(DatabaseName);
            Container container = database.GetContainer(ContainerName);
            return container;



        }
        public IssueController()
        {
            Container = GetContainer();
        }

        private Container GetBookContainer()
        {
            var bookDatabaseName = "LibraryManager"; 
            var bookContainerName = "Book"; 
            CosmosClient cosmosClient = new CosmosClient(URI, PrimaryKey);
            Database database = cosmosClient.GetDatabase(bookDatabaseName);
            return database.GetContainer(bookContainerName);
        }

        //user issues book from the library Issue book entity should be created
        [HttpPost]
        public async Task<IActionResult> IssueBook(IssueEntity issue)
        {
            issue.Id = Guid.NewGuid().ToString();
            issue.IssueDate = DateTime.UtcNow;
            issue.IsReturned = false;
            issue.CreatedOn = DateTime.UtcNow;
            issue.UpdatedOn = DateTime.UtcNow;
            issue.Version = 1;
            issue.Active = true;
            issue.Archived = false;

            var bookContainer = GetBookContainer();
            var bookQuery = bookContainer.GetItemLinqQueryable<Book>(true) .Where(b => b.Id == issue.BookId).FirstOrDefault();
            
            if (bookQuery == null)
            {
                return NotFound("Book not found.");
            }
            bookQuery.IsIssued = true;
            await bookContainer.ReplaceItemAsync(bookQuery, bookQuery.Id);

            await Container.CreateItemAsync(issue);

            return CreatedAtAction(nameof(GetIssueById), new { id = issue.Id }, issue);
        }



        // Retrieve the issue entity by ID
        [HttpGet("{id}")]
        public async Task<ActionResult<IssueEntity>> GetIssueById(string id)
        {
           
                ItemResponse<IssueEntity> response = await Container.ReadItemAsync<IssueEntity>(id, new PartitionKey(id));
                return response.Resource;
        }

        //Get the issue book UID

        [HttpGet]
        public async Task<List<Book>> GetIssuedBookByUid(string uId)
        {
            var issueBook = Container.GetItemLinqQueryable<Book>(true).Where(q => q.UId== uId && q.IsIssued==true).ToList();
            return issueBook;


        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateIssue(string id, [FromBody] IssueEntity updatedIssue)
        {
           
            
               
                ItemResponse<IssueEntity> response = await Container.ReadItemAsync<IssueEntity>(id, new PartitionKey(id));
                var existingIssue = response.Resource;

             
                existingIssue.UId = updatedIssue.UId;
                existingIssue.BookId = updatedIssue.BookId;
                existingIssue.MemberId = updatedIssue.MemberId;
                existingIssue.IssueDate = updatedIssue.IssueDate;
                existingIssue.ReturnDate = updatedIssue.ReturnDate;
                existingIssue.IsReturned = updatedIssue.IsReturned;
                existingIssue.UpdatedOn = DateTime.UtcNow;
                existingIssue.Version += 1;
                existingIssue.Active = updatedIssue.Active;
                existingIssue.Archived = updatedIssue.Archived;

               
                await Container.ReplaceItemAsync(existingIssue, existingIssue.Id, new PartitionKey(existingIssue.Id));

                return Ok(existingIssue);
        }
            
    }
}
