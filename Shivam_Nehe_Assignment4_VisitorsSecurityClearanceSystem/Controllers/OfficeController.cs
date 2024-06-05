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
    public class OfficeController : ControllerBase
    {
        public readonly Container _container;
        public IOfficeInterface _officeInterface;


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

        public OfficeController(IOfficeInterface officeInterface)
        {
            _container = GetContainer();
            _officeInterface = officeInterface;

        }


        [HttpPost]
        public async Task<IActionResult> OfficeRegister(OfficeDTO officeDTO)
        {

            OfficeEntity officeEntity = new OfficeEntity();

            officeEntity.Id = officeDTO.Id;
            officeEntity.OfficeName = officeDTO.OfficeName;
            officeEntity.Address = officeDTO.Address;
            officeEntity.Licensenumber = officeDTO.Licensenumber;
            officeEntity.Contactdetails = officeDTO.Contactdetails;
           


            var responce = await _officeInterface.OfficeRegister(officeEntity);

            officeDTO.Id = responce.Id;
            officeDTO.OfficeName = responce.OfficeName;
            officeDTO.Address = responce.Address;
            officeDTO.Licensenumber = responce.Licensenumber;
            officeDTO.Contactdetails = responce.Contactdetails;
           

            return Ok(officeDTO);
        }



    }
}
