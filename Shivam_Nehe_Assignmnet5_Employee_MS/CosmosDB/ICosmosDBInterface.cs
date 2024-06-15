using EmployeeManagementSystem.Entity;

namespace EmployeeManagementSystem.CosmosDB
{
    public interface ICosmosDBInterface
    {
        Task<EmployeeBasicDetailsEntity> AddEmployeeBasicDetails(EmployeeBasicDetailsEntity employeeBasicDetailsEntity);

        Task<EmployeeBasicDetailsEntity> GetEmployeeById(string id);

        Task<IEnumerable<EmployeeBasicDetailsEntity>> GetAllEmployees();

        Task<EmployeeBasicDetailsEntity> UpdateEmployeeBasicDetails(string id, EmployeeBasicDetailsEntity employeeBasicDetailsEntity);

        Task<bool> DeleteEmployeeBasicDetails(string id);

        Task<EmployeeAdditionalDetailsEntity> AddEmployeeAdditionalDetails(EmployeeAdditionalDetailsEntity employeeAdditionalDetailsEntity);

        Task<EmployeeAdditionalDetailsEntity> GetAdditionalDetailsOfEmployeeById(string id);

        Task<IEnumerable<EmployeeAdditionalDetailsEntity>> GetAllEmployeesWithAdditioanlDetails();


        Task<EmployeeAdditionalDetailsEntity> UpdateEmployeeForAdditionalDetails(string id, EmployeeAdditionalDetailsEntity employeeAdditionalDetailsEntity);

        Task<bool> DeleteEmployeeForAdditionalDetails(string id);

    }

}

