using EmployeeManagementSystem.Comman;
using EmployeeManagementSystem.CosmosDB;
using EmployeeManagementSystem.DTO;
using EmployeeManagementSystem.Entity;
using EmployeeManagementSystem.Interfaces;
using Microsoft.Azure.Cosmos;
using static EmployeeManagementSystem.Services.EmployeeBasicDetailsService;

namespace EmployeeManagementSystem.Services
{
    public class EmployeeBasicDetailsService : IEmployeeBasicDetailsInterface
    {

        public ICosmosDBInterface _cosmosService;
        public EmployeeBasicDetailsService(ICosmosDBInterface cosmosService)
        {
            _cosmosService = cosmosService;
        }

        public async Task<EmployeeBasicDetailsEntity> AddEmployeeBasicDetails(EmployeeBasicDetailsEntity employeeBasicDetailsEntity)
        {
            employeeBasicDetailsEntity.Id = Guid.NewGuid().ToString();

            employeeBasicDetailsEntity.DocumnetType = "EmployeeBasicsDetails";

            employeeBasicDetailsEntity.CreatedBy = "shivam";

            employeeBasicDetailsEntity.CreatedOn = DateTime.Now;

            employeeBasicDetailsEntity.UpdatedBy = "shivam";

            employeeBasicDetailsEntity.UpdatedOn = DateTime.Now;

            employeeBasicDetailsEntity.Version = 1;
            employeeBasicDetailsEntity.Active = true;
            employeeBasicDetailsEntity.Archived = false;

            EmployeeBasicDetailsEntity respons = await _cosmosService.AddEmployeeBasicDetails(employeeBasicDetailsEntity);
            return respons;

        }

        public async Task<EmployeeBasicDetailsEntity> GetEmployeeById(string id)
        {
            return await _cosmosService.GetEmployeeById(id);
        }


        public async Task<List<EmployeeBasicDetailsEntity>> GetAllEmployees()
        {
            return await _cosmosService.GetAllEmployees();
        }

        public async Task<EmployeeBasicDetailsEntity> UpdateEmployeeBasicDetails(string id, EmployeeBasicDetailsEntity employeeBasicDetailsEntity)
        {
            return await _cosmosService.UpdateEmployeeBasicDetails(id, employeeBasicDetailsEntity);
        }


        public async Task<bool> DeleteEmployeeBasicDetails(string id)
        {
            return await _cosmosService.DeleteEmployeeBasicDetails(id);
        }

        public async Task<List<EmployeeBasicDetailsEntity> >GetAllEmployeeByRole(string role)
        {
            return await _cosmosService.GetAllEmployeeByRole(role);
        }

        public async Task<EmployeeFilterCriteria> GetAllEmployeeByPagination(EmployeeFilterCriteria employeeFilterCriteria)
        {
            return await _cosmosService.GetAllEmployeeByPagination(employeeFilterCriteria);
        }

        public async Task<EmployeeBasicDetailsDTO> AddEmployeeByMakePostRequest(EmployeeBasicDetailsDTO employeeBasicDetailsDTO)
        {
            return await _cosmosService.AddEmployeeByMakePostRequest(employeeBasicDetailsDTO);
        }

        public async Task<EmployeeBasicDetailsEntity>GetAllEmployeeByMakeGetRequest(string employeeId )
        {
            return await _cosmosService.GetAllEmployeeByMakeGetRequest(employeeId);
        }
    }
}
