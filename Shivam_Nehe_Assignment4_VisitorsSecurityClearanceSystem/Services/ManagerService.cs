using Microsoft.Azure.Cosmos;
using SecurityClearanceSystem.Entity;
using SecurityClearanceSystem.Interface;

namespace SecurityClearanceSystem.Services
{
    public class ManagerService: IManagerInterface
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
        public ManagerService()
        {
            _container = GetContainer();

        }
        public async Task<ManagerEntity> ManagerRegister(ManagerEntity manager)
        {
            manager.Id = Guid.NewGuid().ToString();
            manager.Username = "shivam";
            manager.DocumnetType = "Msecurityclearance";
            manager.Version = 1;
            manager.CreatedBy = "shivam";
            manager.CreatedOn = DateTime.Now;
            manager.UpdatedBy = "shivam";
            manager.UpdatedOn = DateTime.Now;
            manager.Active = true;
            manager.Archived = false;

            ManagerEntity resposne = await _container.CreateItemAsync(manager);

            return resposne;
        }

    }
}
