using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Azure.Cosmos;
using SecurityClearanceSystem.Entity;
using SecurityClearanceSystem.Interface;

namespace SecurityClearanceSystem.Services
{
    public class UserService: IUserInterface
    {
        public readonly Container _container;
        private Container GetContainer()
        {
            string URI = Environment.GetEnvironmentVariable("URI");
            string PrimaryKey = Environment.GetEnvironmentVariable("PrimaryKey");
            string DatabaseName = Environment.GetEnvironmentVariable("DatabaseName");
            string ContainerName = Environment.GetEnvironmentVariable("ContainerName");
            CosmosClient cosmosclient = new CosmosClient(URI, PrimaryKey);

            Database database = cosmosclient.GetDatabase(DatabaseName);

            Container container = database.GetContainer(ContainerName);

            return container;
        }
        public UserService()
        {
            _container = GetContainer();

        }
        public async Task<UserEntity> UserRegister(UserEntity user)
        {
            user.Id = Guid.NewGuid().ToString();
            user.Username = "shivam";
            user.DocumnetType = "Msecurityclearance";
            user.Version = 1;
            user.CreatedBy = "shivam";
            user.CreatedOn = DateTime.Now;
            user.UpdatedBy = "shivam";
            user.UpdatedOn = DateTime.Now;
            user.Active = true;
            user.Archived = false;

            UserEntity resposne = await _container.CreateItemAsync(user);

            return resposne;
        }


        public async Task<string> LoginUser(UserEntity user)
        {
            var query = _container.GetItemLinqQueryable<UserEntity>()
                .Where(u => u.Username == user.Username && u.Password == user.Password)
                .Take(1);
            
           UserEntity response= await _container.CreateItemAsync(user);
            return response.Username;
        }


    }
}
