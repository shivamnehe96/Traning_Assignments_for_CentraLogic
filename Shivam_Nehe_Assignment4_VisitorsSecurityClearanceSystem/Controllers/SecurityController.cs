using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos;
using SecurityClearanceSystem.DTO;
using SecurityClearanceSystem.Entity;
using SecurityClearanceSystem.Interface;

namespace SecurityClearanceSystem.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SecurityController : ControllerBase
    {
        public readonly Container _container;
        public ISecurityInterface _securityInterface;

        private Container GetContainer()
        {
            string URI = Environment.GetEnvironmentVariable("URI");
            string PrimaryKey = Environment.GetEnvironmentVariable("PrimaryKey");
            string DatabaseName = Environment.GetEnvironmentVariable("DatabaseName");
            string ContainerName = Environment.GetEnvironmentVariable("ContainerName");
            CosmosClient cosmosclient = new CosmosClient(URI, PrimaryKey);
           
            Database databse = cosmosclient.GetDatabase(DatabaseName);
     
            Container container = databse.GetContainer(ContainerName);

            return container;
        }
        public SecurityController(ISecurityInterface securityInterface)
        {
            _container = GetContainer();
            _securityInterface = securityInterface;

        }


        [HttpPost]
        public async Task<IActionResult> SecurityRegister(SecurityDTO securityDTO)
        {

           SecurityEntity securityEntity = new SecurityEntity();

            securityEntity.Name = securityDTO.Name;
            securityEntity.BadgeNumber = securityDTO.BadgeNumber;
            securityEntity.Department = securityDTO.Department;
            securityEntity.Email = securityDTO.Email;
            securityEntity.PhoneNumber = securityDTO.PhoneNumber;
            securityEntity.Role = securityDTO.Role;



            var responce = await _securityInterface.SecurityRegister(securityEntity);

            securityDTO.Name = responce.Name;
            securityDTO.BadgeNumber = responce.BadgeNumber;
            securityDTO.Department = responce.Department;
            securityDTO.Email = responce.Email;
            securityDTO.PhoneNumber = responce.PhoneNumber;
            securityDTO.Role = responce.Role;

            return Ok(securityDTO);
        }

        [HttpGet]
        public IActionResult GetPendingVisitorRequests()
        {
            var pendingRequests = _securityInterface.GetPendingVisitorRequests();
            return Ok(pendingRequests);
        }


    }
}