using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos;
using SecurityClearanceSystem.DTO;
using SecurityClearanceSystem.Entity;
using SecurityClearanceSystem.Interface;

namespace SecurityClearanceSystem.Controllers
{
     [Route("api/[controller]/[action]")]
     [ApiController]
        public class VisitorController : ControllerBase
        {
            public readonly Container _container;
            public IVisitorInterface _visitorInterface;

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

            public VisitorController(IVisitorInterface visitorInterface)
            {
                _container = GetContainer();
                _visitorInterface = visitorInterface;
            }

            [HttpPost]
            public async Task<IActionResult> VisitorRegister(VisitorDTO visitorDTO)
            {
                VisitorEntity visitorEntity = new VisitorEntity();
             
                visitorEntity.Name = visitorDTO.Name;
                visitorEntity.PhoneNumber = visitorDTO.PhoneNumber;
                visitorEntity.Address = visitorDTO.Address;
                visitorEntity.Email = visitorDTO.Email;
                visitorEntity.Purpose = visitorDTO.Purpose;
                visitorEntity.VisitingTo = visitorDTO.VisitingTo;


              
                var responce = await _visitorInterface.VisitorRegister(visitorEntity);
              
                visitorDTO.Name = responce.Name;
                visitorDTO.PhoneNumber = responce.PhoneNumber;
                visitorDTO.Address = responce.Address;
                visitorDTO.Email = responce.Email;
                visitorDTO.Purpose = responce.Purpose;

                return Ok(visitorDTO);
            }

             [HttpGet("{visitorId}")]
             public async Task<IActionResult> VisitorById(string visitorId)
            {
            
            var visitor = await _visitorInterface.VisitorById(visitorId);
            return Ok(visitor);

           
             }

        
        [HttpPost]
        public async Task<IActionResult> UpdateVisitor(VisitorDTO visitorDTO)
        {
            
            var visitorEntity = await _visitorInterface.VisitorById(visitorDTO.Id);

            if (visitorEntity == null)
            {
                return NotFound("Visitor not found");
            }

            
            visitorEntity.Name = visitorDTO.Name;
            visitorEntity.PhoneNumber = visitorDTO.PhoneNumber;
            visitorEntity.Address = visitorDTO.Address;
            visitorEntity.Email = visitorDTO.Email;
            visitorEntity.Purpose = visitorDTO.Purpose;
            visitorEntity.VisitingTo = visitorDTO.VisitingTo;

           

            
            var response = await _visitorInterface.UpdateVisitor(visitorEntity);
            return Ok(visitorDTO);
        }
        


    }

}
