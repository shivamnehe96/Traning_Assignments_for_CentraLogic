using LibraryManagementSystem.Entity;
using LibraryManagementSystem.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos;

namespace LibraryManagementSystem.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class MemberController : ControllerBase
    {
        public string URI = "https://localhost:8081";
        public string PrimaryKey = "C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw==";
        public string DatabaseName = "LibraryManager";
        public string ContainerName = "Member";


        public Container Container;

        private Container GetContainer()
        {
            CosmosClient cosmosClient = new CosmosClient(URI, PrimaryKey);
            Database database = cosmosClient.GetDatabase(DatabaseName);
            Container container = database.GetContainer(ContainerName);
            return container;



        }
        public MemberController()
        {
            Container = GetContainer();
        }

        //Adding Member to the database

        [HttpPost]
        public async Task<IActionResult> AddMember(Member member)
        {
            member.Id = Guid.NewGuid().ToString();

            var newMember =  await Container.CreateItemAsync(member);
            return Ok(newMember);
        }

        // Retrive Member By Uid
        [HttpGet]
        public async Task<Member> GetMemberByUid(string uId) 
        {
            var getMember= Container.GetItemLinqQueryable<Member>(true).Where(q=> q.UId == uId).FirstOrDefault();
            return getMember;
        }

        //Retrive All Members

        [HttpGet]
        public async Task<List<Member>> GetAllMembers()
        {
            var allMembers= Container.GetItemLinqQueryable<Member>(true).ToList();
            return allMembers;

        }

        //Update Member
        [HttpPut]
        public async Task<IActionResult> UpdateMember(Member member)
        {
            if (string.IsNullOrEmpty(member.Id))
            {
                return BadRequest("Member Id is required for update.");
            }

            var existingMember = Container.GetItemLinqQueryable<Member>(true).Where(q => q.Id == member.Id).FirstOrDefault();

            if (existingMember == null)
            {
                return NotFound($"Member with Id {member.Id} not found.");
            }

            // Update properties of the existing member
            existingMember.UId = member.UId;
            existingMember.Name = member.Name;
            existingMember.DateOfBirth = member.DateOfBirth;
            existingMember.Email = member.Email;

            try
            {
                var response = await Container.ReplaceItemAsync(existingMember, existingMember.Id);
                return Ok(response.Resource);
            }
            catch (CosmosException ex)
            {
                return StatusCode((int)ex.StatusCode, ex.Message);
            }
        }

        }
}
