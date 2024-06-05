using Microsoft.Azure.Cosmos;
using SecurityClearanceSystem.Entity;
using SecurityClearanceSystem.Interface;

namespace SecurityClearanceSystem.Services
{
    public class OfficeService: IOfficeInterface

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
        public OfficeService()
        {
            _container = GetContainer();

        }
        public async Task<OfficeEntity> OfficeRegister(OfficeEntity office)
        {
            office.Id = Guid.NewGuid().ToString();
            office.OfficeName = "technologies pvt.lmt.";
            office.DocumnetType = "Osecurityclearance";
            office.Version = 1;
            office.CreatedBy = "shivam";
            office.CreatedOn = DateTime.Now;
            office.UpdatedBy = "shivam";
            office.UpdatedOn = DateTime.Now;
            office.Active = true;
            office.Archived = false;

            OfficeEntity resposne = await _container.CreateItemAsync(office);

            return resposne;
        }
    }
}
