using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Linq;
using SecurityClearanceSystem.DTO;
using SecurityClearanceSystem.Entity;
using SecurityClearanceSystem.Interface;

namespace SecurityClearanceSystem.Services
{
    public class SecurityService: ISecurityInterface
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
        public SecurityService()
        {
            _container = GetContainer();

        }
        public async Task<SecurityEntity> SecurityRegister(SecurityEntity security)
        {
            security.Id = Guid.NewGuid().ToString();
            security.BadgeNumber = security.Id;
            security.DocumnetType = "Ssecurityclearance";
            security.Version = 1;
            security.CreatedBy = "shivam";
            security.CreatedOn = DateTime.Now;
            security.UpdatedBy = "shivam";
            security.UpdatedOn = DateTime.Now;
            security.Active = true;
            security.Archived = false;

            SecurityEntity resposne = await _container.CreateItemAsync(security);

            return resposne;
        }

        public List<VisitorDTO> GetPendingVisitorRequests()
        {
            var pendingVisitors = _container.GetItemLinqQueryable<VisitorEntity>()
                .Where(v => v.Status == "Pending")
                .ToFeedIterator();

            var result = new List<VisitorDTO>();

            while (pendingVisitors.HasMoreResults)
            {
                var response = pendingVisitors.ReadNextAsync().Result;

                foreach (var visitor in response)
                {
                    // Map VisitorEntity to VisitorDTO, you can create a method for mapping
                    var visitorDTO = new VisitorDTO
                    {
                        Name = visitor.Name,
                        Email = visitor.Email,
                        PhoneNumber = visitor.PhoneNumber,
                        Address = visitor.Address,
                        Purpose = visitor.Purpose,
                        VisitingTo = visitor.VisitingTo
                    };

                    result.Add(visitorDTO);
                }
            }

            return result;
        }


    }
}
