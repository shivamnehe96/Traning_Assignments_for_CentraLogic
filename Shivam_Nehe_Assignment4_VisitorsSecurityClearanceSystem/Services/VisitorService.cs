using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Linq;
using SecurityClearanceSystem.DTO;
using SecurityClearanceSystem.Entity;
using SecurityClearanceSystem.Interface;

namespace SecurityClearanceSystem.Services
{
    public class VisitorService : IVisitorInterface
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
        public VisitorService()
        {
            _container = GetContainer();
        }

        public async Task<VisitorEntity> VisitorRegister(VisitorEntity visitor)
        {
            visitor.Id = Guid.NewGuid().ToString();
            visitor.VisitorId = visitor.Id;
            visitor.Status = "Pending";
            visitor.entryTime = DateTime.Now;
            visitor.exitTime = DateTime.Now;
            visitor.DocumnetType = "Vsecurityclearance";
            visitor.Version = 1;
            visitor.CreatedBy = "shivam";
            visitor.CreatedOn = DateTime.Now;
            visitor.UpdatedBy = "shivam";
            visitor.UpdatedOn = DateTime.Now;
            visitor.Active = true;
            visitor.Archived = false;

            VisitorEntity resposne = await _container.CreateItemAsync(visitor);

            return resposne;
        }


        public async Task<VisitorEntity> VisitorById(string visitorId)
        {

            ItemResponse<VisitorEntity> response = await _container.ReadItemAsync<VisitorEntity>(visitorId, new PartitionKey(visitorId));
            return response.Resource;

        }

        public async Task<VisitorEntity> UpdateVisitor(VisitorEntity visitor)
        {


            visitor.Id = Guid.NewGuid().ToString();
            visitor.VisitorId = visitor.Id;
            visitor.Status = "Pending";
            visitor.entryTime = DateTime.Now;
            visitor.exitTime = DateTime.Now;
            visitor.DocumnetType = "Vsecurityclearance";
            visitor.Version = visitor.Version+1;
            visitor.CreatedBy = "shivam";
            visitor.CreatedOn = DateTime.Now;
            visitor.UpdatedBy = "shivam";
            visitor.UpdatedOn = DateTime.Now;
            visitor.Active = true;
            visitor.Archived = false;

           
            var response = await _container.CreateItemAsync(visitor);
            return response;


        }
    }
}
