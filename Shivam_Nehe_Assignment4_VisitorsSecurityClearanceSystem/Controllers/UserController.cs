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
    public class UserController : ControllerBase
    {

        public readonly Container _container;
        public IUserInterface _userInterface;

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

        public UserController(IUserInterface userInterface)
        {
            _container = GetContainer();
            _userInterface = userInterface;

        }


        [HttpPost]
        public async Task<IActionResult> UserRegister(UserDTO userDTO)
        {

            UserEntity userEntity = new UserEntity();

            userEntity.Id = userDTO.Id;
            userEntity.Username = userDTO.Username;
            userEntity.Password = userDTO.Password;
            userEntity.PhoneNumber = userDTO.PhoneNumber;
            userEntity.Email = userDTO.Email;




            var responce = await _userInterface.UserRegister(userEntity);

            userDTO.Username = responce.Username;
            userDTO.Id = responce.Id;
            userDTO.PhoneNumber = responce.PhoneNumber;
            userDTO.Password = responce.Password;
            userDTO.Email = responce.Email;


            return Ok(userDTO);
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginUser(UserEntity userEntity)
        {
            var result = await _userInterface.LoginUser(userEntity);

            if (result != null)
            {
                return Ok(new
                {
                    Message = "Login successful",
                    UId = result
                });
            }

            return Unauthorized(new { Message = "Invalid credentials." });
        }
    }
}
