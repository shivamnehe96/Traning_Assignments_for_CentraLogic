using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos;
using SecurityClearanceSystem.DTO;
using SecurityClearanceSystem.Entity;
using SecurityClearanceSystem.Interface;

namespace SecurityClearanceSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManagerController : ControllerBase
    {
        public readonly Container _container;
        public IManagerInterface _managerInterface;

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

        public ManagerController(IManagerInterface managerInterface)
        {
            _container = GetContainer();
            _managerInterface = managerInterface;

        }


        [HttpPost]
        public async Task<IActionResult> ManagerRegister(ManagerDTO managerDTO)
        {

            ManagerEntity managerEntity = new ManagerEntity();

            managerEntity.Username = managerDTO.Username;
            managerEntity.Id = managerDTO.Id;
            managerEntity.Password = managerDTO.Password;
            managerEntity.PhoneNumber = managerDTO.PhoneNumber;
            managerEntity.Email = managerDTO.Email;
           



            var responce = await _managerInterface.ManagerRegister(managerEntity);

            managerDTO.Username = responce.Username;
            managerDTO.Id = responce.Id;
            managerDTO.PhoneNumber = responce.PhoneNumber;
            managerDTO.Password = responce.Password;
            managerDTO.PhoneNumber = responce.PhoneNumber;
         

            return Ok(managerDTO);
        }


    }
}
