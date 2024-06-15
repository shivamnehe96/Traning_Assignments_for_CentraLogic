using EmployeeManagementSystem.Comman;
using EmployeeManagementSystem.Entity;
using EmployeeManagementSystem.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos;

namespace EmployeeManagementSystem.CosmosDB
{
    public class CosmosDBService: ICosmosDBInterface
    {
        public readonly Container container;

        public CosmosDBService()
        {
            container = GetContainer();
        }

        private Container GetContainer()
        {
            string databaseName = Credentials.DatabaseName;
            string containerName = Credentials.ContainerName;
            string cosmosEndPointURI = Credentials.CosmosEndPointURI;
            string primaryKey = Credentials.PrimaryKey;

            CosmosClient cosmosClient = new CosmosClient(cosmosEndPointURI, primaryKey);
            return cosmosClient.GetContainer(databaseName, containerName);
        }


        public async Task<EmployeeBasicDetailsEntity> AddEmployeeBasicDetails(EmployeeBasicDetailsEntity employeeBasicDetailsEntity)
        {
            employeeBasicDetailsEntity.Id = Guid.NewGuid().ToString();
            var response = await container.CreateItemAsync(employeeBasicDetailsEntity);
            return response.Resource;
        }


        public async Task<EmployeeBasicDetailsEntity> GetEmployeeById(string id)
        { 
           var employee=container.GetItemLinqQueryable<EmployeeBasicDetailsEntity>(true)
                                  .Where(x => x.Id == id).FirstOrDefault();
            return employee;
            
        }


        public async Task<IEnumerable<EmployeeBasicDetailsEntity>> GetAllEmployees()
        {
            var activeEmployees = container.GetItemLinqQueryable<EmployeeBasicDetailsEntity>(true)
                                            .Where(e => e.Active)
                                            .ToList();

            return await Task.FromResult(activeEmployees);
        }

        public async Task<EmployeeBasicDetailsEntity> UpdateEmployeeBasicDetails(string id, EmployeeBasicDetailsEntity employeeBasicDetailsEntity)
        {
            
                var response = await container.ReplaceItemAsync(employeeBasicDetailsEntity, id, new PartitionKey(id));
                return response.Resource;
            
        }

        public async Task<bool> DeleteEmployeeBasicDetails(string id)
        {
           
                var response = await container.DeleteItemAsync<EmployeeBasicDetailsEntity>(id, new PartitionKey(id));
                return response.StatusCode == System.Net.HttpStatusCode.NoContent;
            
        }


        public async Task<EmployeeAdditionalDetailsEntity> AddEmployeeAdditionalDetails(EmployeeAdditionalDetailsEntity employeeAdditionalDetailsEntity)
        {
            employeeAdditionalDetailsEntity.Id = Guid.NewGuid().ToString();
            var response = await container.CreateItemAsync(employeeAdditionalDetailsEntity);
            return response.Resource;
        }

        public async Task<EmployeeAdditionalDetailsEntity> GetAdditionalDetailsOfEmployeeById(string id)
        {
            var employee = container.GetItemLinqQueryable<EmployeeAdditionalDetailsEntity>(true)
                                   .Where(x => x.Id == id).FirstOrDefault();
            return employee;

        }

      

       public async Task<IEnumerable<EmployeeAdditionalDetailsEntity>>GetAllEmployeesWithAdditioanlDetails()
        {
            var activeEmployees = container.GetItemLinqQueryable<EmployeeAdditionalDetailsEntity>(true)
                                            .Where(e => e.Active)
                                            .ToList();

            return await Task.FromResult(activeEmployees);
        }

        public async Task<EmployeeAdditionalDetailsEntity> UpdateEmployeeForAdditionalDetails(string id, EmployeeAdditionalDetailsEntity employeeAdditionalDetailsEntity)
        {

            var response = await container.ReplaceItemAsync(employeeAdditionalDetailsEntity, id, new PartitionKey(id));
            return response.Resource;

        }

        public async Task<bool> DeleteEmployeeForAdditionalDetails(string id)
        {

            var response = await container.DeleteItemAsync<EmployeeAdditionalDetailsEntity>(id, new PartitionKey(id));
            return response.StatusCode == System.Net.HttpStatusCode.NoContent;

        }
    }
}
